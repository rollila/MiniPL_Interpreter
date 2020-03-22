using System.Collections.Generic;

namespace MiniPL
{
    class StatementForNode : StatementNode
    {
        public VariableNode i;
        public ExpressionNode begin;
        public ExpressionNode end;
        public List<StatementNode> statements;
        public Token token;
        public StatementForNode(VariableNode i, ExpressionNode begin, ExpressionNode end, List<StatementNode> statements, Token token)
        {
            this.i = i;
            this.begin = begin;
            this.end = end;
            this.statements = statements;
            this.token = token;
        }
    }
}