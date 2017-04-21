using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            vendor v = new vendor();


            Console.Clear();    //Clear console
            Console.WriteLine("*** Input Your Money ***"); //Let user know what we want
            string inputValCoins = Console.ReadLine(); //users input is a string. Store the input as strCoins.
            v.moneyInput(inputValCoins);

        }
    }
}
