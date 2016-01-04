using System;
using System.Threading.Tasks;

namespace Sitecore.Rocks.Templates.Extensions
{
    public static class TaskExtensions
    {
        public static T WaitResult<T>(this Task<T> task, TimeSpan? timeout = null)
        {
            if (!task.Wait(timeout ?? TimeSpan.FromSeconds(3)))
            {
                throw new TimeoutException();
            }
            return task.Result;
        }
    }
}
