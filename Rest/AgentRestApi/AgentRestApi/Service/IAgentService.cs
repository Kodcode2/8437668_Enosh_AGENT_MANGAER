using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Service
{
    public interface IAgentService
    {
        Task<AgentModel> CreateAgentAsync(AgentDto agentDto);

        Task<AgentModel> UpdateAgenByIdtAsync(AgentDto agentDto, int id);

        Task<List<AgentModel>> GetAllAsync();
    }
}
