using AgentRestVM.Models;
using AgentRestVM.ModelVM;

namespace AgentRestVM.Service
{
    public interface IAgentService
    {
        Task<List<AgentModel>> GetAllAgent();

        Task<List<AgentVM>> GetAllAgentVM();
    }
}
