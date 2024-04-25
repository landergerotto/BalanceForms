using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class HttpRequester
{
    private readonly HttpClient _client;
    private readonly string _baseUri;

    public HttpRequester(string baseUri)
    {
        _client = new HttpClient();
        _baseUri = baseUri;
    }

    public async Task<string> GetAsync(string endpoint)
    {
        try
        {
            // MessageBox.Show(_baseUri + endpoint);
            HttpResponseMessage response = await _client.GetAsync(_baseUri + endpoint);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
            // MessageBox.Show("Response from server:\n" + a);
            // return a;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> PostAsync(string endpoint, string jsonContent)
    {
        try
        {
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUri + endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
