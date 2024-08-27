using AgentRestVM.Models;
using AgentRestVM.ModelVM;
using System.Text;
using System.Text.Json;

namespace AgentRestVM.Service
{
    public class GetAllMissionService(IHttpClientFactory clientFactory, IMissionService missionService,
        IAgentService agentService, ITargetService targetService) : IGetAllMissionService
    {
        public async Task<List<MissionVM>> GetAllTask()
        {
            var agents = await agentService.GetAllAgent();
            var targets = await targetService.GetAllTarget();
            var missions = await missionService.GetAllMission();

            List<MissionVM> missionVMs = new List<MissionVM>();

            missions = missions.Where(m => m.MissionStatus == MissionModel.Status.taskProposal).ToList();

            foreach (var mission in missions)
            {
                var agent = agents.FirstOrDefault(a => a.Id == mission.AgentId);
                var target = targets.FirstOrDefault(t => t.Id == mission.TargetId);


                missionVMs.Add(new MissionVM
                {
                    NickName = agent.NickName,
                    Location_X_Agent = agent.LocationX,
                    Location_Y_Agent = agent.LocationY,
                    TargetName = target.Name,
                    TargetPosition = target.Position,
                    Location_X_Target = target.LocationX,
                    Location_Y_Target = target.LocationY,
                    TimeLeft = mission.TimeLeft,
                    Distance = mission.TimeLeft * 5

                });

            }
            return missionVMs;
        }

        public async Task<MissionVM> DetailsMissoin(int id)
        {
            var missions = await GetAllTask();
            var mission = missions.Find(m => m.Id == id);
            return mission;
        }

        public async Task<DashboardVM> GetDashboard()
        {
            var agents = await agentService.GetAllAgent();
            var targets = await targetService.GetAllTarget();
            var missions = await missionService.GetAllMission();

            var  countAgents = agents.Count();
            var AgentIsActiv = agents.Where(a => a.Agentstatus == AgentModel.Status.Activete).Count();
            var countTarget = targets.Count();
            var TargetEliminated = targets.Where(t => t.TargetStatus == TargetModel.Status.elimintated).Count();
            var countMissions = missions.Count();
            var RelationOfAgentsToGoals = countAgents / countTarget;
            var RelationOfAgentsToPossibleTargets = missions
                .Where(m => m.MissionStatus  == MissionModel.Status.taskProposal)
                .Select(M => M.AgentId).ToHashSet().Count() / countTarget ;

            DashboardVM dashboardVM = new DashboardVM()
            {
                Agent = countAgents,
                AgentActiv = AgentIsActiv,
                Target = countTarget,
                TargetEliminated = TargetEliminated,
                Task = countMissions,
                RelationOfAgentsToGoals = RelationOfAgentsToGoals,
                RelationOfAgentsToPossibleTargets = RelationOfAgentsToPossibleTargets,             
            };
            return dashboardVM;
        }

        public async Task<string> UpdateMissionToActive(int id)
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5226/Missions/{id}");
            var httpContent = new StringContent(
                JsonSerializer.Serialize
                ("status : assigned"),
                Encoding.UTF8,
                "application/json"
              );
            request.Content = httpContent;
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                return "Success";
            }
            return "Not Succsess";
        }
    }
}
