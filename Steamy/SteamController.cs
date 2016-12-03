namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using ColossalFramework;
    using ColossalFramework.PlatformServices;
    using Logger;

    public class SteamController
    {
        private readonly ILogger logger;

        public SteamController(ILogger logger)
        {
            this.logger = logger;
        }

        public void UpdateAchievementsStatus()
        {
            var disableAchievements = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements)
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

        public void UpdatePopupPosition()
        {
            var notificationPosition = (NotificationPosition)ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition);
            PlatformService.SetOverlayNotificationPosition(notificationPosition);
            logger.Info("Changed popup position to {0}", notificationPosition);
        }
    }
}
