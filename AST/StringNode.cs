namespace MiniPL
{
    class StringNode : OperandNode
    {
        public string value;

        public StringNode(string value)
        {
            this.value = value;
        }

        public object getValue()
        {
            return this.value;
        }


    }
}