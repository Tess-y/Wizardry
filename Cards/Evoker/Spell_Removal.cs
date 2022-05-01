using HarmonyLib;
using UnityEngine.UI.ProceduralImage;
using ItemShops.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnboundLib;
using UnityEngine;
using Wizardry.Extensions;
using Wizardry.MonoBehavors;
using Wizardry.Spells;

namespace Wizardry.Cards.Evoker
{
    internal class Spell_Removal : Template
    {
        public static CardInfo card;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            base.SetupCard(cardInfo, gun, cardStats, statModifiers, block);
            className.className = EvokerClass.name;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().Evoker_Removal = true;
            base.OnAddCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }

        protected override GameObject GetCardArt()
        {
            return null;        
        }

        protected override string GetDescription()
        {
            return "Sometimes you need a little deck thinning";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[0];
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }

        public static IEnumerator RemovalScreen()
        {
            try
            {
                Wizardry.instance.Evoker_Removal_Shop = ShopManager.instance.CreateShop("Evoker_Removal");
                Wizardry.instance.Evoker_Removal_Shop.UpdateTitle("Card Removal");
                Wizardry.instance.Evoker_Removal_Shop.UpdateMoneyColumnName("");
            }
            catch { }
            foreach (Player player in PlayerManager.instance.players)
            {
                if (player.data.stats.GetAdditionalData().Evoker_Removal)
                {
                    Wizardry.instance.Evoker_Removal_Shop.RemoveAllItems();
                    int i = 0;
                    foreach(Spell spell in player.gameObject.GetOrAddComponent<EvokerDeck>().spellDeck)
                    {
                        Wizardry.instance.Evoker_Removal_Shop.AddItem("Spell" + ++i, new CardRemovalItem(spell));
                    }
                    while (player.data.stats.GetAdditionalData().Evoker_Removal)
                    {
                        if (!ShopManager.instance.PlayerIsInShop(player)) Wizardry.instance.Evoker_Removal_Shop.Show(player);
                        yield return null;
                    }
                }
            }
            yield break;
        }
    }

    public class CardRemovalItem : Purchasable
    {
        public CardRemovalItem(Spell spell)
        {
            this.spell = spell;
        }

        private Spell spell;

        public override string Name => spell.GetName();

        public override Dictionary<string, int> Cost => new Dictionary<string, int>();

        public override Tag[] Tags => new Tag[0];

        public override bool CanPurchase(Player player)
        {
            return true;
        }

        public override GameObject CreateItem(GameObject parent)
        {

            CardBar[] obj = (CardBar[])Traverse.Create(CardBarHandler.instance).Field("cardBars").GetValue();
            GameObject gameObject = (GameObject)Traverse.Create(obj[0]).Field("source").GetValue();
            GameObject UI = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, gameObject.transform.rotation);

            UI.transform.localScale = new Vector3(10, 10, 3);
            UI.GetComponentInChildren<TextMeshProUGUI>().fontSize = 10;
            UI.GetComponentInChildren<TextMeshProUGUI>().fontSizeMin = 8;
            UI.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            GameObject.Destroy(UI.GetComponent<CardBarButton>());
            UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = new Color(0.5f, 0.5f, 0, 0.7f);
            UI.GetComponentInChildren<TextMeshProUGUI>().text = spell.GetDescription();
            UI.SetActive(true);
            UI.transform.SetParent(parent.transform);
            return UI;
        }

        public override void OnPurchase(Player player, Purchasable item)
        {
            player.data.stats.GetAdditionalData().Evoker_Spells_To_Remove.Add(((CardRemovalItem)item).spell);
            player.data.stats.GetAdditionalData().Evoker_Removal = false;
            player.gameObject.GetOrAddComponent<EvokerDeck>().UpdateDeck();
            Wizardry.instance.Evoker_Removal_Shop.Hide();
        }
    }
}
