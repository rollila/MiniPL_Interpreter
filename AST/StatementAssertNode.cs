namespace MiniPL
{
    class StatementAssertNode : StatementNode
    {
        private ExpressionNode expression;

        public StatementAssertNode(ExpressionNode expression)
        {
            this.expression = expression;
        }
    }
}