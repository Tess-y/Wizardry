using HarmonyLib;
using UnityEngine.UI.ProceduralImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnboundLib;
using UnityEngine;
using Wizardry.Spells;
using Wizardry.Extensions;
using System.Collections;
using UnboundLib.GameModes;
using Wizardry.Spells.Evoker;
using Wizardry.Cards.Evoker;
using UnboundLib.Networking;

namespace Wizardry.MonoBehavors
{
    internal class EvokerDeck : MonoBehaviour
    {
        public List<Spell> spellDeck = new List<Spell>();

        public List<Spell> spellQueueu = new List<Spell>();
        public float spellCoolDownCounter = 0;
        public float shuffleCoolDownCounter = 0;
        Color red = new Color(0.5f, 0, 0, 0.7f);
        Color green = new Color(0, 0.5f, 0, 0.7f);
        public Player player;
        public GameObject UI;
        public GameObject ToolTip;
        public bool enabled;

        public void Start()
        {
            player = GetComponent<Player>();
            CardBar[] obj = (CardBar[])Traverse.Create(CardBarHandler.instance).Field("cardBars").GetValue();
            var _uiGo = GameObject.Find("/Game/UI");
            var _gameGo = _uiGo.transform.Find("UI_Game").Find("Canvas").gameObject;
            GameObject gameObject = (GameObject)Traverse.Create(obj[player.playerID]).Field("source").GetValue();
            UI = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, gameObject.transform.rotation);
            UI.transform.localScale = new Vector3(3, 3, 3);
            UI.transform.SetParent(_gameGo.transform, false);
            UI.transform.localPosition = new Vector3(-900, -475, 0);
            UI.GetComponentInChildren<TextMeshProUGUI>().fontSize = 10;
            UI.GetComponentInChildren<TextMeshProUGUI>().fontSizeMin = 8;
            UI.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            Destroy(UI.GetComponent<CardBarButton>());
            UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
            UI.SetActive(player.data.view.IsMine);

            ToolTip = new GameObject("ToolTip");
            ToolTip.transform.SetParent(UI.transform, false);
            ToolTip.transform.localPosition = new Vector3(25,30,0);
            ToolTip.GetOrAddComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
            ToolTip.GetComponentInChildren<TextMeshProUGUI>().fontSize = 20;
            ToolTip.SetActive(false);
            ToolTip.transform.SetParent(UI.transform.parent);
            ToolTip.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            UI.GetComponent<HoverEvent>().enterEvent.AddListener(() => { ToolTip.SetActive(true); });
            UI.GetComponent<HoverEvent>().exitEvent.AddListener(() => { ToolTip.SetActive(false); });
            GameModeManager.AddHook(GameModeHooks.HookPointStart, OnPointStart);
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, OnBattleStart); 
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, OnPointEnd);
        }

        public void UpdateDeck()
        {
            spellDeck.Clear();
            foreach (var _ in player.data.currentCards.Where(c => c==Evoker.card))
            {
                spellDeck.Add(new Poke());
                spellDeck.Add(new Poke());
                spellDeck.Add(new Poke());
                spellDeck.Add(new Healling_Shard());
            }
            foreach (var _ in player.data.currentCards.Where(c => c==Rebirth_Magics.card))
            {
                player.gameObject.GetOrAddComponent<EvokerDeck>().spellDeck.Add(new Bottled_Phoenix());
                player.gameObject.GetOrAddComponent<EvokerDeck>().spellDeck.Add(new Bottled_Phoenix());
            }

            foreach (Spell spell in player.data.stats.GetAdditionalData().Evoker_Spells_To_Add)
                spellDeck.Add(spell);
            foreach (Spell spell in player.data.stats.GetAdditionalData().Evoker_Spells_To_Remove)
                Wizardry.Debug(spellDeck.Remove(spell));
        } 

        public void Update()
        {
            UI.GetComponentInChildren<TextMeshProUGUI>().text = spellQueueu.Count > 0 ? spellQueueu[0].GetName().Replace(' ', '\n') : "";
            ToolTip.GetOrAddComponent<TextMeshProUGUI>().text = spellQueueu.Count > 0 ? spellQueueu[0].GetDescription() : "Your deck is empty press Q to shuffle.";
            if (spellCoolDownCounter > 0)
            {
                spellCoolDownCounter -= TimeHandler.deltaTime;
                if (spellCoolDownCounter <= 0)
                {
                    spellCoolDownCounter = 0;
                    spellQueueu.RemoveAt(0);
                    if (spellQueueu.Count > 0) UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = green;
                    else if (player.data.stats.GetAdditionalData().Evoker_AutoShuffle) ShuffleDeck();
                }
                UI.transform.rotation = Quaternion.Euler(0, 0, 360 * (spellCoolDownCounter / player.data.stats.GetAdditionalData().Evoker_SpellCoolDown));
            }
            if (shuffleCoolDownCounter > 0)
            {
                shuffleCoolDownCounter -= TimeHandler.deltaTime;
                if (shuffleCoolDownCounter <= 0)
                {
                    shuffleCoolDownCounter = 0;
                    spellQueueu = spellDeck.ToList();
                    if (spellQueueu.Count > 0) UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = green;
                    spellQueueu.Shuffle();
                }
                UI.transform.localScale = new Vector3(3, 3 - (3 * (shuffleCoolDownCounter / player.data.stats.GetAdditionalData().Evoker_ShuffleCoolDown)), 3);
                UI.GetComponentInChildren<TextMeshProUGUI>().text = "Shuffling";
            }
        }

        public void ShuffleDeck()
        {
            if (spellCoolDownCounter == 0 && shuffleCoolDownCounter == 0)
            {
                UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
                shuffleCoolDownCounter = player.data.stats.GetAdditionalData().Evoker_ShuffleCoolDown;
                spellQueueu.Clear();
            }
        }
        public void Cast()
        {
            if (spellQueueu.Count > 0 && spellCoolDownCounter == 0)
            {
                UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
                if (player.data.view.IsMine)
                {
                    NetworkingManager.RPC_Others(typeof(EvokerDeck), nameof(CastRPC), player.playerID, spellQueueu[0].GetName());
                    spellQueueu[0].CastAction(player);
                }
                spellCoolDownCounter = player.data.stats.GetAdditionalData().Evoker_SpellCoolDown;
            }
        }

        [UnboundRPC]
        public static void CastRPC(int playerID, string spellname)
        {
            EvokerSpells.spells[spellname].CastAction(PlayerManager.instance.players.Find(p => p.playerID == playerID));
        }

        public void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPointStart, OnPointStart);
            GameModeManager.RemoveHook(GameModeHooks.HookBattleStart, OnBattleStart);
            GameModeManager.RemoveHook(GameModeHooks.HookPointEnd, OnPointEnd);
            Destroy(ToolTip.gameObject);
            Destroy(UI.gameObject);
        }

        public IEnumerator OnPointStart(IGameModeHandler gm)
        {
            try
            {
                spellCoolDownCounter = 0;
                shuffleCoolDownCounter = 0;
                this.ShuffleDeck();
            }
            catch { }
            yield break;
        }
        public IEnumerator OnBattleStart(IGameModeHandler gm)
        {
            try
            {
                enabled = true;
            }
            catch { }
            yield break;
        }
        public IEnumerator OnPointEnd(IGameModeHandler gm)
        {
            try
            {
                enabled = false;
                UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
                UI.GetComponentInChildren<TextMeshProUGUI>().text = "";
                spellQueueu.Clear();
                spellCoolDownCounter = 0;
                shuffleCoolDownCounter = 0;
                UI.transform.rotation = Quaternion.Euler(0, 0, 360 * (spellCoolDownCounter / player.data.stats.GetAdditionalData().Evoker_SpellCoolDown));
                UI.transform.localScale = new Vector3(3, 3 - (3 * (shuffleCoolDownCounter / player.data.stats.GetAdditionalData().Evoker_ShuffleCoolDown)), 3);
                ToolTip.GetOrAddComponent<TextMeshProUGUI>().text = "";
            }
            catch { }
            yield break;
        }
    }
}
