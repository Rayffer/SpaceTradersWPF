using Newtonsoft.Json;

namespace SpaceTradersWPF.ClassExtensions;

internal static class ObjectCloner
{
    public static T Clone<T>(this T objectToClone) where T : class
    {
        var serializedMonster = JsonConvert.SerializeObject(objectToClone, Formatting.None);
        return JsonConvert.DeserializeObject<T>(serializedMonster);
    }
}
