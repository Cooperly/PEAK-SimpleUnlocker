using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace SimpleUnlocker.Patches;

[HarmonyPatch(typeof(CharacterCustomization), nameof(CharacterCustomization.TryGetCosmeticsFromSteam))]
public static class SelectSash {
    [UsedImplicitly]
    private static IEnumerable<CodeInstruction> Transpiler(
        IEnumerable<CodeInstruction> instructions,
        ILGenerator generator
    ) {
        var matcher = new CodeMatcher(instructions, generator);

        matcher.MatchStartForward(
            new CodeMatch(
                OpCodes.Call,
                AccessTools.PropertyGetter(typeof(AchievementManager), nameof(AchievementManager.Instance))
            ),
            new CodeMatch(OpCodes.Ldc_I4_S, (sbyte) STEAMSTATTYPE.MaxAscent),
            new CodeMatch(
                it => it.opcode == OpCodes.Ldloca_S && it.operand is LocalBuilder { LocalIndex: 7 },
                "ldloca.s 7"
            ),
            new CodeMatch(
                OpCodes.Callvirt,
                AccessTools.Method(typeof(AchievementManager), nameof(AchievementManager.GetSteamStatInt))
            ),
            new CodeMatch(
                instruction =>  instruction.Branches(out _),
                "brfalse"
            )
        );

        if (matcher.ReportFailure(null, Debug.LogError)) {
            throw new Exception();
        }

        matcher.SetOpcodeAndAdvance(OpCodes.Nop);
        matcher.SetOpcodeAndAdvance(OpCodes.Nop);
        matcher.Advance(1);
        matcher.SetOperandAndAdvance(
            AccessTools.Method(typeof(SelectSash), nameof(TryGetSashIndex))
        );

        return matcher.Instructions();
    }

    private static bool TryGetSashIndex(out int index) {
        if (SimpleUnlocker.Sash.Value == SimpleUnlocker.SashColor.Default) {
            return AchievementManager.Instance.GetSteamStatInt(STEAMSTATTYPE.MaxAscent, out index);
        }

        index = (int) SimpleUnlocker.Sash.Value;
        return true;
    }
}