using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyMessageApp.Core.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = "application/json";


        public HttpStatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string key, string message) : base(String.Format("Key:{0}, Message:{1}", key.ToString(), message))
        {
            Key = key;
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string key = "", string message = "", string title = "") : base(message)
        {
            Key = key;
            StatusCode = statusCode;
            Title = title;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }
}
