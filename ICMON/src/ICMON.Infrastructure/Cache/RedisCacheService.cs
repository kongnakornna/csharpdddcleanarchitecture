using StackExchange.Redis;
using System.Text.Json;

namespace ICMON.Infrastructure.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value!, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value, _jsonOptions);
        await _database.StringSetAsync(key, json, expiry);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => await _database.KeyDeleteAsync(key);

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        => await _database.KeyExistsAsync(key);
}
