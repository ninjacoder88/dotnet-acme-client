using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using System.Security.Cryptography;

namespace LetsEncryptCert
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string header = "{\"typ\":\"JWT\",\r\n \"alg\":\"HS256\"}";
            var base64Header = Convert.ToBase64String(Encoding.UTF8.GetBytes(header)).Replace("=", string.Empty);
            Console.WriteLine($"Header: {base64Header}");

            string payload = "{\"iss\":\"joe\",\r\n \"exp\":1300819380,\r\n \"http://example.com/is_root\":true}";
            var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload)).Replace("=", string.Empty);
            Console.WriteLine($"Payload: {base64Payload}");

            var concatHeaderPayload = $"{base64Header}.{base64Payload}";
            Console.WriteLine($"Payload + Header: {concatHeaderPayload}");

            byte[] jwsSigningInput = Encoding.UTF8.GetBytes(concatHeaderPayload);
            //PrintOctet(jwsSigningInput);

            string key = "{\"kty\":\"oct\",\r\n \"k\":\"AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow\"\r\n}";
            char[] chars = key.ToCharArray();
            byte[] bytes = new byte[chars.Length];
            for(int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }
            using(var hmac = new HMACSHA256(bytes))
            {
                var result = hmac.ComputeHash(jwsSigningInput);
                PrintOctet(result);
            }


            
            //string key1 = "{\"kty\":\"oct\",\r\n \"k\":\"AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow\"\r\n}";
            //string key2 = "{\"kty\":\"oct\",\r\n\"k\":\"AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow\"\r\n}";
            //string key3 = "{\"kty\":\"oct\",\"k\":\"AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow\"}";
            //string key4 = "{\"kty\":\"oct\",\r\n \"k\":\"AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow\"\r\n}";
            //string key5 = "{\"kty\":\"oct\",\r\n \"k\":\"AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow\"\r\n}";
            //string base64Key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key1)).Replace("=", string.Empty);

            // byte[] keyBytes = Encoding.UTF8.GetBytes(key1);
            // PrintOctet(keyBytes);

            // using(var hmac = new HMACSHA256(keyBytes))
            // {
            //     byte[] result = hmac.ComputeHash(jwsSigningInput);
            //     PrintOctet(result);
            // }
        }

        private static void PrintOctet(byte[] r)
        {
            foreach(var b in r)
            {
                Console.Write((int)(char)b + " ");
            }
            Console.WriteLine();
        }
    }
}
