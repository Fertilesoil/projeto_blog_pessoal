﻿using blogpessoal.Model;
using Microsoft.EntityFrameworkCore;
using blogpessoal.Configuration;


namespace blogpessoal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postagem>().ToTable("tb_postagem");
            modelBuilder.Entity<Tema>().ToTable("tb_temas");
            modelBuilder.Entity<User>().ToTable("tb_usuarios");

               _= modelBuilder.Entity<Postagem>() // Indica quem tem a chave primária
                 .HasOne( _=> _.Tema) // Indica o tipo de relação, no caso tem um
                 .WithMany(t => t.Postagem) // Indica o tipo de relacionamento, tem muitos 
                 .HasForeignKey("TemaId") // Indica o nome da chave estrangeira no banco de dados
                 .OnDelete(DeleteBehavior.Cascade); // Indica o comportamento ao deletar, se apagar
                                                    // um tema, todas as postagens dele serão apagadas, não haverão orfãos

            _ = modelBuilder.Entity<Postagem>()
                .HasOne(_ => _.Usuario) 
                .WithMany(u => u.Postagem)  
                .HasForeignKey("UsuarioId")
                .OnDelete(DeleteBehavior.Cascade);
        }

        // Registrar DbSet - Objeto responsável por manipular a tabela
        public DbSet<Postagem> Postagens { get; set; } = null!;
        public DbSet<Tema> Temas { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                                .Where(x => x.State == EntityState.Added)
                                .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                //Se uma propriedade da Classe Auditable estiver sendo criada. 
                if (insertedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                        .Where(x => x.State == EntityState.Modified)
                        .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                //Se uma propriedade da Classe Auditable estiver sendo atualizada.  
                if (modifiedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
            protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
            {
            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetConverter>();
            }
        }
    }

