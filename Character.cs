namespace MiniPL
{
    class Character
    {
        public char character;
        public int line;
        public int column;

        public Character(char character, int line, int column)
        {
            this.character = character;
            this.line = line;
            this.column = column;
        }
    }
}