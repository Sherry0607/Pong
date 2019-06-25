using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DAP;
using UnityEngine.SceneManagement;

public class BannerAdTest : MonoBehaviour
{
    private BannerAd BannerAd;
    private static int Banner_PID = 155457;
    private int y = 1500;

    void Start()
    {

        Debug.Log("#####Start");

        this.BannerAd = new BannerAd(Banner_PID, AdPosition.Bottom);
        BannerAd.BannerAdLoaded = delegate ()
        {
            Debug.Log("BannerAdLoaded width : " + BannerAd.GetWidthInPixels() + ", height : " + BannerAd.GetHeightInPixels());
        };
        BannerAd.BannerAdError = delegate (string errorMessage)
        {
            Debug.Log("BannerAdError : " + errorMessage);
        };
        BannerAd.BannerAdClicked = delegate ()
        {
            Debug.Log("BannerAdClicked");
        };

        Button load_btn = GameObject.Find("Load Banner Button").GetComponent<Button>();
        load_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Load Banner Button Clicked");
            BannerAd.LoadAd();
        });

        Button show_btn = GameObject.Find("Show Banner Button").GetComponent<Button>();
        show_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Show Banner Button Clicked");
            BannerAd.Show();
        });

        Button hide_btn = GameObject.Find("Hide Banner Button").GetComponent<Button>();
        hide_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Hide Banner Button Clicked");
            BannerAd.HideAd();
        });

        Button position_btn = GameObject.Find("Set Banner Position").GetComponent<Button>();
        position_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Set Banner Button Clicked");
            BannerAd.SetPosition(0, 500);
        });

        Button y_btn = GameObject.Find("Banner Position Y 10").GetComponent<Button>();
        y_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Banner Position Y 10 Clicked");
            BannerAd.SetPosition(0, y);
            y = y + 10;
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void OnDestroy()
    {
        Debug.Log("destroy");
        if (BannerAd != null)
        {
            BannerAd.Dispose();
        }
    }
}
