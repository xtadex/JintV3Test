using Jint;
using Jint.Native;

namespace JintV3Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();

            var list = new List<int> { 3, 4, 5 };

            engine.Global.Set("arr", JsValue.FromObject(engine, list));

            Console.WriteLine(engine.Evaluate("arr[0]"));     // 3
            Console.WriteLine(engine.Evaluate("arr[1]"));     // 4
            Console.WriteLine(engine.Evaluate("arr[2]"));     // 5
            Console.WriteLine(engine.Evaluate("arr.length")); // 3
            Console.WriteLine(engine.Evaluate("arr[3]"));     // null
            Console.WriteLine(engine.Evaluate("arr.length")); // 3
            
            engine.Execute("arr[2] = 111");                   // OK
            Console.WriteLine(engine.Evaluate("arr[2]"));     // 111

            // Issues are here:
            
            engine.Execute("arr.push()");                     // Not updated
            Console.WriteLine(engine.Evaluate("arr.length")); // 3


            engine.Execute("arr.length = 4");                 // Not updated
            Console.WriteLine(engine.Evaluate("arr.length")); // 3
            

            engine.Execute("arr[3] = 222");                   // ArgumentOutOfRangeException
        }
    }
}
