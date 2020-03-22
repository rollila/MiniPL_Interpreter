namespace MiniPL
{
    class VariableNode : OperandNode
    {
        public string identifier;

        public VariableNode(string identifier)
        {
            this.identifier = identifier;
        }

        public object getValue()
        {
            return this.identifier;
        }

    }
}