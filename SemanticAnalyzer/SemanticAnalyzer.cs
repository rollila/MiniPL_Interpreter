using System.Collections.Generic;
using System;

namespace MiniPL
{
    class SemanticAnalyzer
    {
        private List<StatementNode> AST;
        private SymbolTable symbolTable;
        public List<Error> errors;

        public SemanticAnalyzer(List<StatementNode> AST)
        {
            this.AST = AST;
            this.symbolTable = new SymbolTable();
            this.errors = new List<Error>();
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
            this.GetType().GetMethod("Visit" + statementType.Name).Invoke(this, new[] { node });
        }

        public void VisitStatementAssertNode(StatementAssertNode node)
        {
            object value = VisitExpressionNode(node.expression);
            if (value is VariableNode)
            {
                Symbol variable = symbolTable.get((value as VariableNode).identifier);
                if (variable == null)
                {
                    errors.Add(new UndeclaredVariableError(node.token));
                    return;
                }
                if (variable.type != TokenType.DECLARATION_BOOL)
                {
                    errors.Add(new InvalidTypeError(node.token));
                    return;
                }
                return;
            }
            if (!(value is bool))
            {
                errors.Add(new InvalidTypeError(node.token));
            }
        }

        public void VisitStatementAssignmentNode(StatementAssignmentNode node)
        {
            Symbol existing = symbolTable.get(node.variable.identifier);
            if (existing == null)
            {
                errors.Add(new UndeclaredVariableError(node.token));
                return;
            }
            object value = VisitExpressionNode(node.value);
            if (value is int && existing.type != TokenType.DECLARATION_INT)
            {

                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            if (value is bool && existing.type != TokenType.DECLARATION_BOOL)
            {
                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            if (value is string && existing.type != TokenType.DECLARATION_STRING)
            {
                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            if (value is VariableNode)
            {
                string targetIdentifier = (value as VariableNode).identifier;
                Symbol target = symbolTable.get(targetIdentifier);
                if (target == null)
                {
                    errors.Add(new UndeclaredVariableError(node.token));
                    return;
                }
                if (target.type != node.token.type)
                {
                    errors.Add(new InvalidTypeError(node.token));
                    return;
                }
                value = target.value;
            }
            existing.value = value;
        }

        public void VisitStatementDeclarationNode(StatementDeclarationNode node)
        {
            Symbol existing = symbolTable.get(node.variable.identifier);
            if (existing != null)
            {
                errors.Add(new DuplicateDeclarationError(node.token, node.variable.identifier));
                return;
            }
            object value = VisitExpressionNode(node.value);
            if (value is int && node.token.type != TokenType.DECLARATION_INT)
            {
                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            if (value is bool && node.token.type != TokenType.DECLARATION_BOOL)
            {
                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            if (value is string && node.token.type != TokenType.DECLARATION_STRING)
            {
                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            if (value is VariableNode)
            {
                string targetIdentifier = (value as VariableNode).identifier;
                Symbol target = symbolTable.get(targetIdentifier);
                if (target == null)
                {
                    errors.Add(new UndeclaredVariableError(node.token));
                    return;
                }
                if (target.type != node.token.type)
                {
                    errors.Add(new InvalidTypeError(node.token));
                    return;
                }
                value = target.value;
            }

            symbolTable.add(node.variable.identifier, new Symbol(node.token.type, value));
        }

        public void VisitStatementForNode(StatementForNode node)
        {
            Symbol i = VisitVariableNode(node.i);
            object begin = VisitExpressionNode(node.begin);
            object end = VisitExpressionNode(node.end);

            List<Error> localErrors = new List<Error>();

            if (i == null)
            {
                localErrors.Add(new UndeclaredVariableError(node.token));
            }
            if (begin == null)
            {
                localErrors.Add(new UndeclaredVariableError(node.token));
            }
            if (end == null)
            {
                localErrors.Add(new UndeclaredVariableError(node.token));
            }

            if (i != null && !(i.type == TokenType.DECLARATION_INT))
            {
                localErrors.Add(new InvalidTypeError(node.token));
            }
            if (begin != null && !(begin is int) && !(begin is VariableNode))
            {
                localErrors.Add(new InvalidTypeError(node.token));
            }
            if (begin != null && !(end is int) && !(end is VariableNode))
            {
                localErrors.Add(new InvalidTypeError(node.token));
            }
            if (localErrors.Count > 0)
            {
                errors.AddRange(localErrors);
                return;
            }
            i.value = begin is VariableNode ? symbolTable.get((begin as VariableNode).identifier) : begin;
            foreach (StatementNode statement in node.statements)
            {
                VisitStatementNode(statement);
            }


        }

        public object VisitStatementPrintNode(StatementPrintNode node)
        {
            object value = VisitExpressionNode(node.expression);
            if (value is VariableNode)
            {
                Symbol variableSymbol = VisitVariableNode(value as VariableNode);
                if (variableSymbol != null)
                {
                    return variableSymbol.value;
                }
            }
            return value;
        }

        public void VisitStatementReadNode(StatementReadNode node)
        {
            Symbol value = VisitVariableNode(node.target);
            if (value.type != TokenType.DECLARATION_STRING && value.type != TokenType.DECLARATION_INT)
            {
                errors.Add(new InvalidTypeError(node.token));
                return;
            }
            return;
        }

        public Symbol VisitVariableNode(VariableNode variableNode)
        {
            Symbol var = symbolTable.get(variableNode.identifier);
            if (var == null)
            {
                return null;
            }
            return var;
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
                    errors.Add(new InvalidTypeError(node.op));
                    return null;
                }
            }

            if (valueLeft is int)
            {
                if (valueRight is null)
                {
                    errors.Add(new InvalidNumberOfOperandsError(node.op));
                    return null;
                }
                if (!(valueRight is int))
                {
                    errors.Add(new InvalidNumberOfOperandsError(node.op));
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
                        errors.Add(new InvalidTypeError(node.op));
                        return null;
                }
            }
            if (valueLeft is string)
            {
                if (valueRight is null)
                {
                    errors.Add(new InvalidNumberOfOperandsError(node.op));
                    return null;
                }
                switch (node.op.type)
                {
                    case TokenType.OP_EQUALS:
                        return valueLeft.Equals(valueRight);
                    default:
                        errors.Add(new InvalidTypeError(node.op));
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