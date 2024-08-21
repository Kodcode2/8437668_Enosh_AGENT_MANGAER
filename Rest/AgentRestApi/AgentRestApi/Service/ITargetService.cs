using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Service
{
    public interface ITargetService
    {
        Task<TargetModel> CreateTargettAsync(TargetDto targetDto);

        Task<List<TargetModel>> GetAllAsync();
    }
}
