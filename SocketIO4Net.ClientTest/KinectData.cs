using Newtonsoft.Json;

namespace SocketIO4Net.ClientTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class KinectData
    {

        // List other Kinect data with [JsonProperty]
        [JsonProperty]
        public string Foo { get; set; }

        [JsonProperty]
        public string Bar { get; set; }


        public KinectData()
        {
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static KinectData Deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<KinectData>(jsonString);
        }
    }

}
