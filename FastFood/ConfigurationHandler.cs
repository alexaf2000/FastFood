using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IniParser;
using IniParser.Model;

namespace FastFood
{
    class ConfigurationHandler
    {
        IniData configuration;

        //Constructor que define el fichero de configuración
        public ConfigurationHandler()
        {
            FileIniDataParser parser = new FileIniDataParser();
            this.configuration = parser.ReadFile("config.ini");
        }
        //Metodo get que retornará el valor de el parámetro consultado
        public String getSetting(string Name, string Category) => this.configuration[Category][Name];
    }
}
