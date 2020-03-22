namespace MiniPL
{
    class StatementDeclarationNode : StatementNode
    {
        public VariableNode variable;
        public Token token;
        public ExpressionNode value;

        public StatementDeclarationNode(VariableNode variable, Token token, ExpressionNode value)
        {
            this.variable = variable;
            this.token = token;
            this.value = value;
        }
    }
}