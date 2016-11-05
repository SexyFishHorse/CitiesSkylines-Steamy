namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using ColossalFramework;
    using ColossalFramework.Steamworks;
    using Infrastructure.Configuration;
    using Logger;

    public class SteamController
    {
        private readonly IConfigStore configStore;

        private readonly ILogger logger;

        public SteamController(IConfigStore configStore, ILogger logger)
        {
            this.configStore = configStore;
            this.logger = logger;
        }

        public void UpdateAchievementsStatus()
        {
            if (configStore.HasSetting(SettingKeys.EnableAchievements))
            {
                var disableAchievements = configStore.GetSetting<bool>(SettingKeys.EnableAchievements)
                                              ? SimulationMetaData.MetaBool.False
                                              : SimulationMetaData.MetaBool.True;
                if (Singleton<SimulationManager>.exists && (Singleton<SimulationManager>.instance.m_metaData != null))
                {
                    Singleton<SimulationManager>.instance.m_metaData.m_disableAchievements = disableAchievements;
                    logger.Info("Changed achievements disabled to {0}", disableAchievements);
                }
                else
                {
                    logger.Warn("Simulation manager or metadata not available (This warning can be safely ignored if you're in the main menu)");
                }
            }
        }

        public void UpdatePopupPosition()
        {
            if (configStore.HasSetting(SettingKeys.PopupPosition))
            {
                var notificationPosition = (NotificationPosition)configStore.GetSetting<int>(SettingKeys.PopupPosition);
                Steam.SetOverlayNotificationPosition(notificationPosition);
                logger.Info("Changed popup position to {0}", notificationPosition);
            }
        }
    }
}
