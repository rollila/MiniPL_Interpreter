namespace MiniPL
{
    class StatementAssignmentNode : StatementNode
    {
        public VariableNode variable;
        public ExpressionNode value;
        public Token token;

        public StatementAssignmentNode(VariableNode variable, ExpressionNode value, Token token)
        {
            this.variable = variable;
            this.value = value;
            this.token = token;
        }
    }
}