using InControl;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Wizardry.Extensions
{
        [Serializable]
        public class PlayerActionsAdditionalData
        {
            public PlayerAction SpellBind1;
            public PlayerAction SpellBind2;
            public PlayerAction SpellBind3;
            public PlayerAction SpellBind4;


            public PlayerActionsAdditionalData()
            {
                SpellBind1 = null;
                SpellBind2 = null;
                SpellBind3 = null;
                SpellBind4 = null;
            }
        }
        public static class PlayerActionsExtension
        {
            public static readonly ConditionalWeakTable<PlayerActions, PlayerActionsAdditionalData> data =
                new ConditionalWeakTable<PlayerActions, PlayerActionsAdditionalData>();

            public static PlayerActionsAdditionalData GetAdditionalData(this PlayerActions playerActions)
            {
                return data.GetOrCreateValue(playerActions);
            }

            public static void AddData(this PlayerActions playerActions, PlayerActionsAdditionalData value)
            {
                try
                {
                    data.Add(playerActions, value);
                }
                catch (Exception) { }
            }
        }
}
