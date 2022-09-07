using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using System.Threading.Tasks;
using NSE.Clientes.API.Models;
using System.Linq;
using NSE.Core.Mediator;
using NSE.Core.DomainObjects;
using FluentValidation.Results;
using NSE.Core.Messages;

namespace NSE.Clientes.API.Data
{
    public sealed class ClientesContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClientesContext(DbContextOptions<ClientesContext> options,
            IMediatorHandler mediatorHandler) : base(options) 
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                        e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType(value: "varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(ctx: this);

            return sucesso;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.Notificacoes).ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (evento) =>
                {
                    await mediator.PublicarEvento(evento);
                });

            await Task.WhenAll(tasks);
        }
    }
}
