using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data
{
    public class SqlClientConnectionBD : ISqlClientConnectionBD
    {
        private readonly IConfiguration _configuration;
        public string CadenaConexion { get; set; } = null;

        public SqlClientConnectionBD(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            CadenaConexion = _configuration.GetConnectionString("DefaultConnection");
            return CadenaConexion;
        }
    }
}
