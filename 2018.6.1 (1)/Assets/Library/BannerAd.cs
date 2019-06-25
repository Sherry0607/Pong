using UnityEngine;
using System.Collections;
using System;

namespace DAP
{
    public delegate void DAPBannerAdCallback();
    public delegate void DAPBannerAdErrorCallback(string errorMessage);

    public sealed class BannerAd : IDisposable
    {

        private DAPBannerAdCallback bannerAdLoaded;
        private DAPBannerAdCallback bannerAdClicked;
        private DAPBannerAdErrorCallback bannerAdError;

        public DAPBannerAdCallback BannerAdLoaded
        {
            internal get
            {
                return this.bannerAdLoaded;
            }
            set
            {
                this.bannerAdLoaded = value;
                BannerAdBridge.Instance.OnAdLoaded(bannerAdLoaded);
            }
        }

        public DAPBannerAdCallback BannerAdClicked
        {
            internal get
            {
                return this.bannerAdClicked;
            }
            set
            {
                this.bannerAdClicked = value;
                BannerAdBridge.Instance.OnAdLoaded(bannerAdLoaded);
            }
        }

        public DAPBannerAdErrorCallback BannerAdError
        {
            internal get
            {
                return this.bannerAdError;
            }
            set
            {
                this.bannerAdError = value;
                BannerAdBridge.Instance.OnAdError(bannerAdError);
            }
        }

        AndroidJavaObject objBannerAdBridge;

        public BannerAd(int pid, AdPosition adPosition)
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                BannerAdBridge.Instance.Create(pid, adPosition, this);
                BannerAdBridge.Instance.OnAdLoaded(BannerAdLoaded);
                BannerAdBridge.Instance.OnAdError(BannerAdError);
            }
        }

        public BannerAd(int pid, int x, int y)
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                BannerAdBridge.Instance.Create(pid, x, y, this);
                BannerAdBridge.Instance.OnAdLoaded(BannerAdLoaded);
                BannerAdBridge.Instance.OnAdError(BannerAdError);
            }
        }

        ~BannerAd()
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
            BannerAdBridge.Instance.Destroy();
        }

        public void LoadAd()
        {
            BannerAdBridge.Instance.Load();
        }

        public void Show()
        {
            BannerAdBridge.Instance.Show();
        }

        public void SetPosition(int x, int y)
        {
            BannerAdBridge.Instance.SetPosition(x, y);
        }

        public void HideAd()
        {
            BannerAdBridge.Instance.Hide();
        }

        public int GetWidthInPixels()
        {
            return BannerAdBridge.Instance.GetWidthInPixels();
        }

        public int GetHeightInPixels()
        {
            return BannerAdBridge.Instance.GetHeightInPixels();
        }
    }

    internal interface IBannerAdBridge
    {
        void Create(int pid, AdPosition adPosition, BannerAd bannerAd);

        void Create(int pid, int x, int y, BannerAd bannerAd);

        void Load();

        void Show();

        void SetPosition(int x, int y);

        void Hide();

        int GetWidthInPixels();

        int GetHeightInPixels();

        void Destroy();

        void OnAdLoaded(DAPBannerAdCallback callback);

        void OnAdClicked(DAPBannerAdCallback callback);

        void OnAdError(DAPBannerAdErrorCallback callback);
    }

    internal class BannerAdBridge : IBannerAdBridge
    {

        /* Interface to Interstitial implementation */

        public static readonly IBannerAdBridge Instance;

        internal BannerAdBridge()
        {
        }

        static BannerAdBridge()
        {
            Instance = BannerAdBridge.createInstance();
        }

        private static IBannerAdBridge createInstance()
        {
            return new BannerAdBridgeAndroid();
        }

        public virtual void Create(int pid, AdPosition adPosition, BannerAd bannerAd)
        {
        }

        public virtual void Create(int pid, int x, int y, BannerAd bannerAd)
        {
        }

        public virtual void Load()
        {
        }

        public virtual void Show()
        {
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

        public virtual void Remove()
        {
        }

        public virtual int GetWidthInPixels()
        {
            return 0;
        }

        public virtual int GetHeightInPixels()
        {
            return 0;
        }

        public virtual void OnAdLoaded(DAPBannerAdCallback callback)
        {
        }

        public virtual void OnAdClicked(DAPBannerAdCallback callback)
        {
        }

        public virtual void OnAdError(DAPBannerAdErrorCallback callback)
        {
        }
    }

#if UNITY_ANDROID
    internal class BannerAdBridgeAndroid : BannerAdBridge
    {
        AndroidJavaObject objBannerAd;

        public override void Create(int pid, AdPosition adPosition, BannerAd bannerAd)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            BannerAdBridgeListenerProxy proxy = new BannerAdBridgeListenerProxy(bannerAd);
            int posititon = 0;
            if (adPosition == AdPosition.Top)
            {
                posititon = 0;
            }
            else
            {
                posititon = 1;
            }
            objBannerAd = new AndroidJavaObject("com.duapps.ad.BannerWrapper", currentActivity, pid, posititon);
            objBannerAd.Call("setBannerAdListener", proxy);
        }

        public override void Create(int pid, int x, int y, BannerAd bannerAd)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            BannerAdBridgeListenerProxy proxy = new BannerAdBridgeListenerProxy(bannerAd);
            objBannerAd = new AndroidJavaObject("com.duapps.ad.BannerWrapper", currentActivity, pid, x, y);
            objBannerAd.Call("setBannerAdListener", proxy);
        }

        public override void Load()
        {
            objBannerAd.Call("loadAd");
        }

        public override void Destroy()
        {
            objBannerAd.Call("destroy");
        }

        public override void Show()
        {
            objBannerAd.Call("show");
        }

        public override void SetPosition(int x, int y)
        {
            objBannerAd.Call("setPosition", x, y);
        }

        public override void Hide()
        {
            objBannerAd.Call("hide");
        }

        public override int GetWidthInPixels()
        {
            return objBannerAd.Call<int>("getBannerWidth");
        }

        public override int GetHeightInPixels()
        {
            return objBannerAd.Call<int>("getBannerHeight");
        }

        public override void OnAdLoaded(DAPBannerAdCallback callback)
        {
        }

        public override void OnAdClicked(DAPBannerAdCallback callback)
        {
        }

        public override void OnAdError(DAPBannerAdErrorCallback callback)
        {
        }
    }

#endif

#if UNITY_ANDROID
    internal class BannerAdBridgeListenerProxy : AndroidJavaProxy
    {
        private BannerAd bannerAd;

        public BannerAdBridgeListenerProxy(BannerAd bannerAd)
            : base("com.duapps.ad.BannerAdListener")
        {
            this.bannerAd = bannerAd;
        }


        void onAdLoaded()
        {
            if (this.bannerAd.BannerAdLoaded != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.bannerAd.BannerAdLoaded();
                });
            }
        }

        void onClick()
        {
            if (this.bannerAd.BannerAdClicked != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.bannerAd.BannerAdClicked();
                });
            }
        }

        void onError(string errorMessage)
        {
            if (this.bannerAd.BannerAdError != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.bannerAd.BannerAdError(errorMessage);
                });
            }
        }
    }

#endif

    public enum AdPosition
    {
        Top = 0,
        Bottom = 1
    }
}