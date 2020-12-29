using iToons.Data;
using iToons.Dependencies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace iToons
{
    public class Program
    {
        public static IData Data { get; private set; }
        public static IMusic Music { get; set; }

        public static void Main(string[] args)
        {
            // custom dependency injection & sql data connection 
            // performing migrations on startup
            Data = new IData(new IDataSqlite());
            Data.MigrateDatabase();
            Music = new IMusic(new IMusicFileDirectory());      
            Task.Run(() =>  { Music.GenerateMusicData();  });
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
