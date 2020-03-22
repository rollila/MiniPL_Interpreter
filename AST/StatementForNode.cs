using System.Collections.Generic;

namespace MiniPL
{
    class StatementForNode : StatementNode
    {
        public VariableNode i;
        public ExpressionNode begin;
        public ExpressionNode end;
        public List<StatementNode> statements;

        public StatementForNode(VariableNode i, ExpressionNode begin, ExpressionNode end, List<StatementNode> statements)
        {
            this.i = i;
            this.begin = begin;
            this.end = end;
            this.statements = statements;
        }
    }
}