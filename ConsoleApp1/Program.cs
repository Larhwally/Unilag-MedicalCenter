using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string date = DateTime.Now + "";
            string[] newdate = date.Split(' ');
            Console.WriteLine(newdate[0]);
        }
    }
}
