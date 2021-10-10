namespace DotNetAcmeClient.Logic
{
    public class AcmeRequestConfiguration
    {
        public AcmeRequestConfiguration(string userAgent)
        {
            UserAgent = userAgent;
            AcmeServerUrl = "https://acme-staging-v02.api.letsencrypt.org/acme/";
            AcceptLanguage = "en-US";
        }

        public string AcmeServerUrl { get; set; }

        public string UserAgent { get; }

        public string AcceptLanguage { get; set; }
    }
}
