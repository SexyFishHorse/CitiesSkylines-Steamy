namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using System.Collections.Generic;
    using ColossalFramework.PlatformServices;
    using Infrastructure.Configuration;
    using Infrastructure.UI;
    using Logger;

    public class OptionsPanelManager : IOptionsPanelManager
    {
        private static readonly List<string> Positions = new List<string> { "Bottom right", "Bottom left", "Top right", "Top left", };

        private readonly ILogger logger;

        private readonly SteamController steamController;

        public OptionsPanelManager(ILogger logger, SteamController steamController)
        {
            this.logger = logger;
            this.steamController = steamController;
        }

        public void ConfigureOptionsPanel(IStronglyTypedUiHelper uiHelper)
        {
            try
            {
                var appearance = uiHelper.AddGroup("Appearance");

                appearance.AddDropDown(
                    "Popup position",
                    Positions.ToArray(),
                    ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition),
                    PositionChanged);

                var behaviour = uiHelper.AddGroup("Behaviour");
                behaviour.AddCheckBox("Enable achievements", ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements), AchievementStatusChanged);

                var debugging = uiHelper.AddGroup("Debugging");
                debugging.AddCheckBox("Enable logging", ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging), EnableLoggingChanged);

                logger.Info("OnSettingsUi");
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        private void AchievementStatusChanged(bool isEnabled)
        {
            ModConfig.Instance.SaveSetting(SettingKeys.EnableAchievements, isEnabled);

            steamController.UpdateAchievementsStatus();

            logger.Info("Achievement status enabled {0}", isEnabled);
        }

        private void EnableLoggingChanged(bool isLoggingEnabled)
        {
            ((SteamyLogger)SteamyLogger.Instance).LoggingEnabled = isLoggingEnabled;

            ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, isLoggingEnabled);

            logger.Info("Logging enabled {0}", isLoggingEnabled);
        }

        private void PositionChanged(int selectedIndex)
        {
            NotificationPosition position;
            switch (Positions[selectedIndex].ToLower())
            {
                case "top right":
                    position = NotificationPosition.TopRight;

                    break;
                case "top left":
                    position = NotificationPosition.TopLeft;

                    break;
                case "bottom left":
                    position = NotificationPosition.BottomLeft;

                    break;
                default:
                    position = NotificationPosition.BottomRight;

                    break;
            }

            ModConfig.Instance.SaveSetting(SettingKeys.PopupPosition, (int)position);

            steamController.UpdatePopupPosition();

            logger.Info("Position changed to {0}", position);
        }
    }
}
