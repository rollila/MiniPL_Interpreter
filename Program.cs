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
                if (t == null) break;
                Console.WriteLine("");
                Console.WriteLine(t);
            }
        }
    }
}
