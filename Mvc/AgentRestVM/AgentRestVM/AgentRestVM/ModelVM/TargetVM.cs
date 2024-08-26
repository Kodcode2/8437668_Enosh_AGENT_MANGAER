using System.ComponentModel.DataAnnotations;

namespace AgentRestVM.ModelVM
{
    public class TargetVM
    {

        public int Id { get; set; }

       
        public string Name { get; set; }

        public string Position { get; set; }

        public string PhotoUrl { get; set; }
        public int LocationX { get; set; } = -1;
        public int LocationY { get; set; } = -1;

        public string TargetStatus { get; set; }

    }
}
