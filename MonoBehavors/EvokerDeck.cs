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

namespace Wizardry.MonoBehavors
{
    internal class EvokerDeck : MonoBehaviour
    {
        public List<Spell> spellDeck = new List<Spell>();

        public List<Spell> spellQueueu = new List<Spell>();
        public float spellCoolDown = 2.5f;
        public float spellCoolDownCounter = 0;
        public float shuffleCoolDown = 2.5f;
        public float shuffleCoolDownCounter = 0;
        Color red = new Color(0.5f, 0, 0, 0.7f);
        Color green = new Color(0, 0.5f, 0, 0.7f);
        public Player player;
        public GameObject UI;

        public void Start()
        {
            player = GetComponent<Player>();
            CardBar[] obj = (CardBar[])Traverse.Create(CardBarHandler.instance).Field("cardBars").GetValue(); var _uiGo = GameObject.Find("/Game/UI");
            var _gameGo = _uiGo.transform.Find("UI_Game").Find("Canvas").gameObject;
            GameObject gameObject = (GameObject)Traverse.Create(obj[player.playerID]).Field("source").GetValue();
            UI = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, gameObject.transform.rotation);
            UI.transform.localScale = new Vector3(3,3,3);
            UI.transform.SetParent(_gameGo.transform,false);
            UI.transform.localPosition = new Vector3(-900, -475, 0);
            UI.GetComponentInChildren<TextMeshProUGUI>().fontSize = 10;
            UI.GetComponentInChildren<TextMeshProUGUI>().fontSizeMin = 8;
            UI.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            Destroy(UI.GetComponent<CardBarButton>());
            UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
            UI.SetActive(player.data.view.IsMine);
        }

        public void Update()
        {
            UI.GetComponentInChildren<TextMeshProUGUI>().text = spellQueueu.Count > 0? spellQueueu[0].GetName().Replace(' ', '\n') : "";
            if (spellCoolDownCounter > 0)
            {
                spellCoolDownCounter -= TimeHandler.deltaTime;
                if(spellCoolDownCounter <= 0)
                {
                    spellCoolDownCounter = 0;
                    spellQueueu.RemoveAt(0);
                    if(spellQueueu.Count > 0) UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = green;
                }
                UI.transform.rotation = Quaternion.Euler(0,0,360* (spellCoolDownCounter/spellCoolDown));
            }
            if(shuffleCoolDownCounter > 0)
            {
                shuffleCoolDownCounter -= TimeHandler.deltaTime;
                if(shuffleCoolDownCounter <= 0)
                {
                    shuffleCoolDownCounter = 0;
                    spellQueueu = spellDeck.ToList();
                    if (spellQueueu.Count > 0) UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = green;
                    spellQueueu.Shuffle();
                }
                UI.transform.localScale = new Vector3(3, 3-(3* (shuffleCoolDownCounter / shuffleCoolDown)), 3);
                UI.GetComponentInChildren<TextMeshProUGUI>().text = "Shuffling";
            }
        }

        public void ShuffleDeck()
        {
            UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
            shuffleCoolDownCounter = shuffleCoolDown;
            spellQueueu.Clear();
        }
        public void Cast()
        {
            if (spellQueueu.Count > 0 && spellCoolDownCounter == 0)
            {
                UI.transform.Find("CarrdOrange").GetComponentInChildren<ProceduralImage>().color = red;
                spellQueueu[0].CastAction(player);
                spellCoolDownCounter = spellCoolDown;
            }
        }
    }
}
