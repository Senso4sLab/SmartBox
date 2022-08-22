
using MonkeyCache;
using System.Text.Json;
using System.Linq;

namespace SmartBox.Service;

public class ChachedRestService<T> : BaseRestService<T>	
{
    private IBarrel Barrel { get; }
    public TimeSpan ExpiredInterval =>
        TimeSpan.FromSeconds(60);
    public ChachedRestService(IBarrel barrel, HttpClient httpClient) : base(httpClient)
	{
		this.Barrel = barrel;
    }
	public async Task<IEnumerable<T>> GetAllAsync(string relativeUrl) 		
    {
		var active = GetActiveAsync().ToList();
		var result = await GetExpiredAsync(relativeUrl);
		active.AddRange(result);		
    	return active;
    }
	public async Task<T> GetAsync(string key, string relativeUrl, TimeSpan duration)
    {
        var json = await base.GetJsonAsync($"{relativeUrl}+{key}");
        this.Barrel.Add(key, json, duration);
        return JsonSerializer.Deserialize<T>(json);
    }
	private Task<T[]> GetExpiredAsync(string relativeUrl) =>
		Task.WhenAll(GetExpired(relativeUrl));
    private IEnumerable<Task<T>> GetExpired(string relativeUrl) =>
		this.ReadFromDbBy(CacheState.Expired)
			.Select(tuple => GetAsync(tuple.key, relativeUrl, this.ExpiredInterval))
			.DefaultIfEmpty();
	private IEnumerable<T> GetActiveAsync() =>
		this.ReadFromDbBy(CacheState.Active)
			.Select(tuple => tuple.item)
			.DefaultIfEmpty<T>();
			
	private T ReadFromDbBy(string key) =>
		this.Barrel.Get<T>(key);	
	private IEnumerable<(string key, T item)> ReadFromDbBy(CacheState state) =>
		this.Barrel
			.GetKeys(state)
			.Select(key => (key:key, item: ReadFromDbBy(key)))
			.DefaultIfEmpty<(string, T)>();
			
}
