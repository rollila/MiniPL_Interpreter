namespace MiniPL
{
    class DuplicateDeclarationError : Error
    {
        private string identifier;
        public DuplicateDeclarationError(Token token, string identifier) : base(token)
        {
            this.identifier = identifier;
        }

        public override string ToString()
        {
            return $"Declaration of already declared variable {identifier} on line {token.line + 1}, column {token.column + 1}";
        }
    }
}