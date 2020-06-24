using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace FlightMobileApp.Model.Concrets_Objects
{
    /*
 * The class is representing one Command.
 */
    public class Command
    {
        [JsonPropertyName("aileron")]
        [JsonProperty("aileron")]
        [Range(-1, 1)]
        public double Aileron { get; set; }

        [JsonPropertyName("rudder")]
        [JsonProperty("rudder")]
        [Range(-1, 1)]
        public double Rudder { get; set; }

        [JsonPropertyName("elevator")]
        [JsonProperty("elevator")]
        [Range(-1, 1)]
        public double Elevator { get; set; }

        [JsonPropertyName("throttle")]
        [JsonProperty("throttle")]
        [Range(0.0, 1.0)]
        public double Throttle { get; set; }

        [JsonConstructor]
        public Command(double aileron, double rudder, double elevator, double throttle)
        {
            Aileron = aileron;
            Rudder = rudder;
            Elevator = elevator;
            Throttle = throttle;
        }

        public Command()
        {
        }


    }
}