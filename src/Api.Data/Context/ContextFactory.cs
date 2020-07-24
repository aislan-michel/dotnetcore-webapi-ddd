using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
     public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
     {
          public MyContext CreateDbContext(string[] args)
          {
               //usado para criar migrações
               var connectionString = "Server=localhost;Port=3307;Database=dbAPI;Uid=root;Pwd=123456";
               var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
               optionsBuilder.UseMySql(connectionString);

               return new MyContext(optionsBuilder.Options);
          }
     }
}
