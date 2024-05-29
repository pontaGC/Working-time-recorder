using System.Collections;

namespace SharedLibraries.Logging
{
    /// <summary>
    /// The logger collection. This is a read-only collection.
    /// </summary>
    public class LoggerCollection : ILoggerCollection, ILoggerRegistrar
    {
        #region Fields

        private readonly Dictionary<string, ILogger> logggers = new Dictionary<string, ILogger>();
        private readonly object syncRoot;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerCollection"/> class.
        /// </summary>
        public LoggerCollection()
        {
            this.syncRoot = ((ICollection)this.logggers).SyncRoot;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public int Count
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.logggers.Count;
                }
            }
        }

        #endregion

        #region Explict interface implementation

        /// <inheritdoc />
        void ILoggerRegistrar.Register(ILoggerProvider loggerProvider)
        {
            if (loggerProvider is null)
            {
                throw new ArgumentNullException(nameof(loggerProvider));
            }

            lock (this.syncRoot)
            {
                var loggers = loggerProvider.Provide();
                this.logggers.Add(loggers.Name, loggers);
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (this.syncRoot)
            {
                return ((IEnumerable)this.logggers.Values).GetEnumerator();
            }
        }

        #endregion

        #region Public Mehtods

        /// <inheritdoc />
        public ILogger Resolve(string loggerName)
        {
            ArgumentException.ThrowIfNullOrEmpty(loggerName);

            lock (this.syncRoot)
            {
                if (this.logggers.TryGetValue(loggerName, out var logger))
                {
                    return logger;
                }
            }

            return EmptyLogger.Instance;
        }

        /// <inheritdoc />
        public IEnumerator<ILogger> GetEnumerator()
        {
            lock (this.syncRoot)
            {
                return this.logggers.Values.GetEnumerator();
            }
        }

        #endregion
    }
}
