using System;
using UnityEngine;


    public class VibratorShot
    {
        public static VibratorShot instance;
        private AndroidJavaObject javaObject;
        private AndroidJavaObject currentActivity;
        public static VibratorShot _instance()
        {
            if (instance!=null )
            {
                instance = new VibratorShot();
            }
            return instance;
        }

        public void vibrator(string num)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                Debug.Log("震动里面");
                javaObject = new AndroidJavaObject("com.yeleegame.click.click.MainActivity", context);
                javaObject .Call("UnityCallShake",num);
            }));
        }
    }

