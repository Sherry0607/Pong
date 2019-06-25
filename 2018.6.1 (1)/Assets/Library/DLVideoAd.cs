using UnityEngine;
using System.Collections;
using System;

namespace DAP
{
    public delegate void DAPDLVideoAdCallback();
    public delegate void DAPDLVideoAdErrorCallback(string errorMessage);

    public sealed class DLVideoAd : IDisposable
    {

        private DAPDLVideoAdCallback dlVideoAdLoaded;
        private DAPDLVideoAdCallback dlVideoAdClicked;
        private DAPDLVideoAdErrorCallback dlVideoAdError;

        public DAPDLVideoAdCallback DLVideoAdLoaded
        {
            internal get
            {
                return this.dlVideoAdLoaded;
            }
            set
            {
                this.dlVideoAdLoaded = value;
                DLVideoAdBridge.Instance.OnAdLoaded(dlVideoAdLoaded);
            }
        }

        public DAPDLVideoAdCallback DLVideoAdClicked
        {
            internal get
            {
                return this.dlVideoAdClicked;
            }
            set
            {
                this.dlVideoAdClicked = value;
                DLVideoAdBridge.Instance.OnAdLoaded(dlVideoAdLoaded);
            }
        }

        public DAPDLVideoAdErrorCallback DLVideoAdError
        {
            internal get
            {
                return this.dlVideoAdError;
            }
            set
            {
                this.dlVideoAdError = value;
                DLVideoAdBridge.Instance.OnAdError(dlVideoAdError);
            }
        }

        AndroidJavaObject objDLVideoAdBridge;

        public DLVideoAd(int x, int y)
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                DLVideoAdBridge.Instance.Create(x, y, this);
                DLVideoAdBridge.Instance.OnAdLoaded(DLVideoAdLoaded);
                DLVideoAdBridge.Instance.OnAdError(DLVideoAdError);
            }
        }

        ~DLVideoAd()
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
            DLVideoAdBridge.Instance.Destroy();
        }

        public void LoadAd()
        {
            DLVideoAdBridge.Instance.Load();
        }

        public bool IsReadyToShow()
        {
            return DLVideoAdBridge.Instance.IsReadyToShow();
        }

        public void Show()
        {
            DLVideoAdBridge.Instance.Show();
        }

        public void SetPosition(int x, int y)
        {
            DLVideoAdBridge.Instance.SetPosition(x, y);
        }

        public void HideAd()
        {
            DLVideoAdBridge.Instance.Hide();
        }
    }

    internal interface IDLVideoAdBridge
    {
        void Create(int x, int y, DLVideoAd dlVideoAd);

        void Load();

        bool IsReadyToShow();

        void Show();

        void SetPosition(int x, int y);

        void Hide();

        void Destroy();

        void OnAdLoaded(DAPDLVideoAdCallback callback);

        void OnAdClicked(DAPDLVideoAdCallback callback);

        void OnAdError(DAPDLVideoAdErrorCallback callback);
    }

    internal class DLVideoAdBridge : IDLVideoAdBridge
    {

        /* Interface to Interstitial implementation */

        public static readonly IDLVideoAdBridge Instance;

        internal DLVideoAdBridge()
        {
        }

        static DLVideoAdBridge()
        {
            Instance = DLVideoAdBridge.createInstance();
        }

        private static IDLVideoAdBridge createInstance()
        {
            return new DLVideoAdBridgeAndroid();
        }

        public virtual void Create(int x, int y, DLVideoAd dlVideoAd)
        {
        }

        public virtual void Load()
        {
        }

        public virtual void Show()
        {
        }

        public virtual bool IsReadyToShow()
        {
            return false;
        }

        public virtual void SetPosition(int x, int y)
        {
        }

        public virtual void Hide()
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual void OnAdLoaded(DAPDLVideoAdCallback callback)
        {
        }

        public virtual void OnAdClicked(DAPDLVideoAdCallback callback)
        {
        }

        public virtual void OnAdError(DAPDLVideoAdErrorCallback callback)
        {
        }
    }

    internal class DLVideoAdBridgeAndroid : DLVideoAdBridge
    {
        AndroidJavaObject objDLVideoAd;

        public override void Create(int x, int y, DLVideoAd dlVideoAd)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            DLVideoAdBridgeListenerProxy proxy = new DLVideoAdBridgeListenerProxy(dlVideoAd);
            objDLVideoAd = new AndroidJavaObject("com.dianxinos.dl.ad.video.DLVideoAdWrapper", currentActivity, x, y);
            objDLVideoAd.Call("setAdListener", proxy);
        }

        public override void Load()
        {
            objDLVideoAd.Call("loadAd");
        }

        public override bool IsReadyToShow()
        {
            if (objDLVideoAd != null)
            {
                return objDLVideoAd.Call<bool>("isReadyToShow");
            }
            return false;
        }

        public override void Destroy()
        {
            objDLVideoAd.Call("destroy");
        }

        public override void Show()
        {
            objDLVideoAd.Call("show");
        }

        public override void SetPosition(int x, int y)
        {
            objDLVideoAd.Call("setPosition", x, y);
        }

        public override void Hide()
        {
            objDLVideoAd.Call("hide");
        }

        public override void OnAdLoaded(DAPDLVideoAdCallback callback)
        {
        }

        public override void OnAdClicked(DAPDLVideoAdCallback callback)
        {
        }

        public override void OnAdError(DAPDLVideoAdErrorCallback callback)
        {
        }
    }


    internal class DLVideoAdBridgeListenerProxy : AndroidJavaProxy
    {
        private DLVideoAd dlVideoAd;

        public DLVideoAdBridgeListenerProxy(DLVideoAd dlVideoAd)
            : base("com.dianxinos.dl.ad.video.VideoAdListener")
        {
            this.dlVideoAd = dlVideoAd;
        }


        void onAdLoaded(AndroidJavaObject ad)
        {
            if (this.dlVideoAd.DLVideoAdLoaded != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.dlVideoAd.DLVideoAdLoaded();
                });
            }
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            if (this.dlVideoAd.DLVideoAdClicked != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.dlVideoAd.DLVideoAdClicked();
                });
            }
        }

        void onError(AndroidJavaObject ad, AndroidJavaObject adError)
        {
            if (this.dlVideoAd.DLVideoAdError != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    string errorMessage = adError.Call<string>("getErrorMessage");
                    this.dlVideoAd.DLVideoAdError(errorMessage);
                });
            }
        }
    }

}