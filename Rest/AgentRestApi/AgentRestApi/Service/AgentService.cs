using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Service
{
    public class AgentService(ApplicationDbContext context, IServiceProvider serviceProvider) : IAgentService
    {
        //So that we don't have to service each other
        private IMissionService missionService = serviceProvider.GetRequiredService<IMissionService>();
        public async Task<RestIdDto> CreateAgentAsync(AgentDto agentDto)
        {
            if (agentDto == null)
            {
                throw new Exception("not Found");
            }
            AgentModel agentModel = new()
            {
                NickName = agentDto.NickName,
                PhotoUrl = agentDto.PhotoUrl,
            };
            await context.AddAsync(agentModel);
            await context.SaveChangesAsync();
            return  new() { id  =  agentModel.Id};
        }

        public async Task<AgentModel> UpdateAgenByIdtAsync(AgentDto agentDto, int id)
        {
            var ById = await context.Agents.FindAsync(id);
            if (ById == null)
            {
                throw new Exception("not Found");
            }
            ById = new()
            {
                NickName = agentDto.NickName,
                PhotoUrl = agentDto.PhotoUrl,              
            };
            await context.SaveChangesAsync();
            return ById;
        }

        public async Task<List<AgentModel>> GetAllAsync() =>
            await context.Agents.ToListAsync();

        public async Task<AgentModel> UpdateLocationByIdAgentAsync(LocationDto locationDto, int id)
        {
            var byId = await context.Agents.FindAsync(id);


            //Gets the location sent by the server
            byId.LocationX = locationDto.X;
            byId.LocationY = locationDto.Y;
            await context.SaveChangesAsync();
            return byId;

        }
        //A dictionary that saves us the variables of the directions
        private readonly Dictionary<string, (int x, int y)> dicMove = new()
        {
            {"n", (0,1) },
            {"ne", (1,1) },
            {"nw", (-1, 1) },
            { "w", (-1, 0)},
            {"e", (1, 0) },
            {"sw", (-1, -1) },
            {"s", (0, -1) },
            {"se", (1, -1) }

        };

        public async Task<AgentModel> MoveLocationByIdAgentAsync(MoveDto moveDto, int id)
        {
            bool IsExsit = dicMove.TryGetValue(moveDto.Direction, out var result);
             

            if (!IsExsit)
            {
                throw new Exception($"The value you entered is not equal to the {moveDto.Direction} location");
            }
            var byId = await context.Agents.FindAsync(id);
            if (byId == null)
            {
                throw new Exception($"No ID {id} found");
            }
            //Moved to the point that received service
            byId.LocationX += dicMove[moveDto.Direction].x;
            byId.LocationY += dicMove[moveDto.Direction].y;

            if (byId.LocationX < 0 || byId.LocationX > 1000 ||
             byId.LocationY < 0 || byId.LocationY > 1000)
            {
                throw new Exception("The value you entered is invalid or no ID found");
            }
            //Sending to check whether he has a mission from his range
            await missionService.CreateMission(byId.Id);
            await context.SaveChangesAsync();
            return byId;
        
        }



    }
}
