using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Service
{
    public interface ITargetService
    {
        Task<TargetModel> CreateTargettAsync(TargetDto targetDto);

        Task<List<TargetModel>> GetAllAsync();

        Task<TargetModel> UpdateLocationByIdTargetAsync(LocationDto locationDto, int id);
        Task<TargetModel> MoveLocationByIdTargetAsync(MoveDto pinDto, int id);

    }

}
