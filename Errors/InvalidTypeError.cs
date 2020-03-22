
namespace MiniPL
{
    class InvalidTypeError : Error
    {
        public InvalidTypeError(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Invalid type used in assignment, argument or operand on line {token.line + 1}, column {token.column + 1}";
        }

    }
}