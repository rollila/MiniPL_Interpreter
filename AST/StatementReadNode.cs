namespace MiniPL
{
    class StatementReadNode : StatementNode
    {
        public VariableNode target;
        public Token token;

        public StatementReadNode(VariableNode target, Token token)
        {
            this.target = target;
            this.token = token;
        }
    }
}