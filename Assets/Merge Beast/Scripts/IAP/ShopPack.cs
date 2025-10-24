using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Observer;
using UnityEngine.Events;

namespace MergeBeast
{
    public class ShopPack : MonoBehaviour
    {
        public IAPScr iapController;
        public EnumDefine.ShopPack pack;
        public Button btnBuy;
        public int totalDay;
        public Text TxtPrice;

        private Animator anim;

        private void Awake()
        {
            anim = btnBuy.GetComponent<Animator>();
        }

        
        private void Start()
        {
            btnBuy.onClick.AddListener(ClickBuy);
            //this.RegisterListener(EventID.OnPurchaseSuccess, (sender, param) => OnPurchaseSuccess(param.ToString()));
        }

        private void ClickBuy()
        {
            //if (GetProductId() == null)
            //{
            //    Debug.Log("Get package null");
            //    return;
            //}
            //IAPManager.Instance.BuyPackage(GetProductId());
            // OnPurchaseSuccess(GetProductId());
            AdsManager.Instance.ShowAds(() =>
            {
                Utils.AddRubyCoin(200);


            }, "", EnumDefine.ADSTYPE.Rewarded);
        }

        private void OnPurchaseSuccess(string productId)
        {
            string currentProductId = GetProductId();
            if (productId.Equals(currentProductId))
            {
                if (pack == EnumDefine.ShopPack.Month)
                {
                    BuyMonthSuccess();
                }
                else if (pack == EnumDefine.ShopPack.Week1)
                {
                    BuyWeek1Success();
                }
                else if (pack == EnumDefine.ShopPack.Week2)
                {
                    BuyWeek2Success();
                }

                this.PostEvent(EventID.OnUpDateMoney);
            }
        }

        private void BuyMonthSuccess()
        {
            bool isBuy = PlayerPrefs.GetInt(StringDefine.BUY_MONTH_CARD, 0) == 1;
            if (!isBuy)
            {
                PlayerPrefs.SetInt(StringDefine.BUY_MONTH_CARD, 1);
                CPlayer.AddVipPoint(1000);
                iapController.UpdateVipPoint();
            }

            PlayerPrefs.SetString(StringDefine.DAY_CLAIM_MONTH_CARD, DateTime.Today.ToString());

            Utils.AddRubyCoin(200);
            Utils.AddMedalMerge(100, true);
            BoostManager.Instance.AddFreeBoost(EnumDefine.FREEBOOST.AUTO_MERGE, 6 * 3600, 0);
            BoostManager.Instance.AddBoost(EnumDefine.BOOST.DAMAGE_BOOST_1, 6 * 3600);

            btnBuy.interactable = false;
            btnBuy.GetComponentInChildren<Text>().text = "Claim";
            anim.SetBool("scale", false);

            if (IsMaximum(StringDefine.DAY_COUNT_MONTH))
            {
                gameObject.SetActive(false);
            }

        }

        private void BuyWeek1Success()
        {
            bool isBuy = PlayerPrefs.GetInt(StringDefine.BUY_WEEK_CARD_1, 0) == 1;
            if (!isBuy)
            {
                PlayerPrefs.SetInt(StringDefine.BUY_WEEK_CARD_1, 1);
                CPlayer.AddVipPoint(500);
                iapController.UpdateVipPoint();
            }

            PlayerPrefs.SetString(StringDefine.DAY_CLAIM_WEEK_CARD_1, DateTime.Today.ToString());

            Utils.AddRubyCoin(250);
            BoostManager.Instance.AddFreeBoost(EnumDefine.FREEBOOST.AUTO_MERGE, 6 * 3600, 0);


            btnBuy.interactable = false;
            btnBuy.GetComponentInChildren<Text>().text = "Claim";
            anim.SetBool("scale", false);

            if (IsMaximum(StringDefine.DAY_COUNT_WEEK_1))
            {
                gameObject.SetActive(false);
            }

        }

        private void BuyWeek2Success()
        {
            bool isBuy = PlayerPrefs.GetInt(StringDefine.BUY_WEEK_CARD_2, 0) == 1;
            if (!isBuy)
            {
                PlayerPrefs.SetInt(StringDefine.BUY_WEEK_CARD_2, 1);
                CPlayer.AddVipPoint(2000);
                iapController.UpdateVipPoint();

                BoostManager.Instance.AddBoost(EnumDefine.BOOST.DAMAGE_BOOST_1, 15 * 24 * 3600);
            }

            PlayerPrefs.SetString(StringDefine.DAY_CLAIM_WEEK_CARD_2, DateTime.Today.ToString());
            Utils.AddRubyCoin(500);

            btnBuy.interactable = false;
            btnBuy.GetComponentInChildren<Text>().text = "Claim";
            anim.SetBool("scale", false);

            if (IsMaximum(StringDefine.DAY_COUNT_WEEK_2))
            {
                gameObject.SetActive(false);
            }
        }

        private bool IsMaximum(string key)
        {
            int daycount = PlayerPrefs.GetInt(key, 0);
            daycount++;
            PlayerPrefs.SetInt(key, daycount);
            return daycount >= totalDay;
        }

        private string GetProductId()
        {
            if (pack == EnumDefine.ShopPack.Month)
            {
                return StringDefine.MONTH_CARD;
            }
            if (pack == EnumDefine.ShopPack.Week1)
            {
                return StringDefine.WEEKLY_1;
            }
            if (pack == EnumDefine.ShopPack.Week2)
            {
                return StringDefine.WEEKLY_2;
            }
            return null;
        }


    }
}
