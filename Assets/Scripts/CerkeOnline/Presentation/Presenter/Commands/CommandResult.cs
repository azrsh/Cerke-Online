namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class CommandResult
    {
        public readonly string message;
        public readonly bool isSuccess;

        public CommandResult(bool isSuccess, string message)
        {
            this.isSuccess = isSuccess;
            this.message = message;
        }
    }
}