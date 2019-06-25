using UnityEngine;
using System;

namespace DAP
{
    public sealed class DuAppsFlyerLog : IDisposable
    {
        public static void StartApp()
        {
            DuAppsFlyerLogBridge.Instance.StartApp();
        }

        public static void RebornByWatchVideo()
        {
            DuAppsFlyerLogBridge.Instance.RebornByWatchVideo();
        }

        public static void RebornByVirtualCurrency()
        {
            DuAppsFlyerLogBridge.Instance.RebornByVirtualCurrency();
        }

        public static void LevelAchieved(int level)
        {
            DuAppsFlyerLogBridge.Instance.LevelAchieved(level);
        }

        public static void ClickRewardVideo()
        {
            DuAppsFlyerLogBridge.Instance.ClickRewardVideo();
        }

        public static void ClickInterstitialAd()
        {
            DuAppsFlyerLogBridge.Instance.ClickInterstitialAd();
        }

        ~DuAppsFlyerLog()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean iAmBeingCalledFromDisposeAndNotFinalize)
        {
        }
    }

    internal interface IDuAppsFlyerLogBridge
    {
        void StartApp();
        void RebornByWatchVideo();
        void RebornByVirtualCurrency();
        void LevelAchieved(int level);
        void ClickRewardVideo();
        void ClickInterstitialAd();
    }

    internal class DuAppsFlyerLogBridge : IDuAppsFlyerLogBridge
    {

        public static readonly IDuAppsFlyerLogBridge Instance;

        internal DuAppsFlyerLogBridge()
        {
        }

        static DuAppsFlyerLogBridge()
        {
            Instance = DuAppsFlyerLogBridge.createInstance();
        }

        private static IDuAppsFlyerLogBridge createInstance()
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                return new DuAppsFlyerLogBridgeAndroid();
            }
            else
            {
                return new DuAppsFlyerLogBridge();
            }
        }

        public virtual void StartApp()
        {
        }

        public virtual void RebornByWatchVideo()
        {
        }

        public virtual void RebornByVirtualCurrency()
        {
        }

        public virtual void LevelAchieved(int level)
        {
        }

        public virtual void ClickRewardVideo()
        {
        }

        public virtual void ClickInterstitialAd()
        {
        }
    }

    internal class DuAppsFlyerLogBridgeAndroid : DuAppsFlyerLogBridge
    {
        AndroidJavaClass clzDuAppsFlyerLog = new AndroidJavaClass("com.dulogevent.DuAppsFlyerLog");

        public override void StartApp()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            clzDuAppsFlyerLog.CallStatic("startApp", context);
        }

        public override void RebornByWatchVideo()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            clzDuAppsFlyerLog.CallStatic("rebornByWatchVideo", context);
        }

        public override void RebornByVirtualCurrency()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            clzDuAppsFlyerLog.CallStatic("rebornByVirtualCurrency", context);
        }

        public override void LevelAchieved(int level)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            clzDuAppsFlyerLog.CallStatic("levelAchieved", context, level);
        }

        public override void ClickRewardVideo()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            clzDuAppsFlyerLog.CallStatic("clickRewardVideo", context);
        }

        public override void ClickInterstitialAd()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            clzDuAppsFlyerLog.CallStatic("clickInterstitialAd", context);
        }
    }
}