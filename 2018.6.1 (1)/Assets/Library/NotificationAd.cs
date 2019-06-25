using UnityEngine;
using System;

namespace DAP
{
    public delegate void NotificationCallback();

    public sealed class NotificationAd : IDisposable
    {

        public static NotificationCallback OnRewarded;
        public static NotificationCallback OnAdClosed;

        public static void Load(int sid)
        {
            DuAdNetworkBridge.Instance.LoadNotification(sid);
        }

        ~NotificationAd()
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

    internal interface IDuAdNetworkBridge
    {
        void LoadNotification(int sid);
    }

    internal class DuAdNetworkBridge : IDuAdNetworkBridge
    {

        public static readonly IDuAdNetworkBridge Instance;

        internal DuAdNetworkBridge()
        {
        }

        static DuAdNetworkBridge()
        {
            Instance = DuAdNetworkBridge.createInstance();
        }

        private static IDuAdNetworkBridge createInstance()
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                return new DuAdNetworkBridgeAndroid();
            }
            else
            {
                return new DuAdNetworkBridge();
            }
        }

        public virtual void LoadNotification(int sid)
        {
        }
    }

    internal class DuAdNetworkBridgeAndroid : DuAdNetworkBridge
    {
        public override void LoadNotification(int sid)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                NotificationAdBridgeListenerProxy proxy = new NotificationAdBridgeListenerProxy();
                AndroidJavaClass clzDuAdNetwork = new AndroidJavaClass("com.duapps.ad.base.DuAdNetwork");
                AndroidJavaObject objDuAdNetwork = clzDuAdNetwork.CallStatic<AndroidJavaObject>("getInstance");
                objDuAdNetwork.Call("setNotificationParams", sid, proxy);

                clzDuAdNetwork.CallStatic("setAdmobTestDeviceId", "2A0CE08B4721F86128A6341376AEDBB1");//2A0CE08B4721F86128A6341376AEDBB1//F61EF1473AF0E4F457696559BED2D6FE//E7820F7F6BBAEFBCBFE35B40B2A71DD9
            }));
        }
    }

    internal class NotificationAdBridgeListenerProxy : AndroidJavaProxy
    {

        public NotificationAdBridgeListenerProxy()
            : base("com.dianxinos.outerads.ad.notification.INotificationCallback")
        {
        }

        void onRewarded()
        {
            Loom.QueueOnMainThread(() =>
            {
                if (NotificationAd.OnRewarded != null)
                {
                    NotificationAd.OnRewarded();
                }
            });
        }

        void onAdClosed()
        {
            Loom.QueueOnMainThread(() =>
            {
                if (NotificationAd.OnAdClosed != null)
                {
                    NotificationAd.OnAdClosed();
                }
            });
        }
    }
}