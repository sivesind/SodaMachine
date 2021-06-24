using System.Collections.Generic;

namespace SodaMachine.UserInterface
{
    /// <summary>
    /// Added to be able to mock userinterface in solution. The userinterface is a commanline window.
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Displays the main menu to user with a given list of menuitem.
        /// </summary>
        /// <param name="menuList">List of menuitems as string</param>
        /// <param name="currentAmount">The amount the user has in it's cashregister</param>
        /// <returns></returns>
        string ShowMainMenuAndWaitForInput(List<string> menuList, uint currentAmount);

        /// <summary>
        /// Display a textmessage to the user.
        /// </summary>
        /// <param name="messageToUser">The message to display</param>
        void ShowMessage(string messageToUser);
    }
}