using System;

namespace MiniPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader fileReader = new FileReader("test.minipl");
            Scanner scanner = new Scanner(fileReader);
            while (true)
            {
                Token t = scanner.getNextToken();
                if (t == null) break;
                Console.WriteLine(t);
            }
        }
    }
}
