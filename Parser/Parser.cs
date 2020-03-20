using System;

namespace MiniPL
{
    class Parser
    {
        private Scanner scanner;
        private Token currentToken;
        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
        }

        public void parse()
        {
            currentToken = scanner.getNextToken();
            switch (currentToken.type)
            {
                case TokenType.DECLARATION:
                case TokenType.STATEMENT_FOR:
                case TokenType.STATEMENT_READ:
                case TokenType.STATEMENT_PRINT:
                case TokenType.STATEMENT_ASSERT:
                    Statements();
                    match(TokenType.END_OF_INPUT);
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }

        public bool match(TokenType expected)
        {
            bool result = expected == currentToken.type;
            if (!result) Console.WriteLine("Error at matching");
            consumeToken();
            return result;
        }

        public void consumeToken()
        {
            currentToken = scanner.getNextToken();
        }

        public void Statements()
        {
            switch (currentToken.type)
            {
                case TokenType.DECLARATION:
                case TokenType.IDENTIFIER:
                case TokenType.STATEMENT_FOR:
                case TokenType.STATEMENT_READ:
                case TokenType.STATEMENT_PRINT:
                case TokenType.STATEMENT_ASSERT:
                    Statement();
                    match(TokenType.TERMINATOR);
                    Statements();
                    break;
                case TokenType.END_OF_INPUT:
                    break;
                default:
                    Console.WriteLine("Erroneus token at Statements");
                    break;
            }
        }

        public void Statement()
        {
            switch (currentToken.type)
            {
                case TokenType.DECLARATION:
                    match(TokenType.DECLARATION);
                    Identifier();
                    match(TokenType.DECLARATION_DELIMITER);
                    Type();
                    OptionalAssignment();
                    break;
                case TokenType.IDENTIFIER:
                    Identifier();
                    match(TokenType.ASSIGNMENT);
                    Expression();
                    break;
                case TokenType.STATEMENT_FOR:
                    match(TokenType.STATEMENT_FOR);
                    Identifier();
                    match(TokenType.STATEMENT_FOR_IN);
                    Expression();
                    match(TokenType.STATEMENT_FOR_RANGE);
                    Expression();
                    match(TokenType.STATEMENT_FOR_DO);
                    Statements();
                    match(TokenType.STATEMENT_FOR_END);
                    match(TokenType.STATEMENT_FOR);
                    break;
                case TokenType.STATEMENT_READ:
                    match(TokenType.STATEMENT_READ);
                    Identifier();
                    break;
                case TokenType.STATEMENT_PRINT:
                    match(TokenType.STATEMENT_PRINT);
                    Expression();
                    break;
                case TokenType.STATEMENT_ASSERT:
                    match(TokenType.PAREN_LEFT);
                    Expression();
                    match(TokenType.PAREN_RIGHT);
                    break;
                default:
                    Console.WriteLine("Error at Statement");
                    break;
            }
        }

        public void Expression()
        {
            switch (currentToken.type)
            {
                case TokenType.VALUE_BOOL:
                case TokenType.VALUE_INT:
                case TokenType.VALUE_STRING:
                case TokenType.IDENTIFIER:
                case TokenType.PAREN_LEFT:
                    Operand();
                    switch (currentToken.type)
                    {
                        case TokenType.OP_AND:
                        case TokenType.OP_DIV:
                        case TokenType.OP_EQUALS:
                        case TokenType.OP_LESSTHAN:
                        case TokenType.OP_MINUS:
                        case TokenType.OP_PLUS:
                        case TokenType.OP_MULTI:
                            Operator();
                            Operand();
                            break;
                        default:
                            break;
                    }
                    break;
                case TokenType.OP_NOT:
                    UnaryOperator();
                    Operand();
                    break;
                default:
                    Console.WriteLine("Invalid token at Expression");
                    break;
            }
        }

        public void UnaryOperator()
        {
            match(TokenType.OP_NOT);
        }

        public void Operator()
        {
            switch (currentToken.type)
            {
                case TokenType.OP_AND:
                    match(TokenType.OP_AND);
                    break;
                case TokenType.OP_DIV:
                    match(TokenType.OP_DIV);
                    break;
                case TokenType.OP_EQUALS:
                    match(TokenType.OP_EQUALS);
                    break;
                case TokenType.OP_LESSTHAN:
                    match(TokenType.OP_LESSTHAN);
                    break;
                case TokenType.OP_MINUS:
                    match(TokenType.OP_MINUS);
                    break;
                case TokenType.OP_PLUS:
                    match(TokenType.OP_PLUS);
                    break;
                case TokenType.OP_MULTI:
                    match(TokenType.OP_MULTI);
                    break;
                default:
                    Console.WriteLine("Invalid token at Operator");
                    break;
            }

        }

        public void Operand()
        {
            switch (currentToken.type)
            {
                case TokenType.VALUE_INT:
                    match(TokenType.VALUE_INT);
                    break;
                case TokenType.VALUE_STRING:
                    match(TokenType.VALUE_STRING);
                    break;
                case TokenType.VALUE_BOOL:
                    match(TokenType.VALUE_BOOL);
                    break;
                case TokenType.IDENTIFIER:
                    match(TokenType.IDENTIFIER);
                    break;
                case TokenType.PAREN_LEFT:
                    match(TokenType.PAREN_LEFT);
                    Expression();
                    match(TokenType.PAREN_RIGHT);
                    break;
                default:
                    Console.Write("Invalid token at Operand");
                    break;
            }

        }

        public void OptionalAssignment()
        {
            if (currentToken.type == TokenType.ASSIGNMENT)
            {
                match(TokenType.ASSIGNMENT);
                Expression();
            }
        }

        public void Type()
        {
            switch (currentToken.type)
            {
                case TokenType.DECLARATION_BOOL:
                    match(TokenType.DECLARATION_BOOL);
                    break;
                case TokenType.DECLARATION_INT:
                    match(TokenType.DECLARATION_INT);
                    break;
                case TokenType.DECLARATION_STRING:
                    match(TokenType.DECLARATION_STRING);
                    break;
                default:
                    Console.WriteLine("Invalid token at Type");
                    break;
            }
        }

        public void Identifier()
        {
            if (currentToken.type == TokenType.IDENTIFIER)
            {
                match(TokenType.IDENTIFIER);
            }
            else
            {
                Console.WriteLine("Invalid token at Identifier");
            }

        }
    }
}