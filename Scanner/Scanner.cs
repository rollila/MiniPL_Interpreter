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
                    return new Token(TokenType.END_OF_INPUT);
                }
                Character next;
                StringBuilder buffer;
                if (Char.IsWhiteSpace(current.character)) continue;
                // Console.Write(current.character);
                switch (current.character)
                {
                    case '+':
                        return new Token(TokenType.OP_PLUS);
                    case '-':
                        return new Token(TokenType.OP_MINUS);
                    case '/':
                        return new Token(TokenType.OP_DIV);
                    case '*':
                        return new Token(TokenType.OP_MULTI);
                    case '<':
                        return new Token(TokenType.OP_LESSTHAN);
                    case '=':
                        return new Token(TokenType.OP_EQUALS);
                    case '&':
                        return new Token(TokenType.OP_AND);
                    case '!':
                        return new Token(TokenType.OP_NOT);
                    case ';':
                        return new Token(TokenType.TERMINATOR);
                    case '(':
                        return new Token(TokenType.PAREN_LEFT);
                    case ')':
                        return new Token(TokenType.PAREN_RIGHT);
                    case ':':
                        next = reader.next();
                        if (next.character == '=')
                        {
                            // Console.Write(next.character);
                            return new Token(TokenType.ASSIGNMENT);
                        }
                        reader.previous();
                        return new Token(TokenType.DECLARATION_DELIMITER);
                    case '.':
                        next = reader.next();
                        if (next.character == '.')
                        {
                            // Console.Write(next.character);
                            return new Token(TokenType.STATEMENT_FOR_RANGE);
                        }
                        reader.previous();
                        return new Token(TokenType.INVALID_TOKEN);
                    case '"':
                        buffer = new StringBuilder();
                        buffer.Append(current.character);
                        while (true)
                        {
                            next = reader.next();
                            if (next.line != current.line)
                            {
                                reader.previous();
                                return new Token(TokenType.INVALID_TOKEN);
                            }
                            if (next.character == '"')
                            {
                                // Console.Write(next.character);
                                buffer.Append(next.character);
                                break;
                            }
                            if (next.character == '\\' && (reader.peek().character == '\\' || reader.peek().character == '"'))
                            {
                                next = reader.next();
                            }
                            // Console.Write(next.character);
                            buffer.Append(next.character);
                        }
                        return new Token(TokenType.VALUE_STRING, buffer.ToString());

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
                            // Console.Write(next.character);
                            buffer.Append(next.character);
                        }
                        string result = buffer.ToString();
                        switch (result)
                        {
                            case "var":
                                return new Token(TokenType.DECLARATION);
                            case "assert":
                                return new Token(TokenType.STATEMENT_ASSERT);
                            case "for":
                                return new Token(TokenType.STATEMENT_FOR);
                            case "end":
                                return new Token(TokenType.STATEMENT_FOR_END);
                            case "in":
                                return new Token(TokenType.STATEMENT_FOR_IN);
                            case "do":
                                return new Token(TokenType.STATEMENT_FOR_DO);
                            case "read":
                                return new Token(TokenType.STATEMENT_READ);
                            case "print":
                                return new Token(TokenType.STATEMENT_PRINT);
                            case "int":
                                return new Token(TokenType.DECLARATION_INT);
                            case "string":
                                return new Token(TokenType.DECLARATION_STRING);
                            case "bool":
                                return new Token(TokenType.DECLARATION_BOOL);
                            case "true":
                                return new Token(TokenType.VALUE_BOOL, "true");
                            case "false":
                                return new Token(TokenType.VALUE_BOOL, "false");
                            default:
                                int resultAsNumber;
                                if (int.TryParse(result, out resultAsNumber))
                                {
                                    return new Token(TokenType.VALUE_INT, result);
                                }
                                if (!Char.IsLetter(result[0]))
                                {
                                    return new Token(TokenType.INVALID_TOKEN);
                                }
                                Console.WriteLine(result);
                                return new Token(TokenType.IDENTIFIER, result);
                        }
                }
            }
        }
    }
}