using System;
using System.Collections.Generic;

namespace LetsEncryptCert
{
    public sealed class AcmeMethod
    {
        private AcmeMethod(string methodName)
        {
            Name = methodName;
        }

        public string Name {get;}

        public static bool Validate(string methodName)
        {
            if(_methodNames.Contains(methodName))
                return true;

            throw new Exception("Invalid method name");
        }

        public static AcmeMethod NewNonce = new AcmeMethod(NewNonceName);
        public static AcmeMethod NewAccount = new AcmeMethod(NewAccountName);
        public static AcmeMethod RevokeCertification = new AcmeMethod(RevokeCertificationName);
        public static AcmeMethod NewOrder = new AcmeMethod(NewOrderName);

        public const string NewNonceName = "new-nonce";
        public const string NewAccountName = "new-acct";
        public const string RevokeCertificationName = "revoke-cert";
        public const string NewOrderName = "new-order";

        private static List<string> _methodNames = new List<string>
            {
                NewNonceName, 
                NewAccountName, 
                RevokeCertificationName, 
                NewOrderName
            }; 
    }
}