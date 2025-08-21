namespace GameDevS.Services
{
    public static class ServiceLocator
    {
        public static IAudioService AudioService { get; set; }
        public static IGameExitService GameExitService { get; set; }
    }
}
