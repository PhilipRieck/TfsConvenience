using System;
using System.Net;
using Microsoft.TeamFoundation.Client;

namespace TfsConvenience
{
    class Connection : IConnection
    {
        private ConnectionParameters _parameters;
        private readonly IConnectionParameterProvider _parameterProvider;

        private bool _isValid;
        public bool IsValid {
            get { return _isValid || Parameters.SkipTest; }
            set { _isValid = value; }
        }

        private ICredentials _credentials;

        private ConnectionParameters Parameters
        {
            get { return _parameters ?? (_parameters = _parameterProvider.GetParameters()); }
        }

        public Connection(IConnectionParameterProvider parameterProvider)
        {
            _parameterProvider = parameterProvider;

        }

        public TfsTeamProjectCollection Connect()
        {
           return new TfsTeamProjectCollection(new Uri(Parameters.CollectionUri), GetCredentials());
        }

        private ICredentials GetCredentials()
        {
            if (_credentials == null)
            {
                if (Parameters.UseNetworkCredentials)
                {
                    _credentials = CredentialCache.DefaultNetworkCredentials;
                }
                else if (!string.IsNullOrEmpty(Parameters.UserName) && !string.IsNullOrEmpty(Parameters.Password))
                {
                    var parts = Parameters.UserName.Split('\\');
                    _credentials = parts.Length > 1 
                        ? new NetworkCredential(parts[1], Parameters.Password, parts[0]) 
                        : new NetworkCredential(Parameters.UserName, Parameters.Password);
                }
            }
            return _credentials;
        }
        
        public bool Test()
        {
            try
            {
                using (var tfs = Connect())
                {
                    tfs.Authenticate();
                    if (tfs.HasAuthenticated)
                    {
                        IsValid = true;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
