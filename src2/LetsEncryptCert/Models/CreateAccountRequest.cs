using System.Collections.Generic;

namespace LetsEncryptCert
{
    public class CreateAccountRequest
    {
        public CreateAccountRequest()
        {
            contact = new List<string>();
        }

        public List<string> contact {get;set;}

        public bool termsOfServiceAgreed {get;set;}
    }
}