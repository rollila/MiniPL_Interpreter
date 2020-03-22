namespace MiniPL
{
    class StatementReadNode : StatementNode
    {
        public VariableNode target;

        public StatementReadNode(VariableNode target)
        {
            this.target = target;
        }
    }
}