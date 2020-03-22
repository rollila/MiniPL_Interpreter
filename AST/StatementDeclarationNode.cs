namespace MiniPL
{
    class StatementDeclarationNode : StatementNode
    {
        public VariableNode variable;
        public Token type;
        public ExpressionNode value;

        public StatementDeclarationNode(VariableNode variable, Token type, ExpressionNode value)
        {
            this.variable = variable;
            this.type = type;
            this.value = value;
        }
    }
}