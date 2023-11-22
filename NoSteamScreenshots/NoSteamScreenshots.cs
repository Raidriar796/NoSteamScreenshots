using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;

namespace NoSteamScreenshots;

public class NoSteamScreenshots : ResoniteMod
{
    public override string Name => "NoSteamScreenshots";
    public override string Author => "Raidriar796";
    public override string Version => "1.0.1";
    public override string Link => "https://github.com/Raidriar796/NoSteamScreenshots";
    public static ModConfiguration? Config;

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<bool> Enabled =
        new ModConfigurationKey<bool>(
            "Enabled",
            "Allow photos to save to Steam.",
            () => false);

    public override void OnEngineInit()
    {
        Harmony harmony = new("net.raidriar796.NoSteamScreenshots");
        Config = GetConfiguration();
        Config!.Save(true);
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(SteamConnector), "NotifyOfScreenshot")]
    class ScreenshotPatch
    {
        static bool Prefix() 
        {
            if (Config.GetValue(Enabled)) return true;

            return false;
        }
    }
}
