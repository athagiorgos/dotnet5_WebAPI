using System.Text.Json.Serialization;

namespace dotnet5_WebAPI.Models
{

    // This is to avoid showing indexes of the enum class and show the exact type
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}