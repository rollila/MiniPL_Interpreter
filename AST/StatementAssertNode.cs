namespace MiniPL
{
    class StatementAssertNode : StatementNode
    {
        public ExpressionNode expression;
        public Token token;

        public StatementAssertNode(ExpressionNode expression, Token token)
        {
            this.expression = expression;
            this.token = token;
        }
    }
}