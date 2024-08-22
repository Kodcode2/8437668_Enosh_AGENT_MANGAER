using AgentRestApi.Controllers;
using AgentRestApi.Data;
using AgentRestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace AgentRestApi.Service
{
    public class MssionService(ApplicationDbContext context) : IMssionService
    {
        public double CalculationOfDistanceFromTarget(TargetModel targetModel, AgentModel agentModel)
        {
            var distance = Math.Sqrt(Math.Pow(agentModel.LocationX - targetModel.LocationX, 2) +
                Math.Pow(agentModel.LocationY - agentModel.LocationY, 2));
            return distance;
        }
        public async Task<AgentModel> FindByIdAgent(int id) =>
            await context.Agents.FindAsync(id)
            ?? throw new Exception($"No ID {id} found");

        public async Task<TargetModel> FindByIdTarget(int id) =>
           await context.Targets.FindAsync(id)
           ?? throw new Exception($"No ID {id} found");

        //public double timeLeft(TargetModel targetModel, AgentModel agentModel) => CalculationOfDistanceFromTarget(targetModel, agentModel) / 5; 

        //public MissionModel MaximumCommandRange(TargetModel targetModel, AgentModel agentModel)
        //{
        //    var inTheMiddle = CalculationOfDistanceFromTarget(targetModel, agentModel);
        //    if (inTheMiddle >= 200) 
        //    { 
        //        MissionModel.Status = MissionModel.Status.taskProposal; 

        //    }
    //}
        public async Task<MissionModel> CreateMission( int agentId , int taergtId)
        {
           var agentModel = await FindByIdAgent(agentId);

            var targetModel = await FindByIdTarget(taergtId);   

            if(agentModel.Agentstatus == AgentModel.Status.Activete ||
                targetModel.TargetStatus == TargetModel.Status.elimintated || 
                targetModel.TargetStatus == TargetModel.Status.ThereIsAnAgentAttached)
            {
                throw new Exception();
            }
            if(CalculationOfDistanceFromTarget(targetModel, agentModel) > 200)
            {
                throw new Exception();
            }

            MissionModel missionModel = new()
            {
                AgentId = agentId,
                TargetId = agentId,
                TimeLeft = CalculationOfDistanceFromTarget(targetModel, agentModel) / 5,
                MissionStatus = MissionModel.Status.taskProposal,
            };
            await context.Missions.AddAsync(missionModel);
            await context.SaveChangesAsync();
            return missionModel;
        }
        public AgentModel MoveAgentMssion(AgentModel agentModel, TargetModel targetModel)
        {
            agentModel.LocationX = agentModel.LocationX < targetModel.LocationX ? agentModel.LocationX += 1 : agentModel.LocationX;
            agentModel.LocationX = agentModel.LocationX > targetModel.LocationX ? agentModel.LocationX -= 1 : agentModel.LocationX;
            agentModel.LocationY = agentModel.LocationY < targetModel.LocationY ? agentModel.LocationY += 1 : agentModel.LocationY;
            agentModel.LocationY = agentModel.LocationY > targetModel.LocationY ? agentModel.LocationY -= 1 : agentModel.LocationY;
            return agentModel;
        }

        public async Task<MissionModel> UpdateMoveMssiom(int id)
        {
            var byId = await context.Missions.FindAsync(id);
            if (byId != null)
            {
                throw new Exception();
            }
            MoveAgentMssion(byId.Agent, byId.Targets);
            return byId;


        }

        public async Task<MissionModel> StartingATask(int targetId,int agentId)
        {
            var agentModel = await FindByIdAgent(agentId);
            var targetModel = await FindByIdTarget(targetId);

            if(CalculationOfDistanceFromTarget(targetModel, agentModel) >= 200) 
            {
                throw new Exception(); 
            }
            return null;
          
        }

    }
}
