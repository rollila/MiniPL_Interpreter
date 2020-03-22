namespace MiniPL
{
    class IntNode : OperandNode
    {
        public int value;

        public IntNode(int value)
        {
            this.value = value;
        }

        public object getValue()
        {
            return this.value;
        }
    }
}