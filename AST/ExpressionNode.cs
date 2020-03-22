namespace MiniPL
{
    class ExpressionNode : OperandNode
    {
        public OperandNode left;
        public OperandNode right;
        public Token op;

        public ExpressionNode()
        {

        }

        public object getValue()
        {
            return null;
        }

    }
}