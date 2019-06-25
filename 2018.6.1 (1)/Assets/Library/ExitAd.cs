using UnityEngine;
using System;

namespace DAP
{
    public delegate void DAPExitAdBridgeCallback();
    public delegate void DAPExitAdErrorCallback(int errorCode);

    public sealed class ExitAd : IDisposable
    {
        private DAPExitAdBridgeCallback exitAdReceived;

        private DAPExitAdBridgeCallback exitAdClicked;

        private DAPExitAdBridgeCallback exitAdDismissed;

        private DAPExitAdBridgeCallback exitAdPresent;

        private DAPExitAdBridgeCallback exitAdExit;

        private DAPExitAdBridgeCallback exitAdMore;

        private DAPExitAdBridgeCallback exitAdCancel;

        private DAPExitAdErrorCallback exitAdError;

        public DAPExitAdBridgeCallback ExitAdExit
        {
            internal get
            {
                return this.exitAdExit;
            }
            set
            {
                this.exitAdExit = value;
                ExitAdBridge.Instance.OnAdExit(exitAdExit);
            }
        }

        public DAPExitAdBridgeCallback ExitAdReceived
        {
            internal get
            {
                return this.exitAdReceived;
            }
            set
            {
                this.exitAdReceived = value;
                ExitAdBridge.Instance.OnAdReceived(exitAdReceived);
            }
        }

        public DAPExitAdBridgeCallback ExitAdClicked
        {
            internal get
            {
                return this.exitAdClicked;
            }
            set
            {
                this.exitAdClicked = value;
                ExitAdBridge.Instance.OnAdClicked(exitAdClicked);
            }
        }

        public DAPExitAdBridgeCallback ExitAdDismissed
        {
            internal get
            {
                return this.exitAdDismissed;
            }
            set
            {
                this.exitAdDismissed = value;
                ExitAdBridge.Instance.OnAdDismissed(exitAdDismissed);
            }
        }

        public DAPExitAdBridgeCallback ExitAdPresent
        {
            internal get
            {
                return this.exitAdPresent;
            }
            set
            {
                this.exitAdPresent = value;
                ExitAdBridge.Instance.OnAdPresent(exitAdPresent);
            }
        }

        public DAPExitAdBridgeCallback ExitAdCanceled
        {
            internal get
            {
                return this.exitAdCancel;
            }
            set
            {
                this.exitAdCancel = value;
                ExitAdBridge.Instance.OnAdCanceled(exitAdCancel);
            }
        }

        public DAPExitAdErrorCallback ExitAdError
        {
            internal get
            {
                return this.exitAdError;
            }
            set
            {
                this.exitAdError = value;
                ExitAdBridge.Instance.OnAdError(exitAdError);
            }
        }

        AndroidJavaObject objExitAd;


        public ExitAd(int pid)
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                ExitAdBridge.Instance.Create(pid, this);
                ExitAdBridge.Instance.OnAdExit(ExitAdExit);
                ExitAdBridge.Instance.OnAdReceived(ExitAdReceived);
                ExitAdBridge.Instance.OnAdClicked(ExitAdClicked);
                ExitAdBridge.Instance.OnAdDismissed(ExitAdDismissed);
                ExitAdBridge.Instance.OnAdPresent(ExitAdPresent);
                ExitAdBridge.Instance.OnAdCanceled(ExitAdCanceled);
                ExitAdBridge.Instance.OnAdError(ExitAdError);
            }
        }

        public void Load()
        {
            ExitAdBridge.Instance.Load();
        }

        public void Fill()
        {
            ExitAdBridge.Instance.Fill();
        }

        public void Close()
        {
            ExitAdBridge.Instance.Close();
        }

        public void Show()
        {
            ExitAdBridge.Instance.Show();
        }

        public void AppExit()
        {
            ExitAdBridge.Instance.Exit();
        }

        ~ExitAd()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }

    internal interface IExitAdBridge
    {
        void Create(int pid, ExitAd exitAd);

        void Show();

        void Fill();

        void Load();

        void Close();

        void Exit();

        void OnAdExit(DAPExitAdBridgeCallback callback);

        void OnAdReceived(DAPExitAdBridgeCallback callback);

        void OnAdClicked(DAPExitAdBridgeCallback callback);

        void OnAdDismissed(DAPExitAdBridgeCallback callback);

        void OnAdPresent(DAPExitAdBridgeCallback callback);

        void OnAdCanceled(DAPExitAdBridgeCallback callback);

        void OnAdError(DAPExitAdErrorCallback callback);
    }

    internal class ExitAdBridge : IExitAdBridge
    {
        public static readonly IExitAdBridge Instance;

        internal ExitAdBridge()
        {
        }

        static ExitAdBridge()
        {
            Instance = ExitAdBridge.createInstance();
        }

        private static IExitAdBridge createInstance()
        {
            if (Application.platform != RuntimePlatform.OSXEditor)
            {
                return new ExitAdBridgeAndroid();
            }
            else
            {
                return new ExitAdBridge();
            }

        }

        public virtual void Create(int pid, ExitAd exitAd)
        {
        }

        public virtual void Load()
        {
        }

        public virtual void Fill()
        {
        }

        public virtual void Show()
        {
        }

        public virtual void Close()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void OnAdExit(DAPExitAdBridgeCallback callback)
        {
        }

        public virtual void OnAdReceived(DAPExitAdBridgeCallback callback)
        {
        }

        public virtual void OnAdClicked(DAPExitAdBridgeCallback callback)
        {
        }

        public virtual void OnAdDismissed(DAPExitAdBridgeCallback callback)
        {
        }

        public virtual void OnAdPresent(DAPExitAdBridgeCallback callback)
        {
        }

        public virtual void OnAdCanceled(DAPExitAdBridgeCallback callback)
        {
        }

        public virtual void OnAdError(DAPExitAdErrorCallback callback)
        {
        }
    }

    internal class ExitAdBridgeAndroid : ExitAdBridge
    {

        AndroidJavaObject objExitAd;
        AndroidJavaObject activity;

        public override void Create(int pid, ExitAd exitAd)
        {

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                objExitAd = new AndroidJavaObject("com.duapps.ad.games.ExitSceneAd", activity, pid);
                ExitAdBridgeListenerProxy proxy = new ExitAdBridgeListenerProxy(exitAd);
                objExitAd.Call("setAdListener", proxy);
            }));
        }

        public override void Load()
        {
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objExitAd != null)
                {
                    objExitAd.Call("load");
                }
            }));
        }

        public override void Fill()
        {
            if (objExitAd != null)
            {
                objExitAd.Call("fill");
            }
        }

        public override void Close()
        {
            if (objExitAd != null)
            {
                objExitAd.Call("close");
            }
        }

        public override void Show()
        {
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                if (objExitAd != null)
                {
                    objExitAd.Call("show");
                }
            }));

        }

        public override void Exit()
        {
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                activity.Call("finish");
            }));
        }

        public override void OnAdExit(DAPExitAdBridgeCallback callback)
        {
        }

        public override void OnAdReceived(DAPExitAdBridgeCallback callback)
        {
        }

        public override void OnAdClicked(DAPExitAdBridgeCallback callback)
        {
        }

        public override void OnAdDismissed(DAPExitAdBridgeCallback callback)
        {
        }

        public override void OnAdPresent(DAPExitAdBridgeCallback callback)
        {
        }

        public override void OnAdCanceled(DAPExitAdBridgeCallback callback)
        {
        }

        public override void OnAdError(DAPExitAdErrorCallback callback)
        {
        }
    }

    internal class ExitAdBridgeListenerProxy : AndroidJavaProxy
    {
        private ExitAd exitAd;

        public ExitAdBridgeListenerProxy(ExitAd exitAd)
            : base("com.duapps.ad.games.ExitSceneAd$ExitSceneAdListener")
        {
            this.exitAd = exitAd;
        }

        void onExit()
        {
            if (this.exitAd.ExitAdExit != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdExit();
                });
            }
        }

        void onAdLoaded()
        {
            if (this.exitAd.ExitAdReceived != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdReceived();
                });
            }
        }

        void onAdClicked()
        {
            if (this.exitAd.ExitAdClicked != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdClicked();
                });
            }
        }

        void onAdFail(int errorCode)
        {
            if (this.exitAd.ExitAdError != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdError(errorCode);
                });
            }
        }

        void onAdDismissed()
        {
            if (this.exitAd.ExitAdDismissed != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdDismissed();
                });
            }
        }

        void onAdPresent()
        {
            if (this.exitAd.ExitAdPresent != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdPresent();
                });
            }
        }

        void onCancel()
        {
            if (this.exitAd.ExitAdCanceled != null)
            {
                Loom.QueueOnMainThread(() =>
                {
                    this.exitAd.ExitAdCanceled();
                });
            }
        }
    }
}