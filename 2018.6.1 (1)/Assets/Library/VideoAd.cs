using UnityEngine;
using System;

namespace DAP
{
    public delegate void DAPVideoAdNormalCallback();
    public delegate void DAPVideoAdEndCallback(bool isSuccessfulView, bool isCallToActionClicked);
    public delegate void DAPVideoAdErrorCallback(int errorCode);

    public sealed class VideoAd : IDisposable
    {
        private DAPVideoAdNormalCallback videoAdStart;
        private DAPVideoAdNormalCallback videoAdClick;
        private DAPVideoAdNormalCallback videoAdClose;
        private DAPVideoAdNormalCallback videoAdCompleted;
        private DAPVideoAdErrorCallback videoAdError;
        private DAPVideoAdEndCallback videoAdEnd;
        private DAPVideoAdNormalCallback videoAdPlayable;
        public bool isAdPlayable;

        public DAPVideoAdNormalCallback VideoAdStart
        {
            internal get
            {
                return this.videoAdStart;
            }
            set
            {
                this.videoAdStart = value;
                VideoAdBridge.Instance.OnAdStart(videoAdStart);
            }
        }

        public DAPVideoAdNormalCallback VideoAdClick
        {
            internal get
            {
                return this.videoAdClick;
            }
            set
            {
                this.videoAdClick = value;
                VideoAdBridge.Instance.OnAdClick(videoAdClick);
            }
        }

        public DAPVideoAdNormalCallback VideoAdClose
        {
            internal get
            {
                return this.videoAdClose;
            }
            set
            {
                this.videoAdClose = value;
                VideoAdBridge.Instance.OnAdClose(videoAdClose);
            }
        }

        public DAPVideoAdNormalCallback VideoCompleted
        {
            internal get
            {
                return this.videoAdCompleted;
            }
            set
            {
                this.videoAdCompleted = value;
                VideoAdBridge.Instance.OnVideoCompleted(videoAdCompleted);
            }
        }

        public DAPVideoAdErrorCallback VideoAdError
        {
            internal get
            {
                return this.videoAdError;
            }
            set
            {
                this.videoAdError = value;
                VideoAdBridge.Instance.OnAdError(videoAdError);
            }
        }

        public DAPVideoAdEndCallback VideoAdEnd
        {
            internal get
            {
                return this.videoAdEnd;
            }
            set
            {
                this.videoAdEnd = value;
                VideoAdBridge.Instance.OnAdEnd(videoAdEnd);
            }
        }

        public DAPVideoAdNormalCallback VideoAdPlayable
        {
            internal get
            {
                return this.videoAdPlayable;
            }
            set
            {
                this.videoAdPlayable = value;
                VideoAdBridge.Instance.OnAdPlayable(videoAdPlayable);
            }
        }

        AndroidJavaObject objVideoAdBridge;

        public VideoAd(int pid)
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                VideoAdBridge.Instance.Create(pid, this);
                VideoAdBridge.Instance.OnAdStart(VideoAdStart);
                VideoAdBridge.Instance.OnAdError(VideoAdError);
                VideoAdBridge.Instance.OnAdEnd(VideoAdEnd);
                VideoAdBridge.Instance.OnAdPlayable(VideoAdPlayable);
            }
        }

        ~VideoAd()
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
            VideoAdBridge.Instance.Destroy();
        }

        public void LoadAd()
        {
            VideoAdBridge.Instance.Load();
        }

        public void PlayAdVideo()
        {
            VideoAdBridge.Instance.PlayAd();
        }

        public bool IsAdPlayable()
        {
            return this.isAdPlayable;
        }
    }

    internal interface IVideoAdBridge
    {
        void Create(int pid, VideoAd videoAd);

        void Load();

        void PlayAd();

        void Destroy();

        void OnAdEnd(DAPVideoAdEndCallback callback);

        void OnAdStart(DAPVideoAdNormalCallback callback);

        void OnAdClick(DAPVideoAdNormalCallback callback);

        void OnAdClose(DAPVideoAdNormalCallback callback);

        void OnVideoCompleted(DAPVideoAdNormalCallback callback);

        void OnAdError(DAPVideoAdErrorCallback callback);

        void OnAdPlayable(DAPVideoAdNormalCallback callback);
    }

    internal class VideoAdBridge : IVideoAdBridge
    {

        /* Interface to Interstitial implementation */

        public static readonly IVideoAdBridge Instance;

        internal VideoAdBridge()
        {
        }

        static VideoAdBridge()
        {
            Instance = VideoAdBridge.createInstance();
        }

        private static IVideoAdBridge createInstance()
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                return new VideoAdBridgeAndroid();
            }
            else
            {
                return new VideoAdBridge();
            }

        }

        public virtual void Create(int pid, VideoAd videoAd)
        {
        }

        public virtual void Load()
        {
        }

        public virtual void PlayAd()
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual void OnAdEnd(DAPVideoAdEndCallback callback)
        {
        }

        public virtual void OnAdStart(DAPVideoAdNormalCallback callback)
        {
        }

        public virtual void OnAdClose(DAPVideoAdNormalCallback callback)
        {
        }

        public virtual void OnAdClick(DAPVideoAdNormalCallback callback)
        {
        }

        public virtual void OnVideoCompleted(DAPVideoAdNormalCallback callback)
        {
        }

        public virtual void OnAdError(DAPVideoAdErrorCallback callback)
        {
        }

        public virtual void OnAdPlayable(DAPVideoAdNormalCallback callback)
        {
        }
    }

    internal class VideoAdBridgeAndroid : VideoAdBridge
    {

        AndroidJavaObject objVideoAdBridge;
        AndroidJavaObject currentActivity;
        AndroidJavaObject context;
        VideoAd mVideoAd;

        public override void Create(int pid, VideoAd videoAd)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

            mVideoAd = videoAd;

            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass clzUnityBridge = new AndroidJavaClass("com.duapps.ad.video.DuVideoAdsManager");
                objVideoAdBridge = clzUnityBridge.CallStatic<AndroidJavaObject>("getVideoAd", context, pid);
                VideoAdBridgeListenerProxy proxy = new VideoAdBridgeListenerProxy(videoAd);
                objVideoAdBridge.Call("setListener", proxy);
            }));
        }

        public override void Load()
        {
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objVideoAdBridge != null)
                {
                    objVideoAdBridge.Call("load");
                }
            }));
        }

        public override void PlayAd()
        {
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objVideoAdBridge != null)
                {
                    mVideoAd.isAdPlayable = false;
                    objVideoAdBridge.Call("playAd", context);
                }
            }));
        }

        public override void Destroy()
        {
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objVideoAdBridge != null)
                {
                    objVideoAdBridge.Call("destroy");
                }
            }));
        }

        public override void OnAdEnd(DAPVideoAdEndCallback callback)
        {
        }

        public override void OnAdStart(DAPVideoAdNormalCallback callback)
        {
        }

        public override void OnAdClose(DAPVideoAdNormalCallback callback)
        {
        }

        public override void OnAdClick(DAPVideoAdNormalCallback callback)
        {
        }

        public override void OnVideoCompleted(DAPVideoAdNormalCallback callback)
        {
        }

        public override void OnAdError(DAPVideoAdErrorCallback callback)
        {
        }

        public override void OnAdPlayable(DAPVideoAdNormalCallback callback)
        {
        }
    }

    internal class VideoAdBridgeListenerProxy : AndroidJavaProxy
    {
        private VideoAd videoAd;

        public VideoAdBridgeListenerProxy(VideoAd videoAd)
            : base("com.duapps.ad.video.DuVideoAdListener")
        {
            this.videoAd = videoAd;
        }

        void onAdEnd(AndroidJavaObject adResult)
        {
            bool isSuccessfulView = adResult.Call<bool>("isSuccessfulView");
            bool isCallToActionClicked = adResult.Call<bool>("isCallToActionClicked");
            if (this.videoAd.VideoAdEnd != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoAdEnd(isSuccessfulView, isCallToActionClicked);
                });
            }
        }

        void onAdStart()
        {
            if (this.videoAd.VideoAdStart != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoAdStart();
                });
            }
        }

        void onAdError(AndroidJavaObject error)
        {
            int errorCode = error.Call<int>("getErrorCode");
            if (this.videoAd.VideoAdError != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoAdError(errorCode);
                });
            }
        }

        void onAdPlayable()
        {
            this.videoAd.isAdPlayable = true;
            if (this.videoAd.VideoAdPlayable != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoAdPlayable();
                });
            }
        }

        void onAdClick()
        {
            if (this.videoAd.VideoAdClick != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoAdClick();
                });
            }
        }

        void onAdClose()
        {
            if (this.videoAd.VideoAdClose != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoAdClose();
                });
            }
        }
        void onVideoCompleted()
        {
            if (this.videoAd.VideoCompleted != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.videoAd.VideoCompleted();
                });
            }
        }
    }
}