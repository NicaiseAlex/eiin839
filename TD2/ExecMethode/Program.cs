using System;

namespace ExecMethode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
                Console.WriteLine("<HTML><BODY> <h1>Question 5</h1> <br/> " + args[0] + " et " + args[1] + "</BODY></HTML>");
            else
                Console.WriteLine("Hello");
        }
    }
}
