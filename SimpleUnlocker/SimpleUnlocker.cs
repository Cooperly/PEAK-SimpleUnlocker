using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace SimpleUnlocker;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public class SimpleUnlocker : BaseUnityPlugin {
    public enum SashColor {
        /// Use the sash color provided by the game.
        Default = -1,
        
        Blue = 0,
        Green = 1,
        LightGreen = 2,
        Black = 3,
        Brown = 4,
        Red = 5,
        Purple = 6,
        Silver = 7,
        Gold = 8
    }

    private const string ModGUID = "club.freewifi.void.SimpleUnlocker";
    private const string ModName = "SimpleUnlocker";
    private const string ModVersion = "1.1.0";
    
    public static ConfigEntry<bool> UnlockCosmetics = null!;
    public static ConfigEntry<bool> UnlockAscents = null!;
    
    private void Awake() {
        // Register BepInEx config
        UnlockCosmetics = Config.Bind(
            "General",
            "Unlock Cosmetics",
            true,
            "Unlocks all Cosmetics within the game.");
        
        UnlockAscents = Config.Bind(
            "General",
            "Unlock Ascents",
            false,
            "Unlocks all Ascents within the game.");

        var harmony = new Harmony(ModGUID);
        harmony.PatchAll();
    }
}
