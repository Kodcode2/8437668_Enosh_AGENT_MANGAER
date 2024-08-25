using AgentRestApi.Models;

namespace AgentRestApi.Service
{
    public interface IMissionService
    {
        Task<List<MissionModel>> CreateMission(int agentId);

        Task<List<MissionModel>> GetAllAsync();

        Task<MissionModel> UpdateMissiomStatus(int id);

        Task<List<MissionModel>> MissionUpdateMoveToTarget();
    }
}
