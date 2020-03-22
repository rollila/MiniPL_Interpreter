namespace MiniPL
{
    class Symbol
    {
        public TokenType type;
        public object value;

        public Symbol(TokenType type, object value)
        {
            this.type = type;
            this.value = value;
        }
    }
}