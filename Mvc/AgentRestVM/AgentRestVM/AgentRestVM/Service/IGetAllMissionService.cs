using AgentRestVM.ModelVM;

namespace AgentRestVM.Service
{
    public interface IGetAllMissionService
    {
        Task<List<MissionVM>> GetAllTask();

        Task<MissionVM> DetailsMissoin(int id);

        Task<DashboardVM> GetDashboard();
    }
}
