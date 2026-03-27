using System.Text.Json;
using Engine.Core.Enums;

namespace Engine.Core.Config;

public class ConfigDeserializer
{
    private const string JsonEmptyErrorMessage = 
        "Something went wrong trying to read the EntityConfig.json";

    private const string ParsingJsonErrorMessage =
        "Something went wrong parsing EntityConfig.json to EntityConfig Objects";

    public EntityConfig GetEntityConfig(EntityType entityType)
    {
        string fileName = Path.Combine("Config", "EntityConfig.json");
        string jsonString = File.ReadAllText(fileName);

        if (string.IsNullOrEmpty(jsonString))
        {
            throw new ArgumentNullException(JsonEmptyErrorMessage);
        }

        var config = JsonSerializer.Deserialize<Dictionary<string, EntityConfig>>(jsonString);

        if (config == null) throw new ArgumentNullException(ParsingJsonErrorMessage);
            
        return config[entityType.ToString()];
    }
}