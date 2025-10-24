﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MergeBeast;
using UnityEngine.UI;
using System;
using Observer;
using MergeBeast;
namespace Tiledom
{
    public class BuyTurnController : MonoBehaviour
    {
        [SerializeField] Button btnOk;
        [SerializeField] Text txtNote;

        int countBuy;

        private void OnEnable() {
            countBuy = PlayerPrefs.GetInt(StringDefine.COUNT_BUY_TURN, 0);
            DateTime lastDay = DateTime.Parse(PlayerPrefs.GetString(StringDefine.LAST_DAY_BUY_TURN, DateTime.Today.AddDays(-1).ToString()));
            DateTime today = DateTime.Today;
            int dayDiff = (today - lastDay).Days;
            if(dayDiff > 0) {
                countBuy = 0;
            }

            txtNote.text = $"Get {Config.TILE_TURN_PER_DAY} turn for {Utils.FormatNumber((countBuy + 1 ) * 25)} gem";
            txtNote.text += $"\nYou've purchase {countBuy} time today";

        }
        void Start() {
            btnOk.onClick.AddListener(() => Buy());      
        }

        void Buy() {
            countBuy++;
            int money = countBuy * 25;
            if(Utils.GetCurrentRubyMoney() >= money) {
                Utils.AddRubyCoin(-money);
                this.PostEvent(EventID.OnUpDateMoney);
                PlayerPrefs.SetInt(StringDefine.COUNT_TILE_TURN, 5);
                

                PlayerPrefs.SetInt(StringDefine.COUNT_BUY_TURN, countBuy);
                PlayerPrefs.SetString(StringDefine.LAST_DAY_BUY_TURN, DateTime.Today.ToString());
                
                this.PostEvent(EventID.OnUpdateTileTurn);

                gameObject.SetActive(false);
            } else {
                LevelController.instance.ShowToast("Not Enough Gem");
            }
        }
    }
}