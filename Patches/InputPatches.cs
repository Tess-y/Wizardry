using HarmonyLib;
using InControl;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Wizardry.Extensions;
using Wizardry.MonoBehavors;

namespace Wizardry.Patches
{


    [HarmonyPatch(typeof(GeneralInput), "Update")]
    class GeneralInputPatchUpdate
    {
        private static void Postfix(GeneralInput __instance)
        {
            try
            {
                if (__instance.GetComponent<CharacterData>().playerActions.GetAdditionalData().SpellBind1.WasPressed)
                {
                    Wizardry.Debug("SpellBind1 WasPressed");
                }
                if (__instance.GetComponent<EvokerDeck>() != null && __instance.GetComponent<EvokerDeck>().enabled) {
                    if (__instance.GetComponent<CharacterData>().playerActions.GetAdditionalData().SpellBind1.WasPressed && !__instance.GetComponent<CharacterData>().stats.GetAdditionalData().Evoker_AutoShuffle)
                    {
                        __instance.GetComponent<EvokerDeck>().ShuffleDeck();
                    }
                    if (__instance.GetComponent<CharacterData>().playerActions.GetAdditionalData().SpellBind2.WasPressed)
                    {
                        __instance.GetComponent<EvokerDeck>().Cast();
                    }
                }
            }
            catch { }
        }
    }


    [HarmonyPatch(typeof(PlayerActions))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    class PlayerActionsPatchPlayerActions
    {
        private static void Postfix(PlayerActions __instance)
        {
            __instance.GetAdditionalData().SpellBind1 = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                                    BindingFlags.NonPublic, null, __instance, new object[] { "Spell Button 1" });
            __instance.GetAdditionalData().SpellBind2 = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                                    BindingFlags.NonPublic, null, __instance, new object[] { "Spell Button 2" });
            __instance.GetAdditionalData().SpellBind3 = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                                    BindingFlags.NonPublic, null, __instance, new object[] { "Spell Button 3" });
            __instance.GetAdditionalData().SpellBind4 = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                                    BindingFlags.NonPublic, null, __instance, new object[] { "Spell Button 4" });

        }
    }

    [HarmonyPatch(typeof(PlayerActions), "CreateWithControllerBindings")]
    class PlayerActionsPatchCreateWithControllerBindings
    {
        private static void Postfix(ref PlayerActions __result)
        {
            __result.GetAdditionalData().SpellBind1.AddDefaultBinding(InputControlType.DPadDown);
            __result.GetAdditionalData().SpellBind2.AddDefaultBinding(InputControlType.DPadUp);
            __result.GetAdditionalData().SpellBind3.AddDefaultBinding(InputControlType.DPadLeft);
            __result.GetAdditionalData().SpellBind4.AddDefaultBinding(InputControlType.DPadRight);
        }
    }

    [HarmonyPatch(typeof(PlayerActions), "CreateWithKeyboardBindings")]
    class PlayerActionsPatchCreateWithKeyboardBindings
    {
        private static void Postfix(ref PlayerActions __result)
        {
            __result.GetAdditionalData().SpellBind1.AddDefaultBinding(Key.Q);
            __result.GetAdditionalData().SpellBind2.AddDefaultBinding(Key.E);
            __result.GetAdditionalData().SpellBind3.AddDefaultBinding(Key.R);
            __result.GetAdditionalData().SpellBind4.AddDefaultBinding(Key.F);
        }
    }
}
