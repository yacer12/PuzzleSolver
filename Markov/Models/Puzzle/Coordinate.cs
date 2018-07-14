using Newtonsoft.Json;

public class Coordinate
{
    [JsonProperty("Row")]
    public int X { get; set; }
    [JsonProperty("Row")]
    public int Y { get; set; }
}