namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using SexyFishHorse.CitiesSkylines.Steamy.Adapters;

    public class SteamController
    {
        private readonly PlatformServiceAdapter platformService;

        private readonly SimulationManagerAdapter simulationManager;

        public SteamController(PlatformServiceAdapter platformService, SimulationManagerAdapter simulationManager)
        {
            this.platformService = platformService;
            this.simulationManager = simulationManager;
        }

        public void UpdateAchievementsStatus()
        {
            var enableAchievements = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements);

            simulationManager.SetAchievementsEnabled(enableAchievements);
        }

        public void UpdatePopupPosition()
        {
            var popupPosition = ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition);

            platformService.SetPopupPosition(popupPosition);
        }
    }
}
