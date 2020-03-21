using System;
using System.Collections.Generic;

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

        public List<StatementNode> parse()
        {
            List<StatementNode> statements = new List<StatementNode>();
            currentToken = scanner.getNextToken();
            switch (currentToken.type)
            {
                case TokenType.DECLARATION:
                case TokenType.STATEMENT_FOR:
                case TokenType.STATEMENT_READ:
                case TokenType.STATEMENT_PRINT:
                case TokenType.STATEMENT_ASSERT:
                    statements = Statements();
                    match(TokenType.END_OF_INPUT);
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
            return statements;
        }

        private bool match(TokenType expected)
        {
            bool result = expected == currentToken.type;
            Console.WriteLine("Received: {0}, expected: {1}", currentToken.type, expected);
            if (!result) Console.WriteLine("Error at matching");
            consumeToken();
            return result;
        }

        private void consumeToken()
        {
            currentToken = scanner.getNextToken();
        }

        private List<StatementNode> Statements()
        {
            List<StatementNode> statements = new List<StatementNode>();
            switch (currentToken.type)
            {
                case TokenType.DECLARATION:
                case TokenType.IDENTIFIER:
                case TokenType.STATEMENT_FOR:
                case TokenType.STATEMENT_READ:
                case TokenType.STATEMENT_PRINT:
                case TokenType.STATEMENT_ASSERT:
                    statements.Add(Statement());
                    match(TokenType.TERMINATOR);
                    statements.AddRange(Statements());
                    Statements();
                    break;
                case TokenType.END_OF_INPUT:
                default:
                    break;
            }
            return statements;
        }

        private StatementNode Statement()
        {
            switch (currentToken.type)
            {
                case TokenType.DECLARATION:
                    match(TokenType.DECLARATION);
                    VariableNode declarationVariable = Identifier();
                    match(TokenType.DECLARATION_DELIMITER);
                    Token declarationType = Type();
                    ExpressionNode declarationValue = OptionalAssignment();
                    return new StatementDeclarationNode(declarationVariable, declarationType, declarationValue);
                case TokenType.IDENTIFIER:
                    VariableNode assignmentVariable = Identifier();
                    match(TokenType.ASSIGNMENT);
                    ExpressionNode assignmentValue = Expression();
                    return new StatementAssignmentNode(assignmentVariable, assignmentValue);
                case TokenType.STATEMENT_FOR:
                    match(TokenType.STATEMENT_FOR);
                    VariableNode i = Identifier();
                    match(TokenType.STATEMENT_FOR_IN);
                    ExpressionNode begin = Expression();
                    match(TokenType.STATEMENT_FOR_RANGE);
                    ExpressionNode end = Expression();
                    match(TokenType.STATEMENT_FOR_DO);
                    List<StatementNode> forStatements = Statements();
                    match(TokenType.STATEMENT_FOR_END);
                    match(TokenType.STATEMENT_FOR);
                    return new StatementForNode(i, begin, end, forStatements);
                case TokenType.STATEMENT_READ:
                    match(TokenType.STATEMENT_READ);
                    VariableNode readTarget = Identifier();
                    return new StatementReadNode(readTarget);
                case TokenType.STATEMENT_PRINT:
                    match(TokenType.STATEMENT_PRINT);
                    ExpressionNode printExpression = Expression();
                    return new StatementPrintNode(printExpression);
                case
                 TokenType.STATEMENT_ASSERT:
                    match(TokenType.STATEMENT_ASSERT);
                    match(TokenType.PAREN_LEFT);
                    ExpressionNode assertExpression = Expression();
                    match(TokenType.PAREN_RIGHT);
                    return new StatementAssertNode(assertExpression);
                default:
                    Console.WriteLine("Error at Statement");
                    return null;
            }
        }

        private ExpressionNode Expression()
        {
            ExpressionNode e = new ExpressionNode();
            switch (currentToken.type)
            {
                case TokenType.VALUE_BOOL:
                case TokenType.VALUE_INT:
                case TokenType.VALUE_STRING:
                case TokenType.IDENTIFIER:
                case TokenType.PAREN_LEFT:
                    e.left = Operand();
                    switch (currentToken.type)
                    {
                        case TokenType.OP_AND:
                        case TokenType.OP_DIV:
                        case TokenType.OP_EQUALS:
                        case TokenType.OP_LESSTHAN:
                        case TokenType.OP_MINUS:
                        case TokenType.OP_PLUS:
                        case TokenType.OP_MULTI:
                            e.op = Operator();
                            e.right = Operand();
                            break;
                        default:
                            break;
                    }
                    break;
                case TokenType.OP_NOT:
                    e.op = UnaryOperator();
                    e.right = Operand();
                    break;
                default:
                    Console.WriteLine("Invalid token at Expression");
                    break;
            }
            return e;
        }

        private Token UnaryOperator()
        {
            match(TokenType.OP_NOT);
            return currentToken;
        }

        private Token Operator()
        {
            switch (currentToken.type)
            {
                case TokenType.OP_AND:
                    match(TokenType.OP_AND);
                    return currentToken;
                case TokenType.OP_DIV:
                    match(TokenType.OP_DIV);
                    return currentToken;
                case TokenType.OP_EQUALS:
                    match(TokenType.OP_EQUALS);
                    return currentToken;
                case TokenType.OP_LESSTHAN:
                    match(TokenType.OP_LESSTHAN);
                    return currentToken;
                case TokenType.OP_MINUS:
                    match(TokenType.OP_MINUS);
                    return currentToken;
                case TokenType.OP_PLUS:
                    match(TokenType.OP_PLUS);
                    return currentToken;
                case TokenType.OP_MULTI:
                    match(TokenType.OP_MULTI);
                    return currentToken;
                default:
                    Console.WriteLine("Invalid token at Operator");
                    return null;
            }

        }

        private OperandNode Operand()
        {
            switch (currentToken.type)
            {
                case TokenType.VALUE_INT:
                    Token intToken = currentToken;
                    match(TokenType.VALUE_INT);
                    return new IntNode(Int32.Parse(intToken.value));
                case TokenType.VALUE_STRING:
                    Token stringToken = currentToken;
                    match(TokenType.VALUE_STRING);
                    return new StringNode(stringToken.value);
                case TokenType.VALUE_BOOL:
                    Token boolToken = currentToken;
                    match(TokenType.VALUE_BOOL);
                    return new BooleanNode(Boolean.Parse(boolToken.value));
                case TokenType.IDENTIFIER:
                    Token idToken = currentToken;
                    match(TokenType.IDENTIFIER);
                    return new VariableNode(idToken.value);
                case TokenType.PAREN_LEFT:
                    match(TokenType.PAREN_LEFT);
                    ExpressionNode e = Expression();
                    match(TokenType.PAREN_RIGHT);
                    return e;
                default:
                    Console.Write("Invalid token at Operand");
                    return null;
            }

        }

        private ExpressionNode OptionalAssignment()
        {
            if (currentToken.type == TokenType.ASSIGNMENT)
            {
                match(TokenType.ASSIGNMENT);
                return Expression();
            }
            return null;
        }

        private Token Type()
        {
            switch (currentToken.type)
            {
                case TokenType.DECLARATION_BOOL:
                    match(TokenType.DECLARATION_BOOL);
                    return currentToken;
                case TokenType.DECLARATION_INT:
                    match(TokenType.DECLARATION_INT);
                    return currentToken;
                case TokenType.DECLARATION_STRING:
                    match(TokenType.DECLARATION_STRING);
                    return currentToken;
                default:
                    Console.WriteLine("Invalid token at Type");
                    return null;
            }
        }

        private VariableNode Identifier()
        {
            if (currentToken.type == TokenType.IDENTIFIER)
            {
                match(TokenType.IDENTIFIER);
                return new VariableNode(currentToken.value);
            }
            else
            {
                Console.WriteLine("Invalid token at Identifier");
                return null;
            }

        }
    }
}