namespace MiniPL
{
    class StatementReadNode : StatementNode
    {
        private VariableNode target;

        public StatementReadNode(VariableNode target)
        {
            this.target = target;
        }
    }
}