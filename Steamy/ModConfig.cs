namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using Infrastructure.Configuration;

    public static class ModConfig
    {
        private static ConfigurationManager instance;

        public static ConfigurationManager Instance
        {
            get
            {
                return instance ?? (instance = ConfigurationManagerFactory.GetOrCreateConfigurationManager(SteamyUserMod.ModName));
            }
        }
    }
}
