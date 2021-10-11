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

            await r1.CreateAccountAsync();

            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
