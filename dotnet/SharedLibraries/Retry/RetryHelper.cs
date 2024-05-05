using SharedLibraries.Extensions;

namespace SharedLibraries.Retry
{
    /// <summary>
    /// Retry helper for calling the methods.
    /// </summary>
    public static class RetryHelper
    {
        #region Fields

        private static readonly ushort[] DefaultSpans = [500, 400, 200];

        #endregion

        #region Methods
        
        /// <summary>
        /// Invokes an action with retry.
        /// If an exception that occured is a transient error or the invoker wants to retry, retry several times.
        /// </summary>
        /// <param name="onAction">The invoking action.</param>
        /// <param name="transientExceptionFilter">
        /// The check logic whether the exception thrown is a transient exception.
        /// Retry invoking the action, if the check result is <c>true</c>. Otherwise; throws the exception.
        /// </param>
        /// <param name="retrySpans">The time spans to retry. The unit of them is milliseconds.</param>
        public static void InvokeWithRetry(Action onAction, Predicate<Exception> transientExceptionFilter, params ushort[] retrySpans)
        {
            if (transientExceptionFilter is null)
            {
                InvokeWithRetry(onAction, retrySpans);
                return;
            }

            if (onAction is null)
            {
                return;
            }

            var intervals = retrySpans.IsNullOrEmpty() ? DefaultSpans : retrySpans;
            foreach (var interval in intervals)
            {
                try
                {
                    onAction.Invoke();
                    return;
                }
                catch (Exception ex)
                {
                    if (!transientExceptionFilter(ex))
                    {
                        // If this isn't a transient error or we shouldn't retry,
                        // rethrow the exception.
                        throw;
                    }

                    Task.Delay(interval).Wait();
                }
            }

            onAction.Invoke();
        }

        /// <summary>
        /// Invokes an action with retry. If an exception is occured, retry several times.
        /// </summary>
        /// <param name="onAction">The invoking action.</param>
        /// <param name="retrySpans">The time spans to retry. The unit of them is milliseconds.</param>
        public static void InvokeWithRetry(Action onAction, params ushort[] retrySpans)
        {
            if (onAction is null)
            {
                return;
            }

            var intervals = retrySpans.IsNullOrEmpty() ? DefaultSpans : retrySpans;
            foreach (var interval in intervals)
            {
                try
                {
                    onAction.Invoke();
                    return;

                }
                catch (Exception)
                {
                    Task.Delay(interval).Wait();
                }
            }

            onAction.Invoke();
        }

        /// <summary>
        /// Invokes an action with retry.
        /// If an exception that occured is a transient error or the invoker wants to retry, retry several times.
        /// </summary>
        /// <typeparam name="TResult">The type of invoking the method result.</typeparam>
        /// <param name="execute">The invoking action.</param>
        /// <param name="transientExceptionFilter">
        /// The check logic whether the exception thrown is a transient exception.
        /// Retry invoking the action, if the check result is <c>true</c>. Otherwise; throws the exception.
        /// </param>
        /// <param name="retrySpans">The time spans to retry. The unit of them is milliseconds.</param>
        /// <returns>An execution result, if the execution is success. Otherwise; default value or throws the exception.</returns>
        public static TResult? InvokeWithRetry<TResult>(Func<TResult> execute, Predicate<Exception> transientExceptionFilter, params ushort[] retrySpans)
        {
            if (transientExceptionFilter is null)
            {
                return InvokeWithRetry(execute, retrySpans);
            }

            if (execute is null)
            {
                return default;
            }

            var intervals = retrySpans.IsNullOrEmpty() ? DefaultSpans : retrySpans;
            foreach (var interval in intervals)
            {
                try
                {
                    return execute.Invoke();
                }
                catch (Exception ex)
                {
                    if (!transientExceptionFilter(ex))
                    {
                        // If this isn't a transient error or we shouldn't retry,
                        // rethrow the exception.
                        throw;
                    }

                    Thread.Sleep(interval);
                }
            }

            return execute.Invoke();
        }

        /// <summary>
        /// Invokes an action with retry. If an exception is occured, retry several times.
        /// </summary>
        /// <typeparam name="TResult">The type of invoking the method result.</typeparam>
        /// <param name="execute">The invoking action.</param>
        /// <param name="retrySpans">The time spans to retry. The unit of them is milliseconds.</param>
        /// <returns>An execution result, if the execution is success. Otherwise; default value or throws the exception.</returns>
        public static TResult? InvokeWithRetry<TResult>(Func<TResult> execute, params ushort[] retrySpans)
        {
            if (execute is null)
            {
                return default;
            }

            var intervals = retrySpans.IsNullOrEmpty() ? DefaultSpans : retrySpans;
            foreach (var interval in intervals)
            {
                try
                {
                    return execute.Invoke();
                }
                catch (Exception)
                {
                    Thread.Sleep(interval);
                }
            }

            return execute.Invoke();
        }

        #endregion
    }
}
