namespace MiniPL
{
    enum TokenType
    {
        OP_PLUS,
        OP_MINUS,
        OP_MULTI,
        OP_DIV,
        OP_LESSTHAN,
        OP_EQUALS,
        OP_AND,
        OP_NOT,
        DECLARATION,
        DECLARATION_INT,
        DECLARATION_STRING,
        DECLARATION_BOOL,
        DECLARATION_DELIMITER,
        VALUE_INT,
        VALUE_STRING,
        VALUE_BOOL,
        ASSIGNMENT,
        VALUE,
        STATEMENT_READ,
        STATEMENT_PRINT,
        STATEMENT_ASSERT,
        STATEMENT_FOR,
        STATEMENT_FOR_IN,
        STATEMENT_FOR_END,
        STATEMENT_FOR_RANGE,
        STATEMENT_FOR_DO,
        PAREN_LEFT,
        PAREN_RIGHT,
        TERMINATOR,
        IDENTIFIER,
        INVALID_TOKEN,
        INVALID_MULTILINE_STRING,
        END_OF_INPUT

    }
    class Token
    {
        public TokenType type;
        public string value;
        public int line;
        public int column;

        public Token(TokenType type, int line, int column)
        {
            this.type = type;
            this.line = line;
            this.column = column;
        }

        public Token(TokenType type, string value, int line, int column)
        {
            this.type = type;
            this.value = value;
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}