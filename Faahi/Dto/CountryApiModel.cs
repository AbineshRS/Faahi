namespace Faahi.Dto
{
    public class CountryApiModel
    {
        public NameModel name { get; set; }
        public string cca2 { get; set; }
        public Dictionary<string, CurrencyModel> currencies { get; set; }
        public List<string> timezones { get; set; }
    }

    public class NameModel
    {
        public string common { get; set; }
    }

    public class CurrencyModel
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

}
