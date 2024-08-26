using AgentRestVM.Models;
using AgentRestVM.ModelVM;
using System.Linq;
using System.Text.Json;

namespace AgentRestVM.Service
{
    public class MIssionService(IHttpClientFactory clientFactory) : IMissionService
    {

        private readonly string MUrl = "http://localhost:5062/Missions";
        public async Task<List<MissionModel>> GetAllMission()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, MUrl);
            //request.Headers.Authorization =
            //        new AuthenticationHeaderValue("Bearer", authentication.Token);
            var result = await httpClient.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<MissionModel>? missions = JsonSerializer.Deserialize<List<MissionModel>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return missions;
            }
            throw new Exception("Stotts Incorrect code");
        }

     
    }
}
