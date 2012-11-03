namespace TfsConvenience
{
    public class ConnectionParameters
    {
        public string CollectionUri { get; set; }
        public bool UseNetworkCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool SkipTest { get; set; }
    }
}