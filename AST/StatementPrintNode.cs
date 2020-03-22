namespace MiniPL
{
    class StatementPrintNode : StatementNode
    {
        public ExpressionNode expression;

        public StatementPrintNode(ExpressionNode expression)
        {
            this.expression = expression;
        }
    }
}