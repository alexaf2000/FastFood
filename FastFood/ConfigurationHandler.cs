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
        FileIniDataParser parser;
        IniData configuration;
        String configFile;

        //Constructor que define el fichero de configuración
        public ConfigurationHandler()
        {
            this.parser = new FileIniDataParser();
            this.configFile = "config.ini";
            this.configuration = parser.ReadFile(this.configFile);
        }
        //Metodo get que retornará el valor de el parámetro consultado
        public String getSetting(string Name, string Category) => this.configuration[Category][Name];

        //Metodo get que guardará el valor de el parámetro consultado
        public void setSetting(string Name, string Category, string value) {
            this.configuration[Category][Name] = value;
            this.parser.WriteFile(this.configFile, this.configuration);
               }
    }
}
