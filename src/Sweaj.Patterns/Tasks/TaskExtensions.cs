namespace Sweaj.Patterns.Tasks
{
    public static class TaskExtensions
    {
        public static async Task WithHeartbeat(this Task task, CancellationToken cancellationToken, TimeSpan interval)
        {
            while (!task.IsCompleted)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await Task.Delay(interval, cancellationToken);
            }
        }
    }
}
