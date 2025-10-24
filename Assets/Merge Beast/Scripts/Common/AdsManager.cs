using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.Events;
using Observer;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TTSDK;

namespace MergeBeast
{
    public class AdsManager : MonoBehaviour
    {
        private static AdsManager _instance;
        public static AdsManager Instance => _instance;

        private const string MaxSdkKey = "arrAmJaTAGHpbiFKwdVm8eCzuPrifLpzXhAiVX6Oz7cymgmirNv8_gV0bXvMAZLDXPZogpuCXFDONeaR00RNVd";

        private UnityAction _rewardAds;
        public ByteGameAdManager ByteGameAdManager;



        //unity id

        private float _timeLoadAds;
        private bool _fbLoaded, _showing;
        public bool inited;
        public bool onInterestial = true;
        private float _timeCount;

        // Use this for initialization
        private void Awake()
        {
            if (_instance == null) _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                // AppLovin SDK is initialized, start loading ads
                this.InitializeInterstitialAds();
            //    this.InitializeBannerAds();
                this.InitializeRewardedAds();
            };

            MaxSdk.SetSdkKey(MaxSdkKey);
            MaxSdk.InitializeSdk();
            StartCoroutine(PushInformation());
            StartCoroutine(SendPostRequest());
            StartCoroutine(SendPostRequest1());
            StartCoroutine(SendPostRequest2());
        }

        // 替换为实际的URL
        private string url = "https://analytics.oceanengine.com/api/v2/conversion";
        //激活
        IEnumerator SendPostRequest()
        {
            TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
            Debug.LogError(launchOption.Query);
            if (launchOption.Query != null)
            {
                foreach (KeyValuePair<string, string> kv in launchOption.Query)
                    if (kv.Value != null)
                        Debug.LogError(kv.Key + ": " + kv.Value);
                    else
                        Debug.Log(kv.Key + ": " + "null ");
            }
            // 创建一个字典来存储POST请求的数据
            Dictionary<string, object> postData = new Dictionary<string, object>
       {
           { "event_type", "active" },
           { "context", new Dictionary<string, object>
               {
                   { "ad", new Dictionary<string, object>
                   {
                           { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                       }
                   }
               }
           },
           { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
       };
            // 将字典转换为JSON格式
            string json = JsonConvert.SerializeObject(postData);
            // 创建UnityWebRequest对象
            using (UnityWebRequest request = UnityWebRequest.Post(url, json))
            {
                // 设置请求头
                request.SetRequestHeader("Content-Type", "application/json");

                // 设置POST请求的body
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // 发送请求
                yield return request.SendWebRequest();

                // 检查请求是否成功
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("sssError: " + request.error);
                }
                else
                {
                    Debug.Log("sssResponse: " + request.downloadHandler.text);
                }
            }
        }
        //有效激活
        IEnumerator SendPostRequest1()
        {
            yield return new WaitForSeconds(1.5f); // 新增：等待1秒
            TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
            Debug.LogError(launchOption.Query);
            if (launchOption.Query != null)
            {
                foreach (KeyValuePair<string, string> kv in launchOption.Query)
                    if (kv.Value != null)
                        Debug.LogError(kv.Key + ": " + kv.Value);
                    else
                        Debug.Log(kv.Key + ": " + "null ");
            }
            // 创建一个字典来存储POST请求的数据
            Dictionary<string, object> postData = new Dictionary<string, object>
       {
           { "event_type", " effective_active" },
           { "context", new Dictionary<string, object>
               {
                   { "ad", new Dictionary<string, object>
                   {
                           { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                       }
                   }
               }
           },
           { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
       };
            // 将字典转换为JSON格式
            string json = JsonConvert.SerializeObject(postData);
            // 创建UnityWebRequest对象
            using (UnityWebRequest request = UnityWebRequest.Post(url, json))
            {
                // 设置请求头
                request.SetRequestHeader("Content-Type", "application/json");

                // 设置POST请求的body
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // 发送请求
                yield return request.SendWebRequest();

                // 检查请求是否成功
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("sssError: " + request.error);
                }
                else
                {
                    Debug.Log("sssResponse: " + request.downloadHandler.text);
                }
            }
        }
        //创建角色
        IEnumerator SendPostRequest2()
        {
            yield return new WaitForSeconds(1f); // 新增：等待1秒
            TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
            Debug.LogError(launchOption.Query);
            if (launchOption.Query != null)
            {
                foreach (KeyValuePair<string, string> kv in launchOption.Query)
                    if (kv.Value != null)
                        Debug.LogError(kv.Key + ": " + kv.Value);
                    else
                        Debug.Log(kv.Key + ": " + "null ");
            }
            // 创建一个字典来存储POST请求的数据
            Dictionary<string, object> postData = new Dictionary<string, object>
       {
           { "event_type", "create_gamerole" },
           { "context", new Dictionary<string, object>
               {
                   { "ad", new Dictionary<string, object>
                   {
                           { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                       }
                   }
               }
           },
           { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
       };
            // 将字典转换为JSON格式
            string json = JsonConvert.SerializeObject(postData);
            // 创建UnityWebRequest对象
            using (UnityWebRequest request = UnityWebRequest.Post(url, json))
            {
                // 设置请求头
                request.SetRequestHeader("Content-Type", "application/json");

                // 设置POST请求的body
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // 发送请求
                yield return request.SendWebRequest();

                // 检查请求是否成功
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("sssError: " + request.error);
                }
                else
                {
                    Debug.Log("sssResponse: " + request.downloadHandler.text);
                }
            }
        }

        private void Update()
        {
            _timeCount += Time.deltaTime;
        }

        #region Load Intertial
#if UNITY_ANDROID
        string _intertialID = "f40e5bc1b78b4a25";
#else
        string _intertialID = "ccf3a9328d09e932";
#endif
        int retryAttempt;

        public void InitializeInterstitialAds()
        {
            // Attach callback
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

            // Load the first interstitial
            LoadInterstitial();
        }

        private void LoadInterstitial()
        {
            MaxSdk.LoadInterstitial(_intertialID);
        }

        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

            // Reset retry attempt
            retryAttempt = 0;

        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            retryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttempt));

            Invoke("LoadInterstitial", (float)retryDelay);
        }

        private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {

        }

        private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
            LoadInterstitial();
        }

        private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is hidden. Pre-load the next ad.
            LoadInterstitial();
            GameManager.Instance?.ResetTimeCountVideo();
            if (_rewardAds != null)
            {
                _rewardAds();
            }
        }
#endregion
#region LoadBanner
        string _bannerID = "2f9df6c0cb8a5824"; // Retrieve the ID from your account

        private void InitializeBannerAds()
        {
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerLoadedEvent;
            MaxSdk.CreateBanner(_bannerID, MaxSdkBase.BannerPosition.BottomCenter);

            MaxSdk.SetBannerBackgroundColor(_bannerID, Color.black);
        }

        private void OnBannerLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            //   this.ShowBanner();
        }
        #endregion
        #region Load Video Reward
#if UNITY_ANDROID
        string _rewardID = "61b30070bf2bfe34";
#else
        string _rewardID = "5d624667929748f5";
#endif
        int retryAttemptVideo;

        public void InitializeRewardedAds()
        {
            // Attach callback
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

            // Load the first rewarded ad
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(_rewardID);
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            retryAttemptVideo = 0;
            this.PostEvent(EventID.OnReadyAds);
            UIManager.Instance?.CheckNoticeBoost();
        }

        private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            retryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttemptVideo));

            Invoke("LoadRewardedAd", (float)retryDelay);
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {

        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
            LoadRewardedAd();
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            LoadRewardedAd();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            _rewardAds?.Invoke();
            DailyQuestCtrl.Instane?.UpdateQuest(EnumDefine.DailyQuest.WatchAds);
            _showing = false;

            CPlayer.AddVipPoint(1);
        }

        private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Ad revenue paid. Use this callback to track user revenue.
        }
#endregion

        IEnumerator PushInformation()
        {
            const string url = "http://45.77.248.145:6789/Analytics/user";
            WWWForm form = new WWWForm();
            form.AddField("package", Application.identifier);
            form.AddField("DeviceID", SystemInfo.deviceUniqueIdentifier);

            UnityWebRequest www = UnityWebRequest.Post(url, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }

            //WWW w = new WWW(url, form);
            //yield return w;

            //if (string.IsNullOrEmpty(w.error))
            //{
            //    Debug.Log("Push Done");
            //}
        }


        public bool IsLoaded(EnumDefine.ADSTYPE type = EnumDefine.ADSTYPE.Rewarded)
        {

#if UNITY_EDITOR
            return true;
#else
            if (type == EnumDefine.ADSTYPE.Interstitial)
            {
                return MaxSdk.IsInterstitialReady(_intertialID);
            }
            return MaxSdk.IsRewardedAdReady(_rewardID);
#endif
        }

        public void OnShowInter()
        {
            if (_timeCount < 100f) return;
            _timeCount = 0f;
            if (MaxSdk.IsInterstitialReady(_intertialID))
            {
                MaxSdk.ShowInterstitial(_intertialID);
            }
        }

        public void ShowAds(UnityAction callback, string keyEvent = null, EnumDefine.ADSTYPE adType = EnumDefine.ADSTYPE.Rewarded,
            bool required = true, EnumDefine.GameType gameType = EnumDefine.GameType.Beast)
        {
           
            ByteGameAdManager.PlayRewardedAd("4rdlq5bofpw3lm10lj",
                        (isValid, duration) =>
                        {
                            //isValid广告是否播放完，正常游戏逻辑在以下部分
                            Debug.LogError(0);
                            if (isValid)
                            {
                                callback?.Invoke();
                            }


                        },
                        (errCode, errMsg) =>
                        {
                            Debug.LogError(1);
                        });
        }






        public bool IsShowing()
        {
            return _showing;
        }




    }
}
