namespace AgentRestVM.Models
{
    public class TargetModel
    {
        public enum Status
        {
            live,
            ThereIsAnAgentAttached,
            elimintated
        }

        public int Id { get; set; }

        
        public string Name { get; set; }

        public string Position { get; set; }

        public string PhotoUrl { get; set; }

        public int LocationX { get; set; } 

        public int LocationY { get; set; } 

        public Status TargetStatus { get; set; }

    }
}
