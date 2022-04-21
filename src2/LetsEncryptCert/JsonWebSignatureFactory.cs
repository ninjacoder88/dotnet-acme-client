namespace LetsEncryptCert
{
    public class JsonWebSignatureFactory
    {  
        public JsonWebSignatureFactory(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public JsonWebSignature Create(AcmeMethod acmeMethod, string nonce)
        {
            JsonWebSignature jws = new JsonWebSignature();
            jws.nonce = nonce;
            jws.url = _baseAddress + acmeMethod.Name;

            switch (acmeMethod.Name)
            {
                case AcmeMethod.NewNonceName:
                case AcmeMethod.RevokeCertificationName:
                    jws.jwk = "";
                    break;
                default:
                    jws.kid = "";
                    break;
            }

            return jws;
        }

        private readonly string _baseAddress;
    }
}