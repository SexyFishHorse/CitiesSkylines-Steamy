namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using ColossalFramework.Plugins;
    using Infrastructure.Configuration;
    using Logger;

    public class SteamyLogger : ILogger
    {
        private static ILogger instance;

        private readonly ILogger logger;

        private bool disposed;

        private SteamyLogger()
        {
            var configStore = new ConfigStore(SteamyUserMod.ModName);

            LoggingEnabled = configStore.GetSetting<bool>(SettingKeys.EnableLogging);

            logger = LogManager.Instance.GetOrCreateLogger(SteamyUserMod.ModName);
        }

        public static ILogger Instance
        {
            get
            {
                return instance ?? (instance = new SteamyLogger());
            }
        }

        public bool LoggingEnabled { get; set; }

        public void Dispose()
        {
            logger.Dispose();

            disposed = true;
        }

        public void Error(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Error(message, args);
            }
        }

        public void Info(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Info(message, args);
            }
        }

        public void Log(PluginManager.MessageType messageType, string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Log(messageType, message, args);
            }
        }

        public void LogException(Exception exception, PluginManager.MessageType messageType)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.LogException(exception, messageType);
            }
        }

        public void Warn(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Warn(message, args);
            }
        }

        private void EnsureNotDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
