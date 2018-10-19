using System;
using System.Threading.Tasks;
using diagrammatically.electron_edge.api;

namespace testconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new OptionApi();

            api.StartLogger("").Wait();

            Task.Delay(5000).Wait();
            Console.WriteLine("started");
            dynamic inp = new System.Dynamic.ExpandoObject();
            inp.selection = "testing";
            inp.typed = "tes";
            inp.sourse = "notepath";

            Console.WriteLine("selecting");
            api.SelectWord(inp).Wait();
            Console.WriteLine("selected");
            Console.ReadKey();
        }
    }
}
