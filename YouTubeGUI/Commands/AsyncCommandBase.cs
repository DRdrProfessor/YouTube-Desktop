using System.Threading.Tasks;

namespace YouTubeGUI.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        public abstract Task ExecuteAsync();

        public override bool CanExecute(object? parameter) => !IsExecuting && base.CanExecute(parameter);

        public override async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync();
            }
            finally
            {
                IsExecuting = false;
            }
        }
        private bool _isExecuting;
        private bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                InvokeCanExecuteChanged();
            }
        }
    }
}