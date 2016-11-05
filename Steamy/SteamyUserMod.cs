namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using ColossalFramework.Plugins;
    using ColossalFramework.Steamworks;
    using ICities;
    using Infrastructure;
    using Infrastructure.Configuration;
    using Infrastructure.UI;
    using Logger;

    public class SteamyUserMod : IUserModWithOptionsPanel
    {
        public const string ModName = "Steamy";

        private static readonly List<string> Positions = new List<string>
        {
            "Top left",
            "Top Right",
            "Bottom left",
            "Bottom Right"
        };

        private readonly IConfigStore configStore;

        private ILogger logger;

        private readonly SteamController steamController;

        public SteamyUserMod()
        {
            try
            {
                configStore = new ConfigStore(ModName);
                logger = SteamyLogger.Instance;

                steamController = new SteamController(configStore, logger);

                logger.Info("SteamyUserMod");
            }
            catch (Exception ex)
            {
                if (logger == null)
                {
                    Debugger.Log(1, ModName, ex.Message);
                }
                else
                {
                    logger.LogException(ex, PluginManager.MessageType.Error);
                }

                throw;
            }
        }

        public string Description
        {
            get
            {
                return "Configure how Steam integrates with Cities Skylines";
            }
        }

        public string Name
        {
            get
            {
                return ModName;
            }
        }

        public void OnSettingsUI(UIHelperBase uiHelperBase)
        {
            try
            {
                var uiHelper = uiHelperBase.AsStronglyTyped();

                var appearance = uiHelper.AddGroup("Appearance");

                appearance.AddDropDown(
                    "Popup position",
                    Positions.ToArray(),
                    configStore.GetSetting<int>(SettingKeys.PopupPosition),
                    PositionChanged);

                var behaviour = uiHelper.AddGroup("Behaviour");
                behaviour.AddCheckBox("Enable achievements", GetAchievementStatus(), AchievementStatusChanged);

                var debugging = uiHelper.AddGroup("Debugging");
                debugging.AddCheckBox("Enable logging", configStore.GetSetting<bool>(SettingKeys.EnableLogging),
                                      EnableLoggingChanged);

                logger.Info("OnSettingsUi");
            }
            catch (Exception ex)
            {
                logger.LogException(ex, PluginManager.MessageType.Error);

                throw;
            }
        }

        private void AchievementStatusChanged(bool isEnabled)
        {
            configStore.SaveSetting(SettingKeys.EnableAchievements, isEnabled);

            steamController.UpdateAchievementsStatus();

            logger.Info("Achievement status enabled {0}", isEnabled);
        }

        private void EnableLoggingChanged(bool isLoggingEnabled)
        {
            ((SteamyLogger)SteamyLogger.Instance).LoggingEnabled = isLoggingEnabled;

            configStore.SaveSetting(SettingKeys.EnableLogging, isLoggingEnabled);

            logger.Info("Logging enabled {0}", isLoggingEnabled);
        }

        private bool GetAchievementStatus()
        {
            logger.Info("Get achievement status");

            return configStore.GetSetting<bool>(SettingKeys.EnableAchievements);
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

            configStore.SaveSetting(SettingKeys.PopupPosition, (int)position);

            steamController.UpdatePopupPosition();

            logger.Info("Position changed to {0}", position);
        }
    }
}
