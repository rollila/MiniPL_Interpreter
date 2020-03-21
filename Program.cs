using System.Collections.Generic;
using System;

namespace MiniPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader fileReader = new FileReader("test3.minipl");
            Scanner scanner = new Scanner(fileReader);
            Parser parser = new Parser(scanner);
            List<StatementNode> statements = parser.parse();
            foreach (StatementNode statement in statements)
            {
                Console.WriteLine(statement);

            }
        }
    }
}
