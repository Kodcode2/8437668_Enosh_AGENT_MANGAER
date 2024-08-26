using AgentRestVM.Models;
using AgentRestVM.ModelVM;

namespace AgentRestVM.Service
{
    public interface IMissionService
    {
        Task<List<MissionModel>> GetAllMission();
       
    }
}
