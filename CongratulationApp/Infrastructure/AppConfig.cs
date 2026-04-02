using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CongratulationApp.Infrastructure
{
    // Objective form of appsettings.json config
    public class AppConfig
    {
        public TinyMCE tinyMCE { get; set; } = new TinyMCE();
        public Company company { get; set; } = new Company();
        public Database Database { get; set; } = new Database();
    }

    //Objective database configuration
    public class Database 
    {
        public string? ConnectionString { get; set; }
    }

    //string? means that the reference type variable can be null
    public class TinyMCE
    {
        public string? APIKey { get; set; }
    }

    public class Company
    {
        public string? CompanyName { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyShortPhone { get; set; }
        public string? CompanyEmail { get; set; }
    }
}
