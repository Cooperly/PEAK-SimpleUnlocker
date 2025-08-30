using HarmonyLib;

namespace SimpleUnlocker.Patches;

[HarmonyPatch(typeof(CustomizationOption), nameof(CustomizationOption.IsLocked), MethodType.Getter)]
public class UnlockCosmetics {
    public static bool Prefix(CustomizationOption __instance, ref bool __result) {
        if (!SimpleUnlocker.UnlockCosmetics.Value) {
            return true;
        }
        
        __result = false;
        return false;
    }
}