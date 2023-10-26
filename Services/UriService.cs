using VethubLanding.Interfaces;
using VethubLanding.Models;

namespace VethubLanding.Services
{
    public class UriService : IUriService
    {
        private readonly BaseURI _baseUri;
        public UriService(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAPIUri(string query = null)
        {
            var stringURI = _baseUri.APIURI;
            var uri = new Uri(stringURI);
            if (query == null) return uri;

            var modifiedUri = stringURI + "" + query;
            return new Uri(modifiedUri);
        }
    }
}
