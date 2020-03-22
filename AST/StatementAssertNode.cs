namespace MiniPL
{
    class StatementAssertNode : StatementNode
    {
        public ExpressionNode expression;

        public StatementAssertNode(ExpressionNode expression)
        {
            this.expression = expression;
        }
    }
}