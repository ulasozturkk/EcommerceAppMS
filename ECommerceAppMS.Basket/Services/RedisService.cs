using StackExchange.Redis;

namespace ECommerceAppMS.Basket.Services {
  public class RedisService {

    private readonly string _host;
    private readonly int _port;

    private ConnectionMultiplexer connectionMultiplexer;

    public RedisService(string host, int port) {
      _host = host;
      _port = port;
    }

    public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
    public IDatabase GetDatabase (int db = 1) => connectionMultiplexer.GetDatabase(db);


  }
}
