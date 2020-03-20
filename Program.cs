using System;

namespace MiniPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader fileReader = new FileReader("test3.minipl");
            Scanner scanner = new Scanner(fileReader);
            while (true)
            {
                Token t = scanner.getNextToken();
                if (t.type == TokenType.END_OF_INPUT) break;
                Console.WriteLine(t);
            }
        }
    }
}
