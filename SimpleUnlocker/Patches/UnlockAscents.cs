using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace SimpleUnlocker.Patches;

[HarmonyPatch(typeof(BoardingPass), nameof(BoardingPass.UpdateAscent))]
public static class UnlockAscents {
    private static IEnumerable<CodeInstruction> Transpiler(
        IEnumerable<CodeInstruction> instructions,
        ILGenerator generator
        ) {
            var matcher = new CodeMatcher(instructions, generator);

            matcher.MatchStartForward(
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Call, AccessTools.PropertyGetter(typeof(AchievementManager), nameof(AchievementManager.Instance))),
                new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(AchievementManager), nameof(AchievementManager.GetMaxAscent))),
                new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(BoardingPass), nameof(BoardingPass.maxUnlockedAscent)))
            );

            if (matcher.ReportFailure(null, Debug.LogError)) {
                throw new Exception();
            }

            matcher.RemoveInstructions(4);
            matcher.InsertAndAdvance(
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(UnlockAscents), (nameof(SetUnlockedAscents)))));
            return matcher.Instructions();
    }

    private static void SetUnlockedAscents(BoardingPass boardingPass) {
        if (!SimpleUnlocker.UnlockAscents.Value) {
            boardingPass.maxUnlockedAscent = AchievementManager.Instance.GetMaxAscent();
            return;
        }

        boardingPass.maxUnlockedAscent = boardingPass.maxAscent + 1;
    }
}

// IL_0000: ldarg.0      // this
// IL_0001: call         !0/*class AchievementManager*/ class [Zorro.Core.Runtime]Zorro.Core.Singleton`1<class AchievementManager>::get_Instance()
// IL_0006: callvirt     instance int32 AchievementManager::GetMaxAscent()
// IL_000b: stfld        int32 BoardingPass::maxUnlockedAscent
