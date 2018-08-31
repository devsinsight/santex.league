using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Santex.League.API.Services
{
    public class CustomHttpResponseMessage
    {
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public object Content { get; set; }

        public CustomHttpResponseMessage(int statusCode,string reasonPhrase = null, object content = null) {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
            Content = content;
        }


        public static CustomHttpResponseMessage NotFound() {
            return new CustomHttpResponseMessage(404)
            {
                ReasonPhrase = "Not Found"
            };
        }

        public static CustomHttpResponseMessage LeagueCreated()
        {
            return new CustomHttpResponseMessage(201)
            {
                ReasonPhrase = "Successfully imported"
            };
        }

        public static CustomHttpResponseMessage LeagueAlreadyImported()
        {
            return new CustomHttpResponseMessage(409)
            {
                ReasonPhrase = "League already imported"
            };
        }

        public static CustomHttpResponseMessage ServerError()
        {
            return new CustomHttpResponseMessage(504)
            {
                ReasonPhrase = "Server Error"
            };
        }

        public static CustomHttpResponseMessage TotalPlayers(int total)
        {
            return new CustomHttpResponseMessage(200)
            {
                ReasonPhrase = "Ok",
                Content = new { Total = total }
            };
        }
    }
}
