using AgentRestVM.Models;
using AgentRestVM.ModelVM;
using System.Text.Json;

namespace AgentRestVM.Service
{
    public class TargetService(IHttpClientFactory clientFactory) : ITargetService
    {
        private readonly string MUrl = "http://localhost:5062/targets";


        public async Task<List<TargetModel>> GetAllTarget()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, MUrl);
            //request.Headers.Authorization =
            //        new AuthenticationHeaderValue("Bearer", authentication.Token);
            var result = await httpClient.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<TargetModel>? targets = JsonSerializer.Deserialize<List<TargetModel>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return targets;
            }
            throw new Exception("Stotts Incorrect code");
        }

         public async Task<List<TargetVM>> getAllTargetVM()
        {
            var targets = await GetAllTarget();
            List<TargetVM> targetVMs = new List<TargetVM>();

            foreach (var target in targets) 
            {
                targetVMs.Add(new TargetVM
                {
                    Name = target.Name,
                    Position = target.Position,
                    PhotoUrl = target.PhotoUrl,
                    LocationX = target.LocationX,
                    LocationY = target.LocationY,
                    TargetStatus = $"{target.TargetStatus}",
                });
            }
                return targetVMs;
        }
    }
}
