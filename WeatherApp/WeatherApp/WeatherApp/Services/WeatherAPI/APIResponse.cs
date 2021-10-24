using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Services.WeatherAPI
{
    public class APIResponse
    {
        public bool Successful => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
    }
}
