namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System.Collections.Generic;
    using ColossalFramework.Steamworks;
    using ICities;
    using Infrastructure;
    using Infrastructure.Configuration;
    using Infrastructure.UI;
    using Logger;

    public class SteamyUserMod : IUserModWithOptionsPanel
    {
        public const string SettingKeyPopupPosition = "PopupPosition";

        private static readonly List<string> Positions = new List<string>
        {
            "Top left",
            "Top Right",
            "Bottom left",
            "Bottom Right"
        };

        private readonly IConfigStore configStore;

        private readonly ILogger logger;

        public SteamyUserMod()
        {
            configStore = new ConfigStore("Steamy");

            logger = LogManager.Instance.GetOrCreateLogger("Steamy");
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
                return "Steamy";
            }
        }

        public void OnSettingsUI(UIHelperBase uiHelperBase)
        {
            var uiHelper = uiHelperBase.AsStronglyTyped();

            var appearance = uiHelper.AddGroup("Appearance");

            appearance.AddDropDown(
                "Popup position",
                Positions.ToArray(),
                configStore.GetSetting<int>(SettingKeyPopupPosition),
                PositionChanged);

            logger.Info("OnSettingsUi");
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

            configStore.SaveSetting(SettingKeyPopupPosition, (int)position);
            logger.Info("PositionChanged");
        }
    }
}
