using System.Net.Http;
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

        private readonly string _nonce;
        private readonly HttpClient _httpClient;
    }
}
