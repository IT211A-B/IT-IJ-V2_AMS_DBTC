using System.Text.Json.Serialization;

namespace AMS_Backend_V2.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sex
    {
        Male,
        Female
    }

    public enum Status
    {
        Present,
        Late,
        Absent
    }
}
