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
