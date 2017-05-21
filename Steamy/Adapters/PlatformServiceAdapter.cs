namespace SexyFishHorse.CitiesSkylines.Steamy.Adapters
{
    using ColossalFramework.PlatformServices;
    using SexyFishHorse.CitiesSkylines.Logger;

    public class PlatformServiceAdapter
    {
        private readonly ILogger logger;

        public PlatformServiceAdapter(ILogger logger)
        {
            this.logger = logger;
        }

        public void SetPopupPosition(int position)
        {
            var notificationPosition = (NotificationPosition)position;

            PlatformService.SetOverlayNotificationPosition(notificationPosition);

            logger.Info("Changed popup position to {0}", position);
        }
    }
}
