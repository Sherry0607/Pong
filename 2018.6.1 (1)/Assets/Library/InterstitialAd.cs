using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using AOT;

namespace DAP
{
    public delegate void DAPInterstitialAdBridgeCallback();
    public delegate void DAPInterstitialAdErrorCallback(int errorCode);

    public sealed class InterstitialAd : IDisposable
    {
        private DAPInterstitialAdBridgeCallback interstitialAdDismissed;
        private DAPInterstitialAdBridgeCallback interstitialAdReceive;
        private DAPInterstitialAdBridgeCallback interstitialAdPresent;
        private DAPInterstitialAdBridgeCallback interstitialAdClicked;
        private DAPInterstitialAdErrorCallback interstitialAdError;

        public DAPInterstitialAdBridgeCallback InterstitialAdDismissed
        {
            internal get
            {
                return this.interstitialAdDismissed;
            }
            set
            {
                this.interstitialAdDismissed = value;
                InterstitialAdBridge.Instance.OnAdDismissed(interstitialAdDismissed);
            }
        }

        public DAPInterstitialAdBridgeCallback InterstitialAdReceive
        {
            internal get
            {
                return this.interstitialAdReceive;
            }
            set
            {
                this.interstitialAdReceive = value;
                InterstitialAdBridge.Instance.OnAdReceive(interstitialAdReceive);
            }
        }

        public DAPInterstitialAdBridgeCallback InterstitialAdPresent
        {
            internal get
            {
                return this.interstitialAdPresent;
            }
            set
            {
                this.interstitialAdPresent = value;
                InterstitialAdBridge.Instance.OnAdPresent(interstitialAdPresent);
            }
        }

        public DAPInterstitialAdBridgeCallback InterstitialAdClicked
        {
            internal get
            {
                return this.interstitialAdClicked;
            }
            set
            {
                this.interstitialAdClicked = value;
                InterstitialAdBridge.Instance.OnAdClicked(interstitialAdClicked);
            }
        }

        public DAPInterstitialAdErrorCallback InterstitialAdError
        {
            internal get
            {
                return this.interstitialAdError;
            }
            set
            {
                this.interstitialAdError = value;
                InterstitialAdBridge.Instance.OnAdError(interstitialAdError);
            }
        }

        AndroidJavaObject objInterstitialAdBridge;

        public InterstitialAd(int pid)
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                InterstitialAdBridge.Instance.Create(pid, this);
                InterstitialAdBridge.Instance.OnAdDismissed(InterstitialAdDismissed);
                InterstitialAdBridge.Instance.OnAdReceive(InterstitialAdReceive);
                InterstitialAdBridge.Instance.OnAdPresent(InterstitialAdPresent);
                InterstitialAdBridge.Instance.OnAdClicked(InterstitialAdClicked);
                InterstitialAdBridge.Instance.OnAdError(InterstitialAdError);
            }
        }


        ~InterstitialAd()
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
            InterstitialAdBridge.Instance.Destroy();
        }

        public void LoadAd()
        {
            InterstitialAdBridge.Instance.Load();
        }

        public bool IsReadyToShow()
        {
            return InterstitialAdBridge.Instance.IsReadyToShow();
        }

        public void ShowAd()
        {
            InterstitialAdBridge.Instance.Show();
        }
    }

    internal interface IInterstitialAdBridge
    {

        void Create(int pid, InterstitialAd interstitialAd);

        bool IsReadyToShow();

        void Load();

        void Show();

        void Destroy();

        void OnAdDismissed(DAPInterstitialAdBridgeCallback callback);

        void OnAdReceive(DAPInterstitialAdBridgeCallback callback);

        void OnAdPresent(DAPInterstitialAdBridgeCallback callback);

        void OnAdClicked(DAPInterstitialAdBridgeCallback callback);

        void OnAdError(DAPInterstitialAdErrorCallback callback);

    }

    internal class InterstitialAdBridge : IInterstitialAdBridge
    {

        /* Interface to Interstitial implementation */

        public static readonly IInterstitialAdBridge Instance;

        internal InterstitialAdBridge()
        {
        }

        static InterstitialAdBridge()
        {
            Instance = InterstitialAdBridge.createInstance();
        }

        private static IInterstitialAdBridge createInstance()
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                return new InterstitialAdBridgeAndroid();
            }
            else
            {
                return new InterstitialAdBridge();
            }

        }

        public virtual void Create(int pid, InterstitialAd interstitialAd)
        {
        }

        public virtual void Load()
        {
        }

        public virtual bool IsReadyToShow()
        {
            return false;
        }

        public virtual void Show()
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual void OnAdDismissed(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public virtual void OnAdReceive(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public virtual void OnAdPresent(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public virtual void OnAdClicked(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public virtual void OnAdError(DAPInterstitialAdErrorCallback callback)
        {
        }
    }

    internal class InterstitialAdBridgeAndroid : InterstitialAdBridge
    {

        AndroidJavaObject objInterstitialAd;
        AndroidJavaObject currentActivity;

        public override void Create(int pid, InterstitialAd interstitialAd)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                objInterstitialAd = new AndroidJavaObject("com.duapps.ad.InterstitialAd", context, pid);
                InterstitialAdBridgeListenerProxy proxy = new InterstitialAdBridgeListenerProxy(interstitialAd);
                objInterstitialAd.Call("setInterstitialListener", proxy);
            }));
        }

        public override bool IsReadyToShow()
        {
            if (objInterstitialAd != null)
            {
                return objInterstitialAd.Call<bool>("isReadyToShow");
            }
            return false;
        }

        public override void Load()
        {
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objInterstitialAd != null)
                {
                    objInterstitialAd.Call("load");
                }
            }));
        }

        public override void Show()
        {
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objInterstitialAd != null)
                {
                    objInterstitialAd.Call("show");
                }
            }));
        }

        public override void Destroy()
        {
            if (objInterstitialAd != null)
            {
                objInterstitialAd.Call("destroy");
            }
        }

        public override void OnAdDismissed(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public override void OnAdReceive(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public override void OnAdPresent(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public override void OnAdClicked(DAPInterstitialAdBridgeCallback callback)
        {
        }

        public override void OnAdError(DAPInterstitialAdErrorCallback callback)
        {
        }
    }

    internal class InterstitialAdBridgeListenerProxy : AndroidJavaProxy
    {
        private InterstitialAd interstitialAd;

        public InterstitialAdBridgeListenerProxy(InterstitialAd interstitialAd)
            : base("com.duapps.ad.InterstitialListener")
        {
            this.interstitialAd = interstitialAd;
        }

        void onAdDismissed()
        {
            if (this.interstitialAd.InterstitialAdDismissed != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.interstitialAd.InterstitialAdDismissed();
                });
            }
        }

        void onAdReceive()
        {
            if (this.interstitialAd.InterstitialAdReceive != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.interstitialAd.InterstitialAdReceive();
                });
            }
        }

        void onAdPresent()
        {
            if (this.interstitialAd.InterstitialAdPresent != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.interstitialAd.InterstitialAdPresent();
                });
            }

        }

        void onAdClicked()
        {
            if (this.interstitialAd.InterstitialAdClicked != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.interstitialAd.InterstitialAdClicked();
                });
            }
        }

        void onAdFail(int errorCode)
        {
            if (this.interstitialAd.InterstitialAdError != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.interstitialAd.InterstitialAdError(errorCode);
                });
            }
        }
    }
}