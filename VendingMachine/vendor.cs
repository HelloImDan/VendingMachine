/*  
 *  Author : Daniel
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VendingMachine
{
    public class vendor
    {
        #region Variable Decloration
        //Setup vars
        public int balance = 0;
        ConsoleKeyInfo keyinfo; //Console key info is shortened to keyinfo
        #endregion


        #region User Money Input
        public void moneyInput(string inputValCoins)
        {
            
            switch (inputValCoins)
            {
                //If inputValCoins is 10, 20 or 50
                case "10":
                case "20":
                case "50":

                    int coins = Convert.ToInt16(inputValCoins); //Convert to a int and store as coins
                    balance = coins + balance;  //Add coins to the users balance 

                    Console.Clear();    //Clear the console for new text to be written

                    Console.WriteLine("*** Current Balance: " + balance + " ***");  //Show user their ballance was sucessfully updated and their current balance
                    Console.WriteLine("*** Input More Money or Press 'c' to Continue ***"); //Give user new instructions

                    keyinfo = Console.ReadKey(); //keyinfo is used to read the key being pressed

                    if (keyinfo.Key == ConsoleKey.C) //If the pressed key is == to C
                    {
                        ItemSelection();    //Go to the function to display the current items in the vending machine
                    }else
                    {
                        Console.Clear();
                        Console.WriteLine("*** Input Your Money ***"); //Let user know what we want
                        string inputCoins = Console.ReadLine(); //users input is a string. Store the input as strCoins.
                        moneyInput(inputCoins);    //Go to the function to add money
                    }
                    break;  //Exit the switch case
                
                //Value inputted it not accepted
                default:
                    Console.WriteLine("*** Please insert a accepted coin [10, 20, 50] ***");    //Tell user the value was not accepted and show accepted values

                    inputValCoins = Console.ReadLine(); //users input is a string. Store the input as strCoins
                    moneyInput(inputValCoins); //go to the money input function (Back to the start)

                    break;  //Exit the switch case

            }
        }
        #endregion

        #region Item Selection
        public void ItemSelection()
        {
            Console.Clear();    //Clear the console for text to be written
            Console.WriteLine("********* Choose your item *********");  //Tell user what to do
            Console.WriteLine("");  //Spacer

            string[] items = File.ReadAllLines(@"../../items.txt"); //Read all lines of the 'items.txt' file and store each line in the array
            foreach (string n in items)  //For each item in the array 'items'
            {
                Console.WriteLine(n);    //Write the line
            }

            Console.WriteLine("");  //Spacer
            Console.WriteLine("********* Enter Your Choice *********");  //Tell user what to do

            string itemChoice = Console.ReadLine(); //Users selected item 

            switch (itemChoice)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    int item = Convert.ToInt16(itemChoice); //Convert to a int and store as coins
                    int i = item - 1; // Slected item -1 because it is in array
                    string[] itemStrip = items[i].Replace(" ", "").Split('*');
                    Console.WriteLine("Are you sure you want to purchase " + itemStrip[1] + "? [y,n]"); //Confirmation of purchase
                    string ans = Console.ReadLine(); // Get users answer
                        switch (ans) //switch case for users answer
                        {
                            case "y":
                                PurchaseItem(itemStrip); //purchase item function
                                break;
                            case "n":
                                ItemSelection(); //reset chosen item and restart
                                break;
                            default:
                                Console.WriteLine("*** Please enter a valid command ***"); //Display error
                                Console.WriteLine("*** Y = Yes | N = No ***");  //Display what user should put
                                break;  //Exit the switch case
                    }
                    break;  //Exit the switch case

                default:
                    Console.WriteLine("*** Please enter a valid item ID [1, 2, 3, 4] ***");    //Tell user the value was not accepted and show accepted values
                    break;  //Exit the switch case

            }
        }
        #endregion

        #region Item Purchase
        public void PurchaseItem(string[] itemStrip)
        {
            var p = itemStrip[2]; //Price is equal to 3rd item in array
            var name = itemStrip[1];  //Name is equal to 2nd item in array
            int price = Convert.ToInt16(p); //Convert to int and store as price

            if (balance < price) //If users ballance is less than the items price (user does not have enough balance to purchase the item)
            {
                Console.Clear(); //Clear the console
                Console.WriteLine("*** You do not have enough money to purchase that item. ***");
                Console.WriteLine("*** Please add more by pressing 'A'. ***");

                keyinfo = Console.ReadKey(); //keyinfo is used to read the key being pressed

                if (keyinfo.Key == ConsoleKey.A) //If the pressed key is == to A
                {
                    Console.Clear();
                    Console.WriteLine("*** Input Your Money ***"); //Let user know what we want
                    string inputValCoins = Console.ReadLine(); //users input is a string. Store the input as strCoins.
                    moneyInput(inputValCoins);    //Go to the function to add money
                }
            }
            else{
                balance = price - balance;  //Update balance to item balance - price
                Console.Clear();
                Console.WriteLine("*** Purchase Successfull ***");
                Console.WriteLine("*** Your " + name + " is ready to be collected ***");
                Console.WriteLine("You have " + balance + " left in your account.");
                Console.WriteLine("Would you like to continue shopping? [y,n]");

                string ans = Console.ReadLine(); // Get users answer
                switch (ans) //switch case for users answer
                {
                    case "y":
                        ItemSelection(); //purchase item function
                        break;
                    case "n":
                        exit();
                        break;
                    default:
                        Console.WriteLine("*** Please enter a valid command ***"); //Display error
                        Console.WriteLine("*** Y = Yes | N = No ***");  //Display what user should put
                        break;  //Exit the switch case
                }
            }
            
        }
        #endregion

        #region User Exit
        public void exit()
        {
            Console.Clear();
            Console.WriteLine("*** " + balance + " is ready to be collected ***");  //Show user he can collect is coins
            Console.WriteLine("*** Thank You For Shopping At Dan's Vending Machine ***");  //Dont forget to SMASH that like button!!!!!11
        }
        #endregion
    }
}
