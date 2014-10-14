using Crossword.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossword
{
    class Program
    {
        static List<Exception> s_Errors = new List<Exception>();

        static void Main(string[] args)
        {
            RunTestsIn<CacheTests>();
            RunTestsIn<DatabaseTests>();
            RunTestsIn<CluesTests>();
            RunTestsIn<GuardianTests>();
            if (s_Errors.Any()) Console.ReadKey();
        }

        private static void RunTestsIn<T>() where T : new()
        {
            var methods = typeof(T).GetMethods()
                .Where(m => m.DeclaringType == typeof(T))
                .Where(m => m.GetCustomAttributes(typeof(TestAttribute), false).Any())
                .Where(m => !m.GetCustomAttributes(typeof(ExplicitAttribute), false).Any())
                .Where(m => !m.GetCustomAttributes(typeof(IgnoreAttribute), false).Any());

            foreach (var test in methods)
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
