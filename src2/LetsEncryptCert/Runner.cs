using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LetsEncryptCert
{
    public class Runner
    {
        public async Task RunAsync()
        {
            RSA rsa = RSA.Create();
            RSAParameters rsaKeyInfo = rsa.ExportParameters(false);
            byte[] publicKey = rsa.ExportRSAPublicKey();
            //rsa.TryEncrypt()
            //rsaKeyInfo.

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://acme-staging-v02.api.letsencrypt.org/acme/");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Ninja Software Consulting Certificate Manager 0.1 using ACME v02");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US");

            

            try
            {
                var nonce = await GetNonceAsync(httpClient);
                Console.WriteLine($"Nonce: {nonce}");

                //await CreateAccountAsync(httpClient);
                //await SubmitOrderAsync(httpClient);
                //await FetchChallengesAsync(httpClient);
                //await RespondToChallengesAsync(httpClient);
                //await PollForStatusAsync(httpClient);
                //await FinalizeOrderAsync(httpClient);
                //await PollForStatusAsync(httpClient);
                //await DownloadCertificateAsync(httpClient);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                httpClient.Dispose();
            }
        }

        private async Task<string> GetNonceAsync(HttpClient httpClient)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, "new-nonce"));

            if(!response.IsSuccessStatusCode)
                throw new Exception("Failed to get nonce");

            if(!response.Headers.TryGetValues("Replay-Nonce", out IEnumerable<string> values))
                throw new Exception("No Replay-Nonce header was present");

            if(values == null)
                throw new Exception("No value found in the Replay-Nonce header");

            string nonce = values.SingleOrDefault();

            if(string.IsNullOrEmpty(nonce))
                throw new Exception("Invalid nonce");

            return nonce;
        }

        private async Task CreateAccountAsync(HttpClient httpClient, string nonce)
        {
            // JsonWebSignature jws = new JsonWebSignature();
            // jws.jwk = "";
            // jws.url = httpClient.BaseAddress.ToString() + AcmeMethod.NewAccount.Name;
            // jws.nonce = nonce;

            var protectedObject = new {
                    alg = "ES256",
                    jwk = new {
                        alg = "",
                        crv = "",
                        x = "",
                        y = "",
                        use = "",
                        kid = ""
                    },
                    nonce = nonce,
                    url = "https://acme-staging-v02.api.letsencrypt.org/acme/new-acct"
                };
            var protected64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(protectedObject)));

            var payloadObject = new {
                    termsOfServiceAgreed = true,
                    contact = new []{"mailto:email@ninjasoftwareconsulting.com"}
                };
            var payload64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payloadObject)));

            var createAccountRequest = new {
                Protected = po64,
                payload = payload64,
                signature = ""
            };
            var carJson = JsonConvert.SerializeObject(createAccountRequest);


            // var car = new CreateAccountRequest();
            // car.contact.Add("mailto:email@ninjasoftwareconsulting.com");
            // car.termsOfServiceAgreed = true;
            // var carJson = JsonConvert.SerializeObject(car);

            // var acmeRequest = new {
            //     payload = new {
            //         alg = "RSA256",
                    
            //     }
            // };

            var response = await httpClient.PostAsync("new-acct", new StringContent(carJson, Encoding.UTF8, "application/jose+json"));
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        // private async Task SubmitOrderAsync(HttpClient httpClient)
        // {
        //     JsonWebSignature2 jws = new JsonWebSignature2();
        //     string json = "{}";
        //     var response = await httpClient.PostAsync("new-order", new StringContent(json, Encoding.UTF8, "application/json"));
        //     var responseContent = await response.Content.ReadAsStringAsync();
        // }

        // private async Task FetchChallengesAsync(HttpClient httpClient)
        // {
        //     JsonWebSignature2 jws = new JsonWebSignature2();
        //     string json = "{}";
        //     var response = await httpClient.PostAsync("???", new StringContent(json, Encoding.UTF8, "application/json"));
        //     var responseContent = await response.Content.ReadAsStringAsync();
        // }

        // private async Task RespondToChallengesAsync(HttpClient httpClient)
        // {
        //     JsonWebSignature2 jws = new JsonWebSignature2();
        //     string json = "{}";
        //     var response = await httpClient.PostAsync("???", new StringContent(json, Encoding.UTF8, "application/json"));
        //     var responseContent = await response.Content.ReadAsStringAsync();
        // }

        // private async Task PollForStatusAsync(HttpClient httpClient)
        // {
        //     JsonWebSignature2 jws = new JsonWebSignature2();
        //     string json = "{}";
        //     var response = await httpClient.PostAsync("???", new StringContent(json, Encoding.UTF8, "application/json"));
        //     var responseContent = await response.Content.ReadAsStringAsync();
        // }

        // private async Task FinalizeOrderAsync(HttpClient httpClient)
        // {
        //     JsonWebSignature2 jws = new JsonWebSignature2();
        //     string json = "{}";
        //     var response = await httpClient.PostAsync("???", new StringContent(json, Encoding.UTF8, "application/json"));
        //     var responseContent = await response.Content.ReadAsStringAsync();
        // }

        // private async Task DownloadCertificateAsync(HttpClient httpClient)
        // {
        //     JsonWebSignature2 jws = new JsonWebSignature2();
        //     string json = "{}";
        //     var response = await httpClient.PostAsync("???", new StringContent(json, Encoding.UTF8, "application/json"));
        //     var responseContent = await response.Content.ReadAsStringAsync();
        // }
    }

    

    public class JsonWebSignature
    {
        public string alg => "RSA256";
        public string nonce {get;set;}
        public string url {get;set;}
        public string jwk {get;set;}

        public string kid {get;set;}
    }
}