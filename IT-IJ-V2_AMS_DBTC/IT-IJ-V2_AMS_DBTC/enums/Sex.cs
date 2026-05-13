using System.Text.Json.Serialization;
namespace IT_IJ_V2_AMS_DBTC.enums
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
