using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetAcmeClient.Logic
{
    public class AcmeCertificateRequest
    {
        public AcmeCertificateRequest(AcmeRequestConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<NonceReceived> GetNonceAsync()
        {
            var httpClient = CreateHttpClient();

            try
            {
                var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, "new-nonce"));

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Failed to get nonce");

                if (!response.Headers.TryGetValues("Replay-Nonce", out IEnumerable<string> values))
                    throw new Exception("No Replay-Nonce header was present");

                if (values == null)
                    throw new Exception("No value found in the Replay-Nonce header");

                string nonce = values.SingleOrDefault();

                if (string.IsNullOrEmpty(nonce))
                    throw new Exception("Invalid nonce");

                return new NonceReceived(nonce, httpClient);
            }
            catch
            {
                httpClient.Dispose();
                throw;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.AcmeServerUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "Ninja Software Consulting Certifice Manager 0.1 using ACME v02");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            return client;
        }

        private readonly AcmeRequestConfiguration _configuration;
    }
}
