using System;

namespace AddValueHandler {
    public class Config {
        public static string CONNECTION_STRING 
        {
            get 
            {
                return Environment.GetEnvironmentVariable("CONNECTION_STRING");
            }
        }

        public static string BROKER_URL
        {
            get 
            {
                return Environment.GetEnvironmentVariable("MESSAGE_QUEUE_URL");
            }
        }
    }
}