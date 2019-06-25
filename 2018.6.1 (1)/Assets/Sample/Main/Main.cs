using UnityEngine;
using UnityEngine.UI;
using DAP;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    private static int EXIT_PID = 160108;
    private static int NOTIFICATION_PID = 156785;

    private ExitAd exitAd;
	private bool exitAdInit;

	void Start ()
	{
		Debug.Log ("==============Start");
        Loom.Current.StartUp();

        NotificationAd.Load (NOTIFICATION_PID);
		NotificationAd.OnRewarded = delegate() {
			Debug.Log ("notification onRewarded");
		};

        NotificationAd.OnAdClosed = delegate () {
            Debug.Log("notification onAdClosed");
        };

        Button video_btn = GameObject.Find ("Video Ad Button").GetComponent<Button> ();
		video_btn.onClick.AddListener (delegate() {  
			Debug.Log ("Video Ad Button Click");
			SceneManager.LoadScene ("VideoAdScene");
		});

        Button intersitial_btn = GameObject.Find("Intersitial Ad Button").GetComponent<Button>();
        intersitial_btn.onClick.AddListener(delegate () {
            Debug.Log("Intersitial Ad Button Click");
            SceneManager.LoadScene("InterstitialAdScene");
        });

        Button banner_btn = GameObject.Find("Banner Ad Button").GetComponent<Button>();
        banner_btn.onClick.AddListener(delegate () {
            Debug.Log("Banner Ad Button Click");
            SceneManager.LoadScene("BannerAdScene");
        });

        Button dlVideo_btn = GameObject.Find("DLVideo Ad Button").GetComponent<Button>();
        dlVideo_btn.onClick.AddListener(delegate () {
            Debug.Log("DLVideo Ad Button Click");
            SceneManager.LoadScene("DLVideoAdScene");
        });

        Button rater_btn = GameObject.Find("Show App Rater Button").GetComponent<Button>();
        rater_btn.onClick.AddListener(delegate () {
            Debug.Log("Show App Rater Button Click");
            AppRater.ShowRatingDialog();
        });

        AppRater.SetFirstShowTime(0);

        Button exit_btn = GameObject.Find ("Exit Ad Button").GetComponent<Button> ();
		exit_btn.onClick.AddListener (delegate() {  
			Debug.Log ("Exit Ad Button Click");
			this.exitAd = new ExitAd (EXIT_PID);
			exitAdInit = true;
			exitAd.ExitAdError = delegate(int errorCode) {
				Debug.Log ("ExitAdError : " + errorCode);
			};

			exitAd.ExitAdReceived = delegate() {
				Debug.Log ("ExitAdReceived");
			};

			exitAd.ExitAdCanceled = delegate() {
				Debug.Log ("ExitAdCanceled");
			};

			exitAd.ExitAdClicked = delegate() {
				Debug.Log ("ExitAdClicked");
			};

			exitAd.ExitAdDismissed = delegate() {
				Debug.Log ("ExitAdDismissed");
			};

			exitAd.ExitAdExit = delegate() {
				Debug.Log ("ExitAdExit");
                Application.Quit ();
			};

			exitAd.ExitAdPresent = delegate() {
				Debug.Log ("ExitAdPresent");
			};
			exitAd.Load ();	
		});

        Button appsflyer_btn = GameObject.Find("AppsFlyer Button").GetComponent<Button>();
        appsflyer_btn.onClick.AddListener(delegate () {
            Debug.Log("AppsFlyer Button Click");
            SceneManager.LoadScene("AppsFlyerScene");
        });

        Button duRank_btn = GameObject.Find("Du Rank Button").GetComponent<Button>();
        duRank_btn.onClick.AddListener(delegate () {
            Debug.Log("Du Rank Button Click");
            SceneManager.LoadScene("DuRankScene");
        });
    }

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && exitAdInit) {
			Debug.Log ("GetKeyDown Escape");
			exitAd.Show ();
		}
	}
}
