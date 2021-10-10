using DotNetAcmeClient.Logic;
using System;
using System.Threading.Tasks;

namespace DotNetAcmeClient
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var configuration = new AcmeRequestConfiguration("Ninja Software Consulting Certifice Manager 0.1 using ACME v02");

            var r1 = await new AcmeCertificateRequest(configuration).GetNonceAsync();

            var nonce = await r1.DoSomethingAsync();

            Console.WriteLine(nonce);

            Console.ReadLine();
        }
    }
}
