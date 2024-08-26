using AgentRestVM.Models;
using AgentRestVM.ModelVM;
using System.Text.Json;

namespace AgentRestVM.Service
{
    public class AgentService(IHttpClientFactory clientFactory, IMissionService missionService) : IAgentService
    {
        private readonly string AUrl = "http://localhost:5062/Agents";


        public async Task<List<AgentModel>> GetAllAgent()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, AUrl);
            //request.Headers.Authorization =
            //        new AuthenticationHeaderValue("Bearer", authentication.Token);
            var result = await httpClient.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<AgentModel>? agents = JsonSerializer.Deserialize<List<AgentModel>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return agents;
            }
            throw new Exception("Stotts Incorrect code");
        }

        public async Task<List<AgentVM>> GetAllAgentVM()
        {
            var agents = await GetAllAgent()
                ?? throw new Exception("There was a problem with billing the agent");
            var missins = await missionService.GetAllMission();


            List<AgentVM> agentVMs = new List<AgentVM>();

            foreach (var agent in agents)
            {
                //var misson = missins.FirstOrDefault(x => x.Id == agent.Id && agent.Agentstatus == AgentModel.Status.Activete);
                var time = missins.FirstOrDefault(x => x.Id == agent.Id).TimeLeft;

                var count = missins.Where(y => y.Id == agent.Id && y.MissionStatus == MissionModel.Status.TheMissionIsOver).Count();
                agentVMs.Add(new AgentVM
                {
                    NickName = agent.NickName,
                    PhotoUrl = agent.PhotoUrl,
                    LocationX = agent.LocationX,
                    LocationY = agent.LocationY,
                    Agentstatus = $"{agent.Agentstatus}",
                    Amount = count,
                    TimeLeft = time,
                });

            }
            return agentVMs;
        }

    }
}
