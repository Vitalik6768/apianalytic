using Google.Apis.AnalyticsData.v1beta;
using Google.Apis.AnalyticsData.v1beta.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace api.Services
{
    public class GoogleAnalyticsService
    {
        private readonly string _keyFilePath;
        private readonly string _propertyId;
        private readonly AnalyticsDataService _analyticsDataService;

        public GoogleAnalyticsService(IConfiguration configuration)
        {
            _keyFilePath = configuration["GoogleAnalytics:KeyFilePath"];
            _propertyId = configuration["GoogleAnalytics:PropertyId"];

            GoogleCredential credential;
            using (var stream = new FileStream(_keyFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(AnalyticsDataService.Scope.AnalyticsReadonly);
            }

            _analyticsDataService = new AnalyticsDataService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Analytics Data API Sample",
            });
        }

        public async Task<int> GetTotalUsersLast30DaysAsync()
        {
            var request = new RunReportRequest
            {
                Metrics = new List<Metric> { new Metric { Name = "activeUsers" } },
                DateRanges = new List<DateRange> { new DateRange { StartDate = "30daysAgo", EndDate = "today" } }
            };

            var response = await _analyticsDataService.Properties.RunReport(request, $"properties/{_propertyId}").ExecuteAsync();
            int totalUsers = 0;

            foreach (var row in response.Rows)
            {
                totalUsers += int.Parse(row.MetricValues[0].Value);
            }

            return totalUsers;
        }
    }

    
}