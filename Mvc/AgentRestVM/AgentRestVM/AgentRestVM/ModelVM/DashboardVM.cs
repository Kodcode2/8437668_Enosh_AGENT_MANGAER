namespace AgentRestVM.ModelVM
{
    public class DashboardVM
    {
        public int Agent {  get; set; }
        public int AgentActiv { get; set; }

        public int Target { get; set; }

        public int TargetEliminated { get; set; }

        public int Task { get; set; }

       public double RelationOfAgentsToGoals { get; set; }

        public double RelationOfAgentsToPossibleTargets { get; set; }


    }
}
