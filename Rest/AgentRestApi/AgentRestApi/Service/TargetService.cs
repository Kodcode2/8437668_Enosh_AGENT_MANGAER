using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Service
{
    public class TargetService(ApplicationDbContext context) : ITargetService
    {
        public async Task<RestDto?> CreateTargetAsync(TargetDto targetDto)
        {
            if (targetDto == null)
            {
                throw new Exception("You have not entered any value");
            }
            TargetModel targetModel = new()
            {
                Name = targetDto.Name,
                Position = targetDto.Position,
                PhotoUrl = targetDto.PhotoUrl,

            };
            await context.AddAsync(targetModel);
            await context.SaveChangesAsync();
            return new() { id = targetModel.Id};
        }
        public async Task<List<TargetModel>> GetAllAsync() =>
         await context.Targets.ToListAsync();

        public async Task<TargetModel> UpdateLocationByIdTargetAsync(LocationDto locationDto, int id)
        {
           var byId = await context.Targets.FindAsync(id);
            //    if (byId.LocationX < 0 || byId.LocationX > 1000 ||
            //     byId.LocationY < 0 || byId.LocationY > 1000 || byId == null)
            //    {
            //        throw new Exception("The value you entered is invalid or no ID found");
            //    }


            byId.LocationX = locationDto.X;
            byId.LocationY = locationDto.Y;
            
            await context.SaveChangesAsync();
            return byId;    

            }
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

        public async Task<TargetModel> MoveLocationByIdTargetAsync(MoveDto moveDto, int id)
        {
            bool IsExsit = dicMove.TryGetValue(moveDto.Direction, out var result);

            if (!IsExsit)
            {
                throw new Exception($"The value you entered is not equal to the {moveDto.Direction} location");
            }
            var byId = await context.Targets.FindAsync( id);
            if ( byId == null)
            {
                throw new Exception($"No ID {id} found");
            }

            byId.LocationX += dicMove[moveDto.Direction].x;
            byId.LocationY += dicMove[moveDto.Direction].y;

            if(byId.LocationX < 0 || byId.LocationX > 1000 ||
             byId.LocationY < 0 || byId.LocationY > 1000 )
            {
                throw new Exception("The value you entered is invalid or no ID found");
            }

            await context.SaveChangesAsync();
            return byId;

        }
    }
}
