using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//电子邮件puhalskijsemen@gmail.com
//源码网站 开vpn打开 http://web3incubators.com/
//电报https://t.me/gamecode999
namespace MergeBeast {
    public class VipBenefitItem : MonoBehaviour {
        [SerializeField] VipConfig config;
        [Header("Permanent")]
        [SerializeField] RectTransform permanentObj;
        [SerializeField] RectTransform permanentParent;
        int rowHeight = 50;

        [Header("Login")]
        [SerializeField] RectTransform loginObj;
        [SerializeField] RectTransform loginParent;

        [Header("One time")]
        [SerializeField] Text boostChestText;
        [SerializeField] Text timeJump2Text;
        [SerializeField] Text timeJump4Text;

        [SerializeField] Button btnClaim;

        private List<int> listVipRewarded = new List<int>();

        private string orange = "FFFA07";

        Animator anim;

        private void Awake() {
            anim = btnClaim.GetComponent<Animator>();
        }

        private void OnEnable() {
            SetUpPermanent();
            SetUpLogin();
            SetUpOneTime();

            listVipRewarded.Clear();

            string vipReward = PlayerPrefs.GetString(StringDefine.VIP_REWARDED, "2-2-2-2-2-2-2-2-2-2");
            string[] list = vipReward.Split('-');
            for(int i = 0; i < list.Length; i++) {
                listVipRewarded.Add(int.Parse(list[i]));
            }

            int currentStatus = listVipRewarded[config.index];
            if (currentStatus == 0) {
                //du dieu kien nhung chua nhan
                btnClaim.GetComponentInChildren<Text>().text = "Claim";
            } else if (currentStatus == 1) {
                //da nhan
                btnClaim.GetComponentInChildren<Text>().text = "Claimed";
            } else if (currentStatus == 2) {
                //ko du dieu kien nhan
                if (CPlayer.vipPoint >= config.min) {
                    currentStatus = 0;
                }
                btnClaim.GetComponentInChildren<Text>().text = "Claim";
            }

            anim.SetBool("scale", currentStatus == 0 ? true : false);
            btnClaim.interactable = currentStatus == 0;
            if (btnClaim.interactable) {
                btnClaim.onClick.RemoveAllListeners();
                btnClaim.onClick.AddListener(() => Claim());
            }

        }

        void Claim() {
            //PlayerPrefs.SetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.BOOST_CHEST, 20);
            //PlayerPrefs.SetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.TIME_JUMP_4H, 2);
            //PlayerPrefs.SetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.TIME_JUMP_8H, 2);
            if (config.boostChest > 0) {
                int amount = PlayerPrefs.GetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.BOOST_CHEST);
                amount += config.boostChest;
                PlayerPrefs.SetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.BOOST_CHEST, amount);
            }
            if(config.timeJump2 > 0) {

                int amount = PlayerPrefs.GetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.TIME_JUMP_2H);
                amount += config.timeJump2;
                PlayerPrefs.SetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.TIME_JUMP_2H, amount);
            }
            if(config.timeJump4 > 0) {
                int amount = PlayerPrefs.GetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.TIME_JUMP_4H);
                amount += config.timeJump4;
                PlayerPrefs.SetInt(StringDefine.DAILY_BOOST + (int)EnumDefine.DailyBoost.TIME_JUMP_4H, amount);
            }

            btnClaim.interactable = false;
            anim.SetBool("scale", false);

            listVipRewarded.Clear();
            string vipReward = PlayerPrefs.GetString(StringDefine.VIP_REWARDED, "2-2-2-2-2-2-2-2-2-2");
            string[] list = vipReward.Split('-');
            for (int i = 0; i < list.Length; i++) {
                listVipRewarded.Add(int.Parse(list[i]));
            }

            listVipRewarded[config.index] = 1;

            string s = "";
            for (int i = 0; i < listVipRewarded.Count; i++) {
                s += i < listVipRewarded.Count - 1 ? (listVipRewarded[i] + "-") : listVipRewarded[i].ToString();
            }

            PlayerPrefs.SetString(StringDefine.VIP_REWARDED, s);

            UIManager.Instance.ShowNotify("Claim Success");
            btnClaim.GetComponentInChildren<Text>().text = "Claimed";            
        }

        void SetUpPermanent() {
            if (permanentParent.childCount == 0) {

                GameObject status = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                status.GetComponentInChildren<Text>().text = "VIP status";

                //dps
                GameObject dps = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                dps.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>{0}</color> Boost DPS Bonus", config.dpsBuffAscend, orange);

                GameObject time = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                time.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>{0}</color> min Star Shop refresh delay", config.timeRefreshStarShop, orange);

                GameObject levelUpgrade = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                levelUpgrade.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>{0}</color> Lv Upgrade Bonus as Ascend", config.levelUpgradeAscend, orange);

                GameObject levelStage = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                levelStage.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>{0}</color> Lv Stage Bonus as Ascend", config.levelStageAscend, orange);

                GameObject buffMin = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                buffMin.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>{0}</color> mins per Boost in Shop", config.buffMinute, orange);

                int count1 = 6;
                if (config.quickBuy) {
                    GameObject quickBuy = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                    quickBuy.GetComponentInChildren<Text>().text = "Quick Buy and Merge button";
                    count1++;
                }
                if (config.disableVideo) {
                    GameObject disableVideo = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                    disableVideo.GetComponentInChildren<Text>().text = "Remove Interstital Ad";
                    count1++;
                }
                if (config.disableBanner) {
                    GameObject disableBanner = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                    disableBanner.GetComponentInChildren<Text>().text = "Remove Banner Ad";
                    count1++;
                }
                if (config.percentIgnoreReward > 0) {
                    GameObject reward = Instantiate(GameAssets.Instance.vipBenefitRowPrf, permanentParent);
                    reward.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>{0}%</color> chance to skip a video ad and get the reward", config.percentIgnoreReward, orange);
                    count1++;
                }

                permanentObj.sizeDelta = new Vector2(permanentObj.sizeDelta.x, rowHeight * count1 + 105);
                permanentParent.anchoredPosition = new Vector2(permanentParent.anchoredPosition.x, -(105 + rowHeight * count1 / 2));
            }
        }

        void SetUpLogin() {
            if (loginParent.childCount == 0) {
                int count = 0;
                if (config.moreMedalMerge > 0) {
                    GameObject medalMerge = Instantiate(GameAssets.Instance.vipBenefitRowPrf, loginParent);
                    medalMerge.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>+{0}</color> more Medal Merge", config.moreMedalMerge, orange);
                    count++;
                }

                if (config.moreStar > 0) {
                    GameObject medalMerge = Instantiate(GameAssets.Instance.vipBenefitRowPrf, loginParent);
                    medalMerge.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>+{0}</color> more Star", config.moreStar, orange);
                    count++;
                }

                if (config.moreAutoMerge > 0) {
                    GameObject medalMerge = Instantiate(GameAssets.Instance.vipBenefitRowPrf, loginParent);
                    medalMerge.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>+{0}</color> more Auto Merge", config.moreAutoMerge, orange);
                    count++;
                }
                if (config.rateMoreGem > 0) {
                    GameObject medalMerge = Instantiate(GameAssets.Instance.vipBenefitRowPrf, loginParent);
                    medalMerge.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>+{0}%</color> more Gem", config.rateMoreGem, orange);
                    count++;
                }
                if (config.moreBoostChest > 0) {
                    GameObject medalMerge = Instantiate(GameAssets.Instance.vipBenefitRowPrf, loginParent);
                    medalMerge.GetComponentInChildren<Text>().text = string.Format("<color=#{1}>+{0}</color> more Boost Chest", config.moreBoostChest, orange);
                    count++;
                }
                 
                if (count > 0) {
                    loginObj.sizeDelta = new Vector2(loginObj.sizeDelta.x, rowHeight * count + 105);
                    loginParent.anchoredPosition = new Vector2(loginParent.anchoredPosition.x, -(105 + rowHeight * count / 2));
                } else {
                    loginObj.gameObject.SetActive(false);
                }
            }
        }

        void SetUpOneTime() {
            if (config.boostChest > 0) {
                boostChestText.transform.parent.gameObject.SetActive(true);
                boostChestText.text = string.Format("x{0} Boost Chest", config.boostChest);
            } else {
                boostChestText.transform.parent.gameObject.SetActive(false);
            }

            if (config.timeJump2 > 0) {
                timeJump2Text.transform.parent.gameObject.SetActive(true);
                timeJump2Text.text = string.Format("x{0} Time Jump 2h", config.timeJump2);
            } else {
                timeJump2Text.transform.parent.gameObject.SetActive(false);
            }

            if (config.timeJump4 > 0) {
                timeJump4Text.transform.parent.gameObject.SetActive(true);
                timeJump4Text.text = string.Format("x{0} Time Jump 4h", config.timeJump4);
            } else {
                timeJump4Text.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}