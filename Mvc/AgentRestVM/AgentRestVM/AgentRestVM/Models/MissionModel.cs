namespace AgentRestVM.Models
{
    public class MissionModel
    {
        public enum Status
        {
            taskProposal,
            TheMissionIsOccupied,
            TheMissionIsOver

        }


        public int Id { get; set; }

            public int TargetId { get; set; }

            public int AgentId { get; set; }
            public double TimeLeft { get; set; }

            public DateTime taskStartTime { get; set; }
            public string? ExecuteTime { get; set; }

            public Status MissionStatus { get; set; }
         
    }
}
