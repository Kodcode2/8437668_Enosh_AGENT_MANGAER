using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Service
{
    public interface ITargetService
    {
        Task<TagentModel> CreateTargettAsync(TargetDto targetDto);

        Task<List<TagentModel>> GetAllAsync();

        Task<TagentModel> UpdateLocationByIdTargetAsync(LocationDto locationDto, int id);
        Task<TagentModel> MoveLocationByIdTargetAsync(MoveDto pinDto, int id);

    }

}
