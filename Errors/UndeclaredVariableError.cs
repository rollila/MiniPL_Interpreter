namespace MiniPL
{
    class UndeclaredVariableError : Error
    {
        public UndeclaredVariableError(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Undeclared variable {token.value} used on line {token.line + 1}, column {token.column + 1}";
        }
    }
}