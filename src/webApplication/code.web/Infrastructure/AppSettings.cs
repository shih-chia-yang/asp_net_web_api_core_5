namespace code.web.Infrastructure
{
    public class AppSettings
    {
        public ServiceUrls[] Servers { get; set; }
    }

    public class ServiceUrls
    {
        public string Name{ get; set; }
        public string Url { get; set; }
        public int Version{ get; set; }
    }
}