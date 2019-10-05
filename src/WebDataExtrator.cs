using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OutlookCO2
{
    /// <summary>
    /// Extracts data from web endpoints using regular expressions.
    /// </summary>
    public class WebDataExtrator
    {
        private static HttpClient httpClient;

        /// <summary>
        /// Gets the data from a Http endpoint
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="regexPattern">The regex pattern.</param>
        /// <returns>Extracted data.</returns>
        public async Task<string[]> GetDataFromHttp(string uri, string regexPattern)
        {
            if (httpClient == null) httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(uri);
            return ExtractData(content, regexPattern);
        }

        /// <summary>
        /// Gets the data from a FTP endpoint.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="regexPattern">The regex pattern.</param>
        /// <returns>Extracted data.</returns>
        public async Task<string[]> GetDataFromFtp(string uri, string regexPattern)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            // request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            FtpWebResponse response = (FtpWebResponse) await request.GetResponseAsync();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = await reader.ReadToEndAsync();
            reader.Close();
            response.Close();

            return ExtractData(content, regexPattern);
        }
                
        /// <summary>
        /// Match a string with a regex expression.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="regexPattern">The regex pattern.</param>
        /// <returns>Matched data.</returns>
        private string[] ExtractData(string content, string regexPattern)
        {
            // RegEx tester: https://regex101.com/
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(content);
            if (match.Success)
            {
                return match.Groups.Cast<Group>().Select(o => o.Value).ToArray();
            }
            return new string[] { };
        }
    }
}
