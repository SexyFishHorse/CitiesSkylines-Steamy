namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using ColossalFramework.PlatformServices;
    using ICities;
    using Infrastructure;
    using JetBrains.Annotations;
    using Logger;

    [UsedImplicitly]
    public class SteamyUserMod : UserModBase, ILoadingExtension
    {
        public const string ModName = "Steamy";

        private readonly ILogger logger;

        private readonly SteamController steamController;

        public SteamyUserMod()
        {
            try
            {
                logger = SteamyLogger.Instance;
                steamController = new SteamController(logger);

                OptionsPanelManager = new OptionsPanelManager(logger, steamController);

                if (string.IsNullOrEmpty(ModConfig.Instance.GetSetting<string>(SettingKeys.PopupPosition)))
                {
                    ModConfig.Instance.SaveSetting(SettingKeys.PopupPosition, (int)NotificationPosition.BottomRight);
                }

                steamController.UpdateAchievementsStatus();
                steamController.UpdatePopupPosition();

                logger.Info("SteamyUserMod");
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                //throw; 
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
                logger.LogException(ex);

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
                logger.LogException(ex);

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
                logger.LogException(ex);

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
                logger.LogException(ex);

                throw;
            }
        }

        public override string Description
        {
            get
            {
                return "Configure how Steam integrates with the game";
            }
        }

        public override string Name
        {
            get
            {
                return ModName;
            }
        }
    }
}
