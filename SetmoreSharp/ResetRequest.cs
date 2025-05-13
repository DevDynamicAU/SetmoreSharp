namespace SetmoreSharp
{
    public class Request
    {
        public string EndPoint { get; set; } = string.Empty;
        private Dictionary<string, object> parameters = new Dictionary<string, object>();
        private Dictionary<string, object> urlSegment = new Dictionary<string, object>();
        private List<KeyValuePair<string, string>> formParameters = new List<KeyValuePair<string, string>>();

        public string AcceptHeader { get; set; }

        public Request(string acceptHeader = "application/json")
        {
            AcceptHeader = acceptHeader;
        }

        public void AddUrlSegment(string name, object value)
        {
            if (!urlSegment.ContainsKey(name))
            {
                urlSegment.Add(name, value);
            }
        }

        public void AddParameter(string name, object value)
        {
            if (!parameters.ContainsKey(name))
            {
                parameters.Add(name, value);
            }
        }

        public void UpdateParameter(string name, object newValue)
        {
            if (parameters.ContainsKey(name))
            {
                parameters[name] = newValue;
            }
        }

        public void AddFormParameter(string name, string value)
        {
            formParameters.Add(new KeyValuePair<string, string>(name, value));
        }

        public string GetUrl()
        {
            string url = this.EndPoint;
            string query = string.Empty;
            string sep = "";

            if (!url.EndsWith("/") && !url.Contains("?"))
            {
                url += "/";
            }

            if (url.StartsWith("/"))
            {
                url = url.Substring(1, url.Length - 1);
            }

            foreach (var key in urlSegment.Keys)
            {
                object value = urlSegment[key];
                url = url.Replace("{" + key + "}", value.ToString());
            }

            foreach (var key in parameters.Keys)
            {
                object value = parameters[key];
                query += $"{sep}{key}={value}";
                sep = "&";
            }

            if (parameters.Any())
            {
                if (!url.Contains("?"))
                {
                    url += "?";
                }
                else if (!url.EndsWith("&"))
                {
                    url += "&";
                }

                url += query;
            }

            return url;
        }

        public IEnumerable<KeyValuePair<string, string>> GetFormVaules()
        {
            return formParameters;
        }
    }
}