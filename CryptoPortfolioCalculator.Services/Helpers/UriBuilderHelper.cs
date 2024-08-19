using System.Collections.Specialized;

namespace CoinLoreMiddleware.Services.Helpers
{
    public class UriBuilderHelper(string uri)
    {
        private NameValueCollection collection = System.Web.HttpUtility.ParseQueryString(string.Empty);
        private UriBuilder builder = new UriBuilder(uri);

        public void AddParameter(string key, string value)
            => collection.Add(key, value);

        public Uri GetUri()
        {
            builder.Query = collection.ToString();
            return builder.Uri;
        }
    }
}
