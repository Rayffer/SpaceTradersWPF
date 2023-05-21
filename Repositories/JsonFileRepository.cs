using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Repositories
{
    internal class JsonFileRepository<TypeToStore> : IInformationRepository<TypeToStore> where TypeToStore : class
    {
        private string jsonFilePath;
        public IList<TypeToStore> Store { get; set; } = new List<TypeToStore>();

        public JsonFileRepository()
        {
            var directoryPath = Path.Join("Repositories");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var genericName = typeof(TypeToStore).Name;
            this.jsonFilePath = Path.Join(directoryPath, $"{genericName}.json");
            if (!File.Exists(this.jsonFilePath))
            {
                File.WriteAllText(this.jsonFilePath, "");
                this.SaveInformation();
            }

            this.Store = this.ReadFromFile();
        }

        public void SaveInformation(params TypeToStore[] elements)
        {
            TextWriter writer = null;
            try
            {
                if (elements is not null)
                {
                    foreach (var element in elements)
                    {
                        this.Store.Add(element);
                    }
                }
                var contentsToWriteToFile = JsonConvert.SerializeObject(this.Store, Formatting.Indented);
                writer = new StreamWriter(this.jsonFilePath, false);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                writer?.Close();
            }
        }

        private IList<TypeToStore> ReadFromFile()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(this.jsonFilePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<IList<TypeToStore>>(fileContents, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
