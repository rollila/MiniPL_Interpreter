using System.Collections.Generic;

namespace MiniPL
{
    class StatementForNode : StatementNode
    {
        private VariableNode i;
        private ExpressionNode begin;
        private ExpressionNode end;
        private List<StatementNode> statements;

        public StatementForNode(VariableNode i, ExpressionNode begin, ExpressionNode end, List<StatementNode> statements)
        {
            this.i = i;
            this.begin = begin;
            this.end = end;
            this.statements = statements;
        }
    }
}