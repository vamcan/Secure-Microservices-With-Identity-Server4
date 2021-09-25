using Microsoft.Extensions.Options;
using MvcWebClient.Config;
using MvcWebClient.Http;
using MvcWebClient.Infrastructure;
using MvcWebClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MvcWebClient.Services
{
    public class JobService : IJobService
    {

        // private readonly IHttpClient _apiClient;
        private readonly HttpClient _apiClient;
        private readonly ApiConfig _apiConfig;
        private readonly string _remoteServiceBaseUrl;
        public JobService(HttpClient apiClient, IOptionsMonitor<ApiConfig> apiConfig)
        {
            _apiClient = apiClient;
            _apiConfig = apiConfig.CurrentValue;
            _remoteServiceBaseUrl = $"{_apiConfig.JobsApiUrl}/Jobs";
        }
        public async Task<Job> GetJob(int jobId)
        {
            var uri = API.Job.GetJob(_remoteServiceBaseUrl,jobId);
            var responseString = await _apiClient.GetStringAsync(uri);

            var job = JsonConvert.DeserializeObject<Job>(responseString);

            //var dataString = await _apiClient.GetStringAsync(_apiConfig.JobsApiUrl + "/jobs/" + jobId);
            //return JsonConvert.DeserializeObject<Job>(dataString);
            return job;

        
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var uri = API.Job.GetAllJobs(_remoteServiceBaseUrl);
            var response  = await _apiClient.GetAsync(uri).ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                var jobsString = await response.Content.ReadAsStringAsync();
                var jobs = JsonConvert.DeserializeObject<List<Job>>(jobsString);
                return jobs;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized || 
                response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }
            return null;
        }
    }
}
