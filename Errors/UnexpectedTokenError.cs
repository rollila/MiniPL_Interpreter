
namespace MiniPL
{
    class UnexpectedTokenError : Error
    {
        public UnexpectedTokenError(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Unexpected token {token.type} on line {token.line + 1}, column {token.column + 1}";
        }

    }
}