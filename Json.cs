using System.IO;
using System.Text.Json;
using System.Windows.Forms;

public static class Json
{
    public static ApiResponse DeserializeResponse(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<ApiResponse>(json);
        }
        catch (JsonException e)
        {
            // Handle or log the exception as needed
            return new ApiResponse { response = 0 };
        }
    }

    public static string SerializeToJson<T>(T obj)
    {
        // Create a memory stream to hold the serialized JSON
        using (MemoryStream stream = new MemoryStream())
        {
            // Create a JsonSerializer object
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true // If you want the JSON to be indented for readability
            };
            // Serialize the object to JSON and write it to the stream
            JsonSerializer.Serialize(stream, obj, options);

            // Reset the stream position to the beginning
            stream.Position = 0;

            // Read the serialized JSON from the stream
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

public class ApiResponse
{
    public Respostas response { get; set; }
}

public enum Respostas
{
    NComecado = 0,
    Comecado = 1, 
    Parou = 2
}
