namespace MiniPL
{
    class StatementAssignmentNode : StatementNode
    {
        private VariableNode variable;
        private ExpressionNode value;

        public StatementAssignmentNode(VariableNode variable, ExpressionNode value)
        {
            this.variable = variable;
            this.value = value;
        }
    }
}