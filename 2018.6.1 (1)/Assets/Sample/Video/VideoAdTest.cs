using UnityEngine;
using UnityEngine.UI;
using DAP;
using UnityEngine.SceneManagement;

public class VideoAdTest : MonoBehaviour
{
    private VideoAd videoAd;
    private static int VIDEO_PID = 155456;

    void Start()
    {

        Debug.Log("#####Start");

        this.videoAd = new VideoAd(VIDEO_PID);

        videoAd.VideoAdEnd = delegate (bool isSuccessfulView, bool isCallToActionClicked) {
            Debug.Log("isSuccessfulView : " + isSuccessfulView + ", isCallToActionClicked : " + isCallToActionClicked);
        };

        videoAd.VideoAdError = delegate (int errorCode) {
            Debug.Log("errorCode : " + errorCode);
        };

        videoAd.VideoAdPlayable = delegate () {
            Debug.Log("VideoAdPlayable");
        };

        videoAd.VideoAdStart = delegate () {
            Debug.Log("VideoAdStart");
        };

        videoAd.VideoAdClick = delegate () {
            Debug.Log("VideoAdClick");
        };

        videoAd.VideoAdClose = delegate () {
            Debug.Log("VideoAdClose");
        };

        videoAd.VideoCompleted = delegate () {
            Debug.Log("VideoCompleted");
        };

        Button load_btn = GameObject.Find("Load Video Button").GetComponent<Button>();
        load_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("Load Video Button Clicked");
            videoAd.LoadAd();
        });

        Button display_btn = GameObject.Find("Display Video Button").GetComponent<Button>();
        display_btn.onClick.AddListener(delegate ()
        {
            Debug.Log("videoAd.IsAdPlayable() :" + videoAd.IsAdPlayable());
            if (videoAd.IsAdPlayable())
            {
                videoAd.PlayAdVideo();
            }
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
