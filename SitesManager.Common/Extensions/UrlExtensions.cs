using System;
using System.Net;

namespace SitesManager.Common.Extensions
{
    public static class UrlExtensions
    {
        public static HttpStatusCode GetStatusCode(this string url, int timeout = 30000)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return HttpStatusCode.NotFound;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout;
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode;
            }
            catch (Exception e) //TODO need logging service
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return HttpStatusCode.NotFound;
            }
        }
    }
}
