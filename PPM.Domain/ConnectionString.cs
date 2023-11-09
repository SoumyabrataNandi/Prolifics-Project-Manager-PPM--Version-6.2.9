using Microsoft.Extensions.Configuration;

namespace PPM.Domain
{
    public class ConnectionString
    {
        public string Connection()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            var ConFiguration = builder.AddJsonFile("AppConnectionOfDataBase.json").Build();
            string? connectionString = ConFiguration["ConnectionString:ConnectionDateBase"];
            return connectionString!;
        }
    }
}