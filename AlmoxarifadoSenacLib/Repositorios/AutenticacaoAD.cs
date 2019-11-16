using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace AlmoxarifadoSenacLib.Repositorios
{
    public class AutenticacaoAD
    {
        public struct Credentials
        {
            public string Username;
            public string Password;
        }



        public class Domain_Authentication
        {
            public Credentials Credentials;
            public string Domain;
            public bool verificar;


            public Domain_Authentication(string Username, string Password, string SDomain)
            {
                Credentials.Username = Username;
                Credentials.Password = Password;
                Domain = SDomain;
             
            }
            public bool IsValid()
            {
                try
                {
                    using (
                        PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain))
                    {
                        // validate the credentials
                        return pc.ValidateCredentials(Credentials.Username, Credentials.Password);

                    }
                }
                catch(Exception e)
                {
                    return false;
                }
            }
           
        }
    }
}

