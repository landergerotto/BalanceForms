using System.Text.Json;
using System.Windows.Forms;

public static class Json
{
    public static object DeserializeResponse(string json)
    {
        try
        {
            var a = JsonSerializer.Deserialize<ApiResponse>(json);
            MessageBox.Show(a.Response.ToString());
            return JsonSerializer.Deserialize<object>(json);
        }
        catch (JsonException e)
        {
            // Handle or log the exception as needed
            return new object {  };
        }
    }
}

public class ApiResponse
{
    public object Response { get; set; } = new object();
}
