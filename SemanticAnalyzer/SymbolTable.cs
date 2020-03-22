using System.Collections.Generic;

namespace MiniPL
{
    class SymbolTable
    {
        private Dictionary<string, Symbol> symbolTable;
        public SymbolTable()
        {
            this.symbolTable = new Dictionary<string, Symbol>();
        }

        public void add(string key, Symbol value)
        {
            symbolTable.Add(key, value);
        }

        public Symbol get(string key)
        {
            if (key == null) return null;
            if (symbolTable.ContainsKey(key))
            {
                return symbolTable[key];
            };
            return null;

        }
    }
}