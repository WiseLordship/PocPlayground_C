using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Poc.ConfigParser
{
    public class ConfigParser
    {
        public XmlDocument GetConfigDocument(string fileLocation)
        {
            if (!File.Exists(fileLocation)) throw new FileNotFoundException();

            using (var reader = new StreamReader(
                new FileStream(
                    fileLocation,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read)
            ))
            {
                var doc = new XmlDocument();
                var xmlIn = reader.ReadToEnd();
                reader.Close();
                doc.LoadXml(xmlIn);
                return doc;
            }
        }

        public Dictionary<string, string> GetAppSettings(XmlDocument file)
        {
            var dictionary = new Dictionary<string, string>();
            var configurationNode = file.SelectSingleNode("/configuration");
            foreach (XmlNode child in configurationNode.ChildNodes)
                if (child.Name.Equals("appSettings"))
                    foreach (XmlNode node in child.ChildNodes)
                        if (node.Name.Equals("add"))
                            dictionary.Add
                            (
                                node.Attributes["key"].Value,
                                node.Attributes["value"].Value
                            );
            return dictionary;
        }


        public Dictionary<string, string> GetConnectionSettings(XmlDocument file)
        {
            var dictionary = new Dictionary<string, string>();
            var configurationNode = file.SelectSingleNode("/configuration");
            foreach (XmlNode child in configurationNode.ChildNodes)
                if (child.Name.Equals("connectionStrings"))
                    foreach (XmlNode node in child.ChildNodes)
                        if (node.Name.Equals("add"))
                            dictionary.Add
                            (
                                node.Attributes["name"].Value,
                                node.Attributes["connectionString"].Value
                            );
            return dictionary;
        }

        public XmlNode GetConfigSecion(XmlDocument file)
        {
            var configurationNode = file.SelectSingleNode("/configuration");
            foreach (XmlNode child in configurationNode.ChildNodes)
                if (child.Name.Equals("watchGuard.config"))
                    return child;
            return null;
        }
    }
}
