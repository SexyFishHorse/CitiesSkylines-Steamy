namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using System.Diagnostics;
    using ColossalFramework.Plugins;
    using ICities;
    using Infrastructure.Configuration;
    using JetBrains.Annotations;
    using Logger;

    [UsedImplicitly]
    public class LoadingImplementation : ILoadingExtension
    {
        private readonly ILogger logger;

        private readonly SteamController steamController;

        public LoadingImplementation()
        {
            try
            {
                var configStore = new ConfigStore("Steamy");

                logger = SteamyLogger.Instance;

                steamController = new SteamController(configStore, logger);

                logger.Info("LoadingImplementation");
            }
            catch (Exception ex)
            {
                if (logger == null)
                {
                    Debugger.Log(1, SteamyUserMod.ModName, ex.Message);
                }
                else
                {
                    logger.LogException(ex, PluginManager.MessageType.Error);
                }

                throw;
            }
        }

        public void OnCreated(ILoading loading)
        {
            try
            {
                logger.Info("On created");

                steamController.UpdatePopupPosition();
                steamController.UpdateAchievementsStatus();
            }
            catch (Exception ex)
            {
                logger.LogException(ex, PluginManager.MessageType.Error);

                throw;
            }
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                logger.Info("On level loaded");

                steamController.UpdatePopupPosition();
                steamController.UpdateAchievementsStatus();
            }
            catch (Exception ex)
            {
                logger.LogException(ex, PluginManager.MessageType.Error);

                throw;
            }
        }

        public void OnLevelUnloading()
        {
            try
            {
                logger.Info("On level unloading");
            }
            catch (Exception ex)
            {
                logger.LogException(ex, PluginManager.MessageType.Error);

                throw;
            }
        }

        public void OnReleased()
        {
            try
            {
                logger.Info("On released");
            }
            catch (Exception ex)
            {
                logger.LogException(ex, PluginManager.MessageType.Error);

                throw;
            }
        }
    }
}
