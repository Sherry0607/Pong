using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DAP;
using UnityEngine.SceneManagement;

public class AppsFlyerTest : MonoBehaviour
{
    private int mLevel = 1;

    void Start()
    {

        Debug.Log("#####Start");
        
        Button start_game_btn = GameObject.Find("Start App Button").GetComponent<Button>();
        start_game_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Start App Button Clicked");
            DuAppsFlyerLog.StartApp();
        });

        Button reborn_video_btn = GameObject.Find("Reborn By Video Button").GetComponent<Button>();
        reborn_video_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Reborn By Video Button Clicked");
            DuAppsFlyerLog.RebornByWatchVideo();
        });

        Button reborn_currency_btn = GameObject.Find("Reborn By Currency Button").GetComponent<Button>();
        reborn_currency_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Reborn By Currency Button Clicked");
            DuAppsFlyerLog.RebornByVirtualCurrency();
        });

        Button level_achieved_btn = GameObject.Find("Level Achieved Button").GetComponent<Button>();
        level_achieved_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Level Achieved Button Clicked");
            DuAppsFlyerLog.LevelAchieved(mLevel++);
        });

        Button click_video_btn = GameObject.Find("Click Video Button").GetComponent<Button>();
        click_video_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Click Video Button Clicked");
            DuAppsFlyerLog.ClickRewardVideo();
        });

        Button click_interstitial_btn = GameObject.Find("Click Interstitial Button").GetComponent<Button>();
        click_interstitial_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Click Interstitial Button Clicked");
            DuAppsFlyerLog.ClickInterstitialAd();
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
