using System.Collections.Generic;
using System;

namespace MiniPL
{
    class SemanticAnalyzer
    {
        private List<StatementNode> AST;
        private SymbolTable symbolTable;

        public SemanticAnalyzer(List<StatementNode> AST)
        {
            this.AST = AST;
            this.symbolTable = new SymbolTable();
        }

        public void Analyze()
        {
            foreach (StatementNode statement in AST)
            {
                VisitStatementNode(statement);
            }
        }

        public void VisitStatementNode(StatementNode node)
        {
            Type statementType = node.GetType();
            Console.WriteLine(statementType);
            this.GetType().GetMethod("Visit" + statementType.Name).Invoke(this, new[] { node });
        }

        public void VisitStatementAssertNode(StatementAssertNode node)
        {
            object value = VisitExpressionNode(node.expression);
            if (!(value is bool))
            {
                Console.WriteLine("Invalid expression type for assert statement");
            }
        }

        public void VisitStatementAssignmentNode(StatementAssignmentNode node)
        {
            Symbol existing = symbolTable.get(node.variable.identifier);
            if (existing == null)
            {
                Console.WriteLine("Attempted to assign value before declaration");
                return;
            }
            object value = VisitExpressionNode(node.value);
            existing.value = value;
        }

        public void VisitStatementDeclarationNode(StatementDeclarationNode node)
        {
            Symbol existing = symbolTable.get(node.variable.identifier);
            if (existing != null)
            {
                Console.WriteLine("Variable {0} is already declared", node.variable.identifier);
                return;
            }
            object value = VisitExpressionNode(node.value);

            symbolTable.add(node.variable.identifier, new Symbol(node.type.type, value));
        }

        public void VisitStatementForNode(StatementForNode node)
        {
            Symbol i = VisitVariableNode(node.i);
            object begin = VisitExpressionNode(node.begin);
            object end = VisitExpressionNode(node.end);
            if (!(i.type == TokenType.DECLARATION_INT))
            {
                Console.WriteLine("Invalid type for variable {0}, expected int", node.i.identifier);
            }
            if (!(begin is int))
            {
                Console.WriteLine("Invalid expression at beginning of for range");
            }
            if (!(end is int))
            {
                Console.WriteLine("Invalid expression at end of for range");
            }
            foreach (StatementNode statement in node.statements)
            {
                VisitStatementNode(statement);
            }


        }

        public string VisitStatementPrintNode(StatementPrintNode node)
        {
            object value = VisitExpressionNode(node.expression);
            if (value is string)
            {
                return value as string;
            }
            Console.WriteLine("Invalid operand type for print, expected string");
            return null;
        }

        public string VisitStatementReadNode(StatementReadNode node)
        {
            Symbol value = VisitVariableNode(node.target);
            if (value.type != TokenType.DECLARATION_STRING)
            {
                Console.WriteLine("Invalid variable type for read statement");
                return null;
            }
            return value.value as string;
        }

        public string VisitStringNode(StringNode stringNode)
        {
            return stringNode.value;
        }

        public Symbol VisitVariableNode(VariableNode variableNode)
        {
            return symbolTable.get(variableNode.identifier);
        }

        public object VisitExpressionNode(ExpressionNode node)
        {
            if (node == null) return null;
            if (node.op == null)
            {
                return node.left.getValue();
            }
            object valueLeft = VisitOperandNode(node.left);
            object valueRight = VisitOperandNode(node.right);
            if (valueLeft is bool)
            {
                if (node.op.type == TokenType.OP_NOT)
                {
                    return !(bool)valueLeft;
                }
                if (node.op.type == TokenType.OP_AND)
                {
                    if (valueRight == null)
                    {
                        return false;
                    }
                    return (bool)valueLeft && (bool)valueRight;
                }
                else
                {
                    Console.WriteLine("Invalid operand type for OP_NOT");
                    return null;
                }
            }
            if (valueLeft is int)
            {
                if (valueRight is null)
                {
                    Console.WriteLine("Missing right operand for numeric operation");
                    return null;
                }
                if (!(valueRight is int))
                {
                    Console.WriteLine("Invalid right operand for numeric operation");
                    return null;
                }
                switch (node.op.type)
                {
                    case TokenType.OP_PLUS:
                        return (int)valueLeft + (int)valueRight;
                    case TokenType.OP_MINUS:
                        return (int)valueLeft - (int)valueRight;
                    case TokenType.OP_MULTI:
                        return (int)valueLeft * (int)valueRight;
                    case TokenType.OP_DIV:
                        return (int)valueLeft / (int)valueRight;
                    case TokenType.OP_LESSTHAN:
                        return (int)valueLeft < (int)valueRight;
                    case TokenType.OP_EQUALS:
                        return (int)valueLeft == (int)valueRight;
                    default:
                        Console.WriteLine("Invalid operation for numeric operands");
                        return null;
                }
            }
            if (valueLeft is string)
            {
                if (valueRight is null)
                {
                    Console.WriteLine("Missing right operand for string operation");
                    return null;
                }
                switch (node.op.type)
                {
                    case TokenType.OP_EQUALS:
                        return valueLeft.Equals(valueRight);
                    default:
                        Console.WriteLine("Invalid operation for string operands");
                        return null;
                }
            }


            return null;
        }


        public object VisitOperandNode(OperandNode node)
        {
            if (node == null) return null;

            if (node is ExpressionNode)
            {
                return VisitExpressionNode(node as ExpressionNode);
            }

            if (node is VariableNode)
            {
                return VisitVariableNode(node as VariableNode).value;
            }

            return node.getValue();
        }
    }

}