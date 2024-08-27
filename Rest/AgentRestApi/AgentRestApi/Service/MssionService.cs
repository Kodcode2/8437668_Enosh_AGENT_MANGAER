using AgentRestApi.Controllers;
using AgentRestApi.Data;
using AgentRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace AgentRestApi.Service
{
    public class MssionService(ApplicationDbContext context) : IMissionService
    {

        //Constructs a task if it meets the conditions
        public async Task<List<MissionModel>> CreateMission(int agentId)
        {
            var agentModel = await FindByIdAgent(agentId);

            var ThereIsAPerception = await context.Targets
                .Where(t => t.TargetStatus == TargetModel.Status.live
            && Math.Sqrt(Math.Pow(agentModel.LocationX - t.LocationX, 2) +
                Math.Pow(agentModel.LocationY - t.LocationY, 2)) <= 200)
                .Select(t => t)
                .ToListAsync();
            if (await IsAgentStatus(agentId))
            {
                List<MissionModel> missionModel = new();

                foreach (var targetID in ThereIsAPerception)
                {
                    MissionModel mission = new()
                    {
                        AgentId = agentId,
                        TargetId = targetID.Id,
                        TimeLeft = CalculationOfDistanceFromTarget(targetID, agentModel) / 5,
                        MissionStatus = MissionModel.Status.taskProposal,

                        
                    };
                    //Book if it doesn't already exist
                    bool exist = context.Missions.Any(m => m.AgentId == mission.AgentId && m.TargetId == mission.TargetId);
                    if (exist)
                    {
                        continue;
                    }
                    missionModel.Add(mission);
                }
                await context.Missions.AddRangeAsync(missionModel);
                await context.SaveChangesAsync();
                return missionModel;
            }
            throw new Exception();
        }

        public async Task<List<MissionModel>> GetAllAsync() =>
      await context.Missions.ToListAsync();
        //Updates the task and the active agent
        public async Task<MissionModel> UpdateMissiomStatus(int id)
        {
            var missionId = await context.Missions.FindAsync(id)
                ?? throw new Exception();
            var agentModel = await context.Agents.FindAsync(missionId.AgentId);
            var targetModel = await context.Targets.FindAsync(missionId.TargetId);
            if (missionId.MissionStatus == MissionModel.Status.taskProposal
                && agentModel.Agentstatus == AgentModel.Status.offDuty
                && targetModel.TargetStatus == TargetModel.Status.live)
            {
                agentModel.Agentstatus = AgentModel.Status.Activete;
                targetModel.TargetStatus = TargetModel.Status.ThereIsAnAgentAttached;
                missionId.MissionStatus = MissionModel.Status.TheMissionIsOccupied;
                missionId.taskStartTime = DateTime.Now;
                await context.SaveChangesAsync();
                return missionId;
            }
            throw new Exception();
        }
        //A function to serve for chasing the agent after the target and checking if he reached the target
        public async Task<List<MissionModel>> MissionUpdateMoveToTarget()
        {
            var missions = await context.Missions
                .Where(m => m.MissionStatus == MissionModel.Status.TheMissionIsOccupied)
                .ToListAsync();

            foreach (var id in missions)
            {
                var agentModel = await context.Agents.FindAsync(id.AgentId);
                var targetModel = await context.Targets.FindAsync(id.TargetId);
                if (!EliminatesTheTarget(agentModel, targetModel))
                {
                    id.TimeLeft = CalculationOfDistanceFromTarget(id.Targets, id.Agent) / 5;
                    MoveAgentMssion(agentModel, targetModel);
                    await context.SaveChangesAsync();
                }
                else
                {
                    agentModel.Agentstatus = AgentModel.Status.offDuty;
                    targetModel.TargetStatus = TargetModel.Status.elimintated;
                    id.MissionStatus = MissionModel.Status.TheMissionIsOver;
                    id.ExecuteTime = $"{DateTime.Now - id.taskStartTime:mm\\:ss}";
                    await context.SaveChangesAsync();
                }
            }
            return missions;
        }

        public double CalculationOfDistanceFromTarget(TargetModel targetModel, AgentModel agentModel)
        {
            var distance = Math.Sqrt(Math.Pow(agentModel.LocationX - targetModel.LocationX, 2) +
                Math.Pow(agentModel.LocationY - targetModel.LocationY, 2));
            return distance;
        }
        public async Task<AgentModel> FindByIdAgent(int id) =>
            await context.Agents.FindAsync(id)
            ?? throw new Exception($"No ID {id} found");
        //Checking if the goal has been reached
        public async Task<TargetModel> FindByIdTarget(int id) =>
           await context.Targets.FindAsync(id)
           ?? throw new Exception($"No ID {id} found");
        public bool EliminatesTheTarget(AgentModel agentId, TargetModel targetId)
        {
            if (agentId.LocationX == targetId.LocationX && targetId.LocationY == targetId.LocationY)
            {
                return true;
            }
            return false;
        }
        //Agent pursuit function after the target

        public AgentModel MoveAgentMssion(AgentModel agentModel, TargetModel targetModel)
        {
            agentModel.LocationX = agentModel.LocationX < targetModel.LocationX ? agentModel.LocationX += 1 : agentModel.LocationX;
            agentModel.LocationX = agentModel.LocationX > targetModel.LocationX ? agentModel.LocationX -= 1 : agentModel.LocationX;
            agentModel.LocationY = agentModel.LocationY < targetModel.LocationY ? agentModel.LocationY += 1 : agentModel.LocationY;
            agentModel.LocationY = agentModel.LocationY > targetModel.LocationY ? agentModel.LocationY -= 1 : agentModel.LocationY;
            return agentModel;
        }

        
        public async Task<bool> IsAgentStatus(int Id)
        {
            var byId = await context.Agents.FindAsync(Id)
                ?? throw new Exception();
            if (byId.Agentstatus == AgentModel.Status.offDuty)
            {
                return true;
            }
            return false;
        }
    }
}
