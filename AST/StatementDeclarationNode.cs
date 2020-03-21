namespace MiniPL
{
    class StatementDeclarationNode : StatementNode
    {
        private VariableNode variable;
        private Token type;
        private ExpressionNode value;

        public StatementDeclarationNode(VariableNode variable, Token type, ExpressionNode value)
        {
            this.variable = variable;
            this.type = type;
            this.value = value;
        }
    }
}