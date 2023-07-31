using System.Collections.Concurrent;

namespace RoutingAssignment
{
    public class CountryCollection
    {
        public ConcurrentDictionary<int,string> Countries { get; set; }= new ConcurrentDictionary<int, string>();
        public CountryCollection()
        {
            Countries.TryAdd(1, "United States");
            Countries.TryAdd(2, "Canada");
            Countries.TryAdd(3, "United Kingdom");
            Countries.TryAdd(4, "India");
            Countries.TryAdd(5, "Japan");
        }
    }
}
