namespace SodaMachine.DomainModel.UserCommand.Base
{
    /// <summary>
    /// Used discovering usercommands in assembly. <seealso cref="UserCommandBase"/> and
    /// <see cref="OrderCommand"/> as examples.
    /// </summary>
    public interface IUserCommand
    {
        /// <summary>
        /// The command entered by the user on the commandprompt in the userinterface.
        /// </summary>
        string CommandText { get; }
        
        /// <summary>
        /// The text displayed in the userinterface, explaining the command and it's 
        /// syntax for the user.
        /// </summary>
        string MenuItemText { get; }

        /// <summary>
        /// Executes the functionality of usercommand in the sodamachine
        /// </summary>
        /// <param name="userCommandLine">Commandline entered by the user when executing the command.</param>
        void Execute(string[] userCommandLine);
    }
}