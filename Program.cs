using System.Collections.Generic;
using System;

namespace MiniPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MiniPL Parser v0.01");
            Console.WriteLine("Enter filename to parse: ");
            string filename = Console.ReadLine();
            Reader fileReader = new FileReader(filename);
            Scanner scanner = new Scanner(fileReader);
            Parser parser = new Parser(scanner);
            List<StatementNode> statements = parser.parse();
            if (parser.errors.Count > 0)
            {
                Console.WriteLine("Found {0} errors while parsing:", parser.errors.Count);
                Console.WriteLine("");
                foreach (Error error in parser.errors)
                {
                    Console.WriteLine(error);
                }
                return;
            }
            SemanticAnalyzer analyzer = new SemanticAnalyzer(statements);
            analyzer.Analyze();
            if (analyzer.errors.Count > 0)
            {
                Console.WriteLine("Found {0} errors during semantic analysis:", analyzer.errors.Count);
                Console.WriteLine("");
                foreach (Error error in analyzer.errors)
                {
                    Console.WriteLine(error);
                }
                return;
            }
            Console.WriteLine("Program parsed & analyzed successfully and no errors found.");
        }
    }
}
