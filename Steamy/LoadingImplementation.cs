namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using ColossalFramework.Steamworks;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Configuration;
    using SexyFishHorse.CitiesSkylines.Logger;

    public class LoadingImplementation : ILoadingExtension
    {
        private readonly IConfigStore configStore;

        private readonly ILogger logger;

        public LoadingImplementation()
        {
            configStore = new ConfigStore("Steamy");

            logger = LogManager.Instance.GetOrCreateLogger("Steamy");
            logger.Info("LoadingImplementation");
        }

        public void OnCreated(ILoading loading)
        {
            logger.Info("On created");
            if (configStore.HasSetting(SteamyUserMod.SettingKeyPopupPosition))
            {
                Steam.SetOverlayNotificationPosition((NotificationPosition)configStore.GetSetting<int>(SteamyUserMod.SettingKeyPopupPosition));
            }
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            logger.Info("On level loaded");
            if (configStore.HasSetting(SteamyUserMod.SettingKeyPopupPosition))
            {
                Steam.SetOverlayNotificationPosition((NotificationPosition)configStore.GetSetting<int>(SteamyUserMod.SettingKeyPopupPosition));
            }
        }

        public void OnLevelUnloading()
        {
            logger.Info("On level unloading");
        }

        public void OnReleased()
        {
            logger.Info("On released");
        }
    }
}
