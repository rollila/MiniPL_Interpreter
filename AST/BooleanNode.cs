namespace MiniPL
{
    class BooleanNode : OperandNode
    {
        public bool value;

        public BooleanNode(bool value)
        {
            this.value = value;
        }

        public object getValue()
        {
            return this.value;
        }

    }

}