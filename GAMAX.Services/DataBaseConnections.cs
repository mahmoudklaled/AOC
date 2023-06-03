using Secuirty;

namespace GAMAX.Services
{
    public class DataBaseConnections
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string encryptionKey = "P@ssw0rd";
        public DataBaseConnections(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public string GetDbConnectionString()
        {
            var aes   = new AES_Security();
            return aes.Decrypt(_connectionString);
        }
    }
}
