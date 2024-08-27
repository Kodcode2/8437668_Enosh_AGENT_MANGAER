using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Service
{
    public interface IAgentService
    {
        Task<RestIdDto> CreateAgentAsync(AgentDto agentDto);

        Task<AgentModel> UpdateAgenByIdtAsync(AgentDto agentDto, int id);

        Task<List<AgentModel>> GetAllAsync();

        Task<AgentModel> UpdateLocationByIdAgentAsync(LocationDto locationDto, int id);
        Task<AgentModel> MoveLocationByIdAgentAsync(MoveDto moveDto, int id);




    }
}
