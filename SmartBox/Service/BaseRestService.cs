
using System.Text.Json;

namespace SmartBox.Service;

public class BaseRestService<T>	
{	
	private HttpClient HttpClient { get; }
	public BaseRestService(HttpClient httpClient)
	{		
		this.HttpClient = httpClient;
		this.DefaultHttpHeader();        
    }

	private void DefaultHttpHeader()
	{
		this.HttpClient.DefaultRequestHeaders.Accept.Clear();
		this.HttpClient.DefaultRequestHeaders.Accept.Add(
			new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
	}

	public void AddHttpHeader(string key, string value) =>
		this.HttpClient.DefaultRequestHeaders.Add(key, value);

	public async Task<T> GetAsync(string url) =>
		JsonSerializer.Deserialize<T>(await GetJsonAsync(url));

	protected Task<string[]> GetAsync(IEnumerable<string> urls) =>
		Task.WhenAll(urls.Select(url => GetJsonAsync(url)));
	
	public async Task<string> GetJsonAsync(string url)
	{
		var response = await this.HttpClient.GetAsync(url);
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadAsStringAsync();
	}

	//Put za spremembo jezika, interval merjenja, enote
}
