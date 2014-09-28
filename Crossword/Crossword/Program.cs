using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
    class Program
    {
        static List<Exception> s_Errors = new List<Exception>();

        static void Main(string[] args)
        {
            RunTestsIn<DatabaseTests>();
            if (s_Errors.Any()) Console.ReadKey();
        }

        private static void RunTestsIn<T>() where T : new()
        {
            foreach (var test in typeof(T).GetMethods().Where(m => m.DeclaringType == typeof(T)))
            {
                try
                {
                    test.Invoke(new T(), new object[] { });
                    Console.Write(".");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    s_Errors.Add(e);
                }
            }
        }
    }
}
