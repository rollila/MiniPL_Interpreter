using System.IO;

namespace MiniPL
{
    class FileReader : Reader
    {
        private string filename;
        private string[] text;
        private int line = 0;
        private int column = -1;

        public FileReader(string filename)
        {
            this.filename = filename;
        }

        public void init()
        {
            this.text = File.ReadAllLines(this.filename);
        }

        public Character peek()
        {
            if (!hasNextChar() && !hasNextLine()) return null;
            if (!hasNextChar() && hasNextLine()) return new Character(text[line + 1][0], line + 1, 0);
            return new Character(text[line][column + 1], line, column + 1);
        }

        public Character next()
        {
            Character newChar = peek();
            if (newChar == null) return null;
            this.line = newChar.line;
            this.column = newChar.column;
            return newChar;
        }

        public Character previous()
        {
            if (!hasPreviousChar() && !hasPreviousLine()) return null;
            if (!hasPreviousChar() && hasPreviousLine())
            {
                line -= 1;
                column = text[line].Length - 1;
            }
            else
            {
                column -= 1;
            }
            return new Character(text[line][column], line, column);
        }

        private bool hasNextLine()
        {
            return text.Length > line + 1;
        }

        private bool hasNextChar()
        {
            return text[line].Length > column + 1;
        }

        private bool hasPreviousLine()
        {
            return line > 0;
        }

        private bool hasPreviousChar()
        {
            return column > 0;
        }
    }
}