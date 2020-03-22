namespace MiniPL
{
    class StatementAssignmentNode : StatementNode
    {
        public VariableNode variable;
        public ExpressionNode value;

        public StatementAssignmentNode(VariableNode variable, ExpressionNode value)
        {
            this.variable = variable;
            this.value = value;
        }
    }
}