using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DAP;
using UnityEngine.SceneManagement;

public class InterstitialAdTest : MonoBehaviour
{
	private static int INTERSTITIAL_PID = 155458;

	private InterstitialAd interstitialAd;

	void Start ()
	{
        this.interstitialAd = new InterstitialAd (INTERSTITIAL_PID);
		interstitialAd.InterstitialAdReceive = delegate() {
			Debug.Log ("InterstitialAdReceive");
		};
		interstitialAd.InterstitialAdPresent = delegate() {
			Debug.Log ("InterstitialAdPresent");
		};
		interstitialAd.InterstitialAdClicked = delegate() {
			Debug.Log ("InterstitialAdClicked");
		};
		interstitialAd.InterstitialAdDismissed = delegate() {
			Debug.Log ("InterstitialAdDismissed");
		};
		interstitialAd.InterstitialAdError = delegate(int errorCode) {
			Debug.Log ("InterstitialAdError : " + errorCode);
		};
     

        Button load_btn = GameObject.Find("Load Interstitial Button").GetComponent<Button>();
        load_btn.onClick.AddListener(delegate () {
            Debug.Log("Load Interstitial Button Clicked");
            interstitialAd.LoadAd();
        });

        Button isReadyToShow_btn = GameObject.Find("IsReadyToShow Button").GetComponent<Button>();
        isReadyToShow_btn.onClick.AddListener(delegate () {
            Debug.Log("IsReadyToShow Button Clicked " + interstitialAd.IsReadyToShow());
        });

        Button show_btn = GameObject.Find("Show Interstitial Button").GetComponent<Button>();
        show_btn.onClick.AddListener(delegate () {
            Debug.Log("Show Interstitial Button Clicked");
            if(interstitialAd.IsReadyToShow())
            {
                interstitialAd.ShowAd();
            }
        });
    }

    void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("MainScene");
		}
	}
}
