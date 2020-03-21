namespace MiniPL
{
    class BooleanNode : OperandNode
    {
        public bool value;

        public BooleanNode(bool value)
        {
            this.value = value;
        }
    }
}