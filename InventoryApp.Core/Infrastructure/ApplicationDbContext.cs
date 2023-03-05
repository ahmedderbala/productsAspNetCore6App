using InventoryApp.Core.Entities;
using InventoryApp.Core.Extensions;
using InventoryApp.Core.Entities;
using InventoryApp.Core.Extensions;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

using System.Data.Common;
using System.Reflection;
using static Humanizer.In;
using System.Data.Entity;

namespace InventoryApp.Core.Infrastructure
{
    public class ApplicationDbContext : DexefAccountingContext
    {
        //public ApplicationDbContext()
        //{
        //}

        //public ApplicationDbContext(DbConnection connection)
        //{
        //    // this.Database.SetDbConnection(connection);
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MyInMemoryDatabase");
            //optionsBuilder.AddInterceptors(new AuditLogInterceptor());
            //optionsBuilder.UseLoggerFactory()
            optionsBuilder.UseExceptionProcessor();
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var jsonvalueMethodInfo = typeof(Json).GetRuntimeMethod(nameof(Json.Value), new[] { typeof(string), typeof(string) });
            var translatevalueMethodInfo = typeof(Translate).GetRuntimeMethod(nameof(Translate.Value), new[] { typeof(int), typeof(string), typeof(int) });

            modelBuilder.HasDbFunction(jsonvalueMethodInfo).HasTranslation(args => SqlFunctionExpression.Create("JSON_VALUE", args, typeof(string), null));
            modelBuilder.HasDbFunction(translatevalueMethodInfo).HasTranslation(args => SqlFunctionExpression.Create("dbo.GetValueTranslate", args, typeof(string), null));


            #region Ignore Dynamic Variables for not existing fields

            //modelBuilder.Entity<CountryCurrency>().Ignore(f => f.TranslationHeaderId);


            #endregion Ignore Dynamic Variables for not existing fields
            #region Apply Global Filters

            //if (!Database.GetDbConnection().GetType().Name.StartsWith("Effort", StringComparison.Ordinal))
            //{
            //TODO:UnComment this
            //modelBuilder.ApplyGlobalFilters<IEntityBase>(e => e.Inactive == false, "Inactive");
            //modelBuilder.ApplyGlobalFilters<IEntityBase>(e => e.IsDeleted == false, "IsDeleted");
            //}

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Commit()
        {
            SaveChanges();
        }

        public virtual async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}