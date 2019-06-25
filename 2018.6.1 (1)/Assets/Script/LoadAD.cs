using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAP;
public class LoadAD : MonoBehaviour
{
    private VideoAd videoAd;
    private BannerAd BannerAd;
    private InterstitialAd interstitialAd;
    private static int Banner_PID = 166553;//155457//166553
    private static int VIDEO_PID = 166554;//155456//166554
    private static int INTERSTITIAL_PID = 166556;//155458//166556

    void Awake()
    {

    }
    void Start()
    {
        this.BannerAd = new BannerAd(Banner_PID, AdPosition.Bottom);
        this.videoAd = new VideoAd(VIDEO_PID);
        this.interstitialAd = new InterstitialAd(INTERSTITIAL_PID);
        BannerAd.LoadAd();
        InvokeRepeating("LoadBanner",0.1f,30f);//延迟1秒执行，每次执行间隔30秒
        Loadinterstitial();
        LoadVideo();
    }
    //加载三个广告，banner 每隔5秒加载一次
    //banner是在加载完场景之后直接show，skip是点击按钮触发，
    //插屏广告是每关结束
    // Update is called once per frame
    public void   Loadinterstitial()
    {
        interstitialAd.LoadAd();
    }

    public void LoadVideo()
    {
        videoAd.LoadAd();
    }

    public void LoadBanner()
    {
        BannerAd.Show();
        BannerAd.LoadAd();
    }

    public void showBanner()
    {
        BannerAd.LoadAd();
        BannerAd .Show();
    }

    void Update()
    {
        
    }
}
