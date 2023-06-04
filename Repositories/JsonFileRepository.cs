using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Repositories;

internal class JsonFileRepository<TypeToStore> : IInformationRepository<TypeToStore> where TypeToStore : class
{
    private string jsonFilePath;
    public IList<TypeToStore> Store { get; set; } = new List<TypeToStore>();

    public JsonFileRepository()
    {
        var directoryPath = Path.Join("Agents", ApplicationInformation.CurrentAgentSymbol, "Repositories");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        var genericName = typeof(TypeToStore).Name;
        this.jsonFilePath = Path.Join(directoryPath, $"{genericName}.json");
        if (!File.Exists(this.jsonFilePath))
        {
            File.WriteAllText(this.jsonFilePath, JsonConvert.SerializeObject(this.Store));
            this.SaveInformation();
        }

        this.Store = this.ReadFromFile();
    }

    public void SaveInformation(params TypeToStore[] elements)
    {
        if (elements is not null)
        {
            foreach (var element in elements)
            {
                this.Store.Add(element);
            }
        }
        var contentsToWriteToFile = JsonConvert.SerializeObject(this.Store, Formatting.Indented);
        File.WriteAllText(this.jsonFilePath, contentsToWriteToFile);
    }

    private IList<TypeToStore> ReadFromFile()
    {
        var content = File.ReadAllText(this.jsonFilePath);
        return JsonConvert.DeserializeObject<IList<TypeToStore>>(content, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    }
}
