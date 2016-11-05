namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System.Collections.Generic;
    using ColossalFramework;
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

        public SteamyUserMod()
        {
            configStore = new ConfigStore(ModName);

            logger = SteamyLogger.Instance;
            logger.Info("SteamyUserMod");
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
            var uiHelper = uiHelperBase.AsStronglyTyped();

            var appearance = uiHelper.AddGroup("Appearance");

            appearance.AddDropDown(
                "Popup position",
                Positions.ToArray(),
                configStore.GetSetting<int>(SettingKeys.PopupPosition),
                PositionChanged);

            var debugging = uiHelper.AddGroup("Debugging");
            debugging.AddCheckBox("Enable logging", configStore.GetSetting<bool>(SettingKeys.EnableLogging),
                                  EnableLoggingChanged);

            logger.Info("OnSettingsUi");
        }

        private void EnableLoggingChanged(bool isLoggingEnabled)
        {
            ((SteamyLogger)SteamyLogger.Instance).LoggingEnabled = isLoggingEnabled;

            configStore.SaveSetting(SettingKeys.EnableLogging, isLoggingEnabled);

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

            configStore.SaveSetting(SettingKeys.PopupPosition, (int)position);
            logger.Info("Position changed to {0}", position);
        }
    }
}
