using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;

namespace TfsConvenience
{
    public static class Connection 
    {
        public static TfsTeamProjectCollection Connect(ConnectionParameters parameters)
        {
           return new TfsTeamProjectCollection(new Uri(parameters.CollectionUri), GetCredentials(parameters));
        }
        
        private static ICredentials GetCredentials(ConnectionParameters parameters)
        {
            ICredentials credentials = null;
            
            if (parameters.UseNetworkCredentials)
            {
                credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else if (!string.IsNullOrEmpty(parameters.UserName) && !string.IsNullOrEmpty(parameters.Password))
            {
                var parts = parameters.UserName.Split('\\');
                credentials = parts.Length > 1
                    ? new NetworkCredential(parts[1], parameters.Password, parts[0])
                    : new NetworkCredential(parameters.UserName, parameters.Password);
            }
            
            return credentials;
        }
        
        public static Task<bool> Test(ConnectionParameters parameters)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var tfs = Connect(parameters))
                    {
                        tfs.Authenticate();
                        if (tfs.HasAuthenticated)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}
