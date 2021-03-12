using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CC
    {
        public static void Message(ConsoleColor color, string Message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(Message);
            Console.ResetColor();
        }
    }
}
