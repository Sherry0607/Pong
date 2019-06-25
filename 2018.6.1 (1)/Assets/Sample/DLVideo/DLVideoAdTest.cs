using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DAP;
using UnityEngine.SceneManagement;

public class DLVideoAdTest : MonoBehaviour
{
	private DLVideoAd dlVideoAd;

	void Start ()
	{

		Debug.Log ("#####Start");

        this.dlVideoAd = new DLVideoAd(200,200);
        dlVideoAd.DLVideoAdLoaded = delegate() {
			Debug.Log ("DLVideoAdLoaded");
		};
        dlVideoAd.DLVideoAdError = delegate(string errorMessage) {
			Debug.Log ("DLVideoAdError : " + errorMessage);
		};
        dlVideoAd.DLVideoAdClicked = delegate() {
			Debug.Log ("DLVideoAdClicked");
		};

        Button load_btn = GameObject.Find("Load DLVideo Button").GetComponent<Button>();
        load_btn.onClick.AddListener(delegate () {
            Debug.Log("Load Banner Button Clicked");
            dlVideoAd.LoadAd();
        });

        Button show_btn = GameObject.Find("Show DLVideo Button").GetComponent<Button>();
        show_btn.onClick.AddListener(delegate () {
            Debug.Log("Show Banner Button Clicked");
            dlVideoAd.Show();
        });

        Button hide_btn = GameObject.Find("Hide DLVideo Button").GetComponent<Button>();
        hide_btn.onClick.AddListener(delegate () {
            Debug.Log("Hide Banner Button Clicked");
            dlVideoAd.HideAd();
        });

        Button position_btn = GameObject.Find("Set DLVideo Position").GetComponent<Button>();
        position_btn.onClick.AddListener(delegate () {
            Debug.Log("Set Banner Button Clicked");
            dlVideoAd.SetPosition(500, 500);
        });

        Button isReadyToShow_btn = GameObject.Find("IsReadyToShow Button").GetComponent<Button>();
        isReadyToShow_btn.onClick.AddListener(delegate () {
            Debug.Log("IsReadyToShow Clicked " + dlVideoAd.IsReadyToShow());
        });
    }

    void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("MainScene");
		}
	}

	void OnDestroy ()
	{
		Debug.Log ("destroy");
        if(dlVideoAd != null) {
            dlVideoAd.Dispose();
        }
    }
}
