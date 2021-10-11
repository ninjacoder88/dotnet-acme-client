using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAcmeClient.Logic
{
    public class NonceReceived
    {
        internal NonceReceived(string nonce, HttpClient httpClient)
        {
            _nonce = nonce;
            _httpClient = httpClient;
        }

        public async Task<string> DoSomethingAsync()
        {
            await Task.CompletedTask;
            _httpClient.Dispose();
            return _nonce;
        }

        public async Task CreateAccountAsync()
        {
            //RSA rsa = RSA.Create();
            //Request request;
            string jws;
            byte[] signedHash;
            using (var rsa = RSA.Create())
            {
                var header = new 
                {
                    alg = "RS256",
                    nonce = _nonce,
                    url = _httpClient.BaseAddress.ToString() + "new-acct",
                    jwk = new { kty = "RS256", k = rsa.ExportRSAPublicKey() }//public key used to sign JWS?
                };
                string jsonHeader = JsonConvert.SerializeObject(header);
                string base64Header = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonHeader)).Replace("=", string.Empty);

                var payload = new
                {
                    contact = new List<string> { "mailto:email@ninjasoftwareconsulting.com"},
                    termsOfServiceAgreed = true
                };
                string jsonPayload = JsonConvert.SerializeObject(payload);
                string base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload)).Replace("=", string.Empty);

                string combine = $"{base64Header}.{base64Payload}";
                byte[] jwsSigningInput = Encoding.UTF8.GetBytes(combine);


            
                byte[] hash;
                using(var sha256 = SHA256.Create())
                {
                    hash = sha256.ComputeHash(jwsSigningInput);
                }

                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(rsa);
                RSAFormatter.SetHashAlgorithm("SHA256");

                signedHash = RSAFormatter.CreateSignature(hash);

                jws = "{\"payload\":\"" + jsonPayload + "\",\"protected\":\"" + jsonHeader + "\",\"signature\":\"" + Encoding.UTF8.GetString(signedHash) + "\"}";
            
                //request = new Request { Protected = jsonHeader, Payload = payload, Signature = Encoding.UTF8.GetString(signedHash) };
            }

            //string json = JsonConvert.SerializeObject(request);

            var str = new StringContent(jws);
            str.Headers.Clear();
            str.Headers.ContentType = new MediaTypeHeaderValue("application/jose+json");


            var response = await _httpClient.PostAsync("new-acct", str);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseContent);
            Console.WriteLine(responseContent);
        }

        public async Task Post()
        {
            var protectedHeader = new
            {
                alg = "RSA256",
                nonce = _nonce,
                url = _httpClient.BaseAddress.ToString() + "new-acct",
                kid = ""//existing account? account url received by posting to new account
            };

            var payload = new
            {
                contact = new List<string> { "mailto:email@ninjasoftwareconsulting.com" },
                termsOfServiceAgreed = true
            };

            var response = await _httpClient.PostAsync("", new StringContent("", Encoding.UTF8, "application/jose+json"));
        }

        public async Task PostAsGet()
        {
            var protectedHeader = new
            {
                alg = "RSA256",
                nonce = _nonce,
                url = _httpClient.BaseAddress.ToString() + "new-acct",
                kid = ""//existing account? account url received by posting to new account
            };

            var payload = new
            {
                
            };

            var response = await _httpClient.PostAsync("", new StringContent("", Encoding.UTF8, "application/jose+json"));
        }

        private readonly string _nonce;
        private readonly HttpClient _httpClient;
    }

    public class Request
    {
        public object Protected { get; set; }

        public object Payload { get; set; }

        public string Signature { get; set; }
    }
}
