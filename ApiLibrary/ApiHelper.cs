using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The ApiHelper class contains methods and variables necessary for creating a http client
    /// <summary>
    /// The <c>ApiHelper</c> class.
    /// Contains variables and methods necessary for creating a http client.
    /// </summary>
    public static class ApiHelper
    {
        // The ApiClient
        /// <value>Gets and Sets the ApiClient value.</value>
        public static HttpClient ApiClient { get; set; }

        // Initializes the http client
        /// <summary>
        /// Initializes the http client.
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }
    }
}
