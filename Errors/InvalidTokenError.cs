
namespace MiniPL
{
    class InvalidTokenError : Error
    {
        public InvalidTokenError(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Invalid token on line {token.line + 1}, column {token.column + 1}";
        }

    }
}