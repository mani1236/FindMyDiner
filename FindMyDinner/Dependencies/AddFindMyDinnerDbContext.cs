using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using FindMydinner.Domain.Model.Entities;
using Microsoft.Extensions.Configuration;
namespace FindMyDinner.Dependencies
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFindMyDinnerDbContext(this IServiceCollection @this)
        {
            var options = @this.BuildServiceProvider().GetService<DbContextOptions>();
            string ConnectionString = "Server=INBLRWIT241492\\SQLEXPRESS;Database=FindMyDinner;Trusted_Connection=True;";
            var MaxRetryCount = "";
            var MaxRetryDelay = "";
            var TransientErrors = "";
            @this.AddEntityFrameworkSqlServer();
            //@this.AddEntityFrameworkProxies(); need to check 
           @this.AddDbContext<FindMyDinnerContext>((provider, builder) =>{

               builder                     
                      .UseSqlServer(
                          options.ConnectionString,
                          context =>
                          {
                              context.EnableRetryOnFailure(
                                  options.MaxRetryCount,
                                  TimeSpan.FromMilliseconds(options.MaxRetryDelay),
                                  options.TransientErrors);
                          })
                         .UseInternalServiceProvider(provider)
                    .ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));

           });
            return @this;
        }
    }
}
