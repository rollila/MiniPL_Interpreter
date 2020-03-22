
namespace MiniPL
{
    class InvalidNumberOfOperandsError : Error
    {
        public InvalidNumberOfOperandsError(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Invalid number of operands for operation on line {token.line + 1}, column {token.column + 1}";
        }

    }
}