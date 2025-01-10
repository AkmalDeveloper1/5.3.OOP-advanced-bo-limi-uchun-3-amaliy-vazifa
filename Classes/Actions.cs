using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5._3.OOP_advanced_bo_limi_uchun_3_amaliy_vazifa.Classes
{
    public class Actions
    {    
        public static void SetColor(string colorMessage,ConsoleColor color = ConsoleColor.Green)
        {
            ConsoleColor actualColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(colorMessage);
            Console.ForegroundColor = actualColor;
        }
        public static void SetColorLine(string colorMessage,ConsoleColor color = ConsoleColor.Green)
        {
            ConsoleColor actualColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(colorMessage);
            Console.ForegroundColor = actualColor;
        }
    }
}