using System;
using System.Text;

namespace MiniPL
{

    class Scanner
    {
        private Reader reader;

        public Scanner(Reader reader)
        {
            this.reader = reader;
            reader.init();
        }

        public Token getNextToken()
        {
            while (true)
            {
                Character current = reader.next();
                if (current == null)
                {
                    return new Token(TokenType.END_OF_INPUT, -1, -1);
                }
                Character next;
                StringBuilder buffer;
                if (Char.IsWhiteSpace(current.character)) continue;
                switch (current.character)
                {
                    case '+':
                        return new Token(TokenType.OP_PLUS, current.line, current.column);
                    case '-':
                        return new Token(TokenType.OP_MINUS, current.line, current.column);
                    case '/':
                        return new Token(TokenType.OP_DIV, current.line, current.column);
                    case '*':
                        return new Token(TokenType.OP_MULTI, current.line, current.column);
                    case '<':
                        return new Token(TokenType.OP_LESSTHAN, current.line, current.column);
                    case '=':
                        return new Token(TokenType.OP_EQUALS, current.line, current.column);
                    case '&':
                        return new Token(TokenType.OP_AND, current.line, current.column);
                    case '!':
                        return new Token(TokenType.OP_NOT, current.line, current.column);
                    case ';':
                        return new Token(TokenType.TERMINATOR, current.line, current.column);
                    case '(':
                        return new Token(TokenType.PAREN_LEFT, current.line, current.column);
                    case ')':
                        return new Token(TokenType.PAREN_RIGHT, current.line, current.column);
                    case ':':
                        next = reader.next();
                        if (next.character == '=')
                        {
                            return new Token(TokenType.ASSIGNMENT, current.line, current.column);
                        }
                        reader.previous();
                        return new Token(TokenType.DECLARATION_DELIMITER, current.line, current.column);
                    case '.':
                        next = reader.next();
                        if (next.character == '.')
                        {
                            return new Token(TokenType.STATEMENT_FOR_RANGE, current.line, current.column);
                        }
                        reader.previous();
                        return new Token(TokenType.INVALID_TOKEN, current.character.ToString(), current.line, current.column);
                    case '"':
                        buffer = new StringBuilder();
                        buffer.Append(current.character);
                        while (true)
                        {
                            next = reader.next();
                            if (next.line != current.line)
                            {
                                reader.previous();
                                return new Token(TokenType.INVALID_MULTILINE_STRING, current.line, current.column);
                            }
                            if (next.character == '"')
                            {
                                buffer.Append(next.character);
                                break;
                            }
                            if (next.character == '\\' && (reader.peek().character == '\\' || reader.peek().character == '"'))
                            {
                                next = reader.next();
                            }
                            buffer.Append(next.character);
                        }
                        return new Token(TokenType.VALUE_STRING, buffer.ToString(), current.line, current.character);

                    default:
                        buffer = new StringBuilder();
                        buffer.Append(current.character);
                        while (true)
                        {
                            next = reader.next();
                            if (!Char.IsLetter(next.character) && !Char.IsNumber(next.character) && next.character != '_')
                            {
                                reader.previous();
                                break;
                            }
                            if (Char.IsWhiteSpace(next.character)) break;
                            buffer.Append(next.character);
                        }
                        string result = buffer.ToString();
                        switch (result)
                        {
                            case "var":
                                return new Token(TokenType.DECLARATION, current.line, current.character);
                            case "assert":
                                return new Token(TokenType.STATEMENT_ASSERT, current.line, current.character);
                            case "for":
                                return new Token(TokenType.STATEMENT_FOR, current.line, current.character);
                            case "end":
                                return new Token(TokenType.STATEMENT_FOR_END, current.line, current.character);
                            case "in":
                                return new Token(TokenType.STATEMENT_FOR_IN, current.line, current.character);
                            case "do":
                                return new Token(TokenType.STATEMENT_FOR_DO, current.line, current.character);
                            case "read":
                                return new Token(TokenType.STATEMENT_READ, current.line, current.character);
                            case "print":
                                return new Token(TokenType.STATEMENT_PRINT, current.line, current.character);
                            case "int":
                                return new Token(TokenType.DECLARATION_INT, current.line, current.character);
                            case "string":
                                return new Token(TokenType.DECLARATION_STRING, current.line, current.character);
                            case "bool":
                                return new Token(TokenType.DECLARATION_BOOL, current.line, current.character);
                            case "true":
                                return new Token(TokenType.VALUE_BOOL, "true", current.line, current.character);
                            case "false":
                                return new Token(TokenType.VALUE_BOOL, "false", current.line, current.character);
                            default:
                                int resultAsNumber;
                                if (int.TryParse(result, out resultAsNumber))
                                {
                                    return new Token(TokenType.VALUE_INT, result, current.line, current.character);
                                }
                                if (!Char.IsLetter(result[0]))
                                {
                                    return new Token(TokenType.INVALID_TOKEN, result, current.line, current.character);
                                }
                                return new Token(TokenType.IDENTIFIER, result, current.line, current.character);
                        }
                }
            }
        }
    }
}