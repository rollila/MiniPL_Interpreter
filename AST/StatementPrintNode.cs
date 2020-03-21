namespace MiniPL
{
    class StatementPrintNode : StatementNode
    {
        private ExpressionNode expression;

        public StatementPrintNode(ExpressionNode expression)
        {
            this.expression = expression;
        }
    }
}