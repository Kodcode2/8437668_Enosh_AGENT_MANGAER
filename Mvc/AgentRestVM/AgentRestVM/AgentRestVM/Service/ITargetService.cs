using AgentRestVM.Models;
using AgentRestVM.ModelVM;

namespace AgentRestVM.Service
{
    public interface ITargetService
    {
        Task<List<TargetModel>> GetAllTarget();

        Task<List<TargetVM>> getAllTargetVM();
    }
}
