using UnityEngine;
using System;

namespace DAP
{
    public sealed class AppRater : IDisposable
    {

        public static void ShowRatingDialog()
        {
            AppRaterBridge.Instance.ShowRatingDialog();
        }

        public static void SetFirstShowTime(int hours)
        {
            AppRaterBridge.Instance.SetFirstShowTime(hours);
        }

        ~AppRater()
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

    internal interface IAppRaterBridge
    {
        void ShowRatingDialog();
        void SetFirstShowTime(int hours);
    }

    internal class AppRaterBridge : IAppRaterBridge
    {

        public static readonly IAppRaterBridge Instance;

        internal AppRaterBridge()
        {
        }

        static AppRaterBridge()
        {
            Instance = AppRaterBridge.createInstance();
        }

        private static IAppRaterBridge createInstance()
        {
            return new AppRaterBridgeAndroid();
        }

        public virtual void ShowRatingDialog()
        {
        }

        public virtual void SetFirstShowTime(int hours)
        {
        }
    }

    internal class AppRaterBridgeAndroid : AppRaterBridge
    {
        AndroidJavaObject currentActivity;

        public override void ShowRatingDialog()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass clzAppRater = new AndroidJavaClass("com.apprater.AppRater");
                clzAppRater.CallStatic("showRatingDialogUntilPermit", currentActivity);
            }));
        }

        public override void SetFirstShowTime(int hours)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass clzAppRater = new AndroidJavaClass("com.apprater.AppRater");
                clzAppRater.CallStatic("setFirstShowTime", hours);
            }));
        }
    }
}