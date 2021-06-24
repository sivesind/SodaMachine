using System;
using System.Collections.Generic;

namespace SodaMachine.UserInterface
{
    /// <summary>
    /// Simple consolebased userinterface. An interface is used to allow mocking the userinterface.
    /// </summary>
    public class UserInterfaceImplementation : IUserInterface
    {
        /// <summary>
        /// Writes a list of menuoptions and waits for user keypress. This class is not tested since it's 
        /// simple and tightly linked to hardware.
        /// </summary>
        /// <param name="menuList">List of usercommands with syntax</param>
        /// <param name="currentAmount">The inserted amount of money is always displayed to the user</param>
        /// <returns>Raw user input as a string</returns>
        public string ShowMainMenuAndWaitForInput(List<string> menuList, uint currentAmount)
        {
            Console.WriteLine("\n\nAvailable commands:");
            //important that this is failsafe, menu should always halt on readline
            try
            {
                foreach (var menuItem in menuList)
                {
                    Console.WriteLine(menuItem);
                }
            }
            catch (Exception unexpectedException)
            {
                Console.WriteLine(
                    $"Error displaying menu, contact sivesind@gmail.com " +
                    $"with this info '{unexpectedException.Message}'");
            }

            Console.WriteLine("-------");
            Console.WriteLine($"Inserted amount: " + currentAmount);
            Console.WriteLine("-------\n\n");

            string inputFromUser = Console.ReadLine();

            return inputFromUser;
        }

        /// <summary>
        /// Display a given message to the user.
        /// </summary>
        public void ShowMessage(string messageToUser)
        {
            Console.WriteLine(messageToUser);
        }
    }
}
