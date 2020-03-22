
namespace MiniPL
{
    class InvalidMultilineStringError : Error
    {
        public InvalidMultilineStringError(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"String starting on line {token.line + 1}, column {token.column + 1} does not terminate on the same line.";
        }

    }
}