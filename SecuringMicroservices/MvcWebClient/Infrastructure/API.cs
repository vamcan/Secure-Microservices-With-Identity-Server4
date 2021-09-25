using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebClient.Infrastructure
{
    public static class API
    {
        public static class Job
        {
            public static string GetJob(string baseUri, int jobId)
            {
                return $"{baseUri}/{jobId}";
            }

            public static string GetAllJobs(string baseUri)
            {
                return baseUri;
            }
 
        }


 
    }
}
