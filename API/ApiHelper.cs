using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace APILib
{
    public class ApiHelper
    {
        public static HttpClient _client { get; set; } = new HttpClient();

        public ApiHelper()
        {
        }
    }
}
