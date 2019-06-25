using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DAP;
using UnityEngine.SceneManagement;
/// <summary>
/// 广告，每关开始load。按钮中不load，有show。没有返回游戏。
/// 插屏广告关闭回调load
/// video广告准备好的回调，按钮展示
/// </summary>
public class ShowADButton : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private VideoAd videoAd;
    private BannerAd BannerAd;
    private static int INTERSTITIAL_PID = 166556;//155458//166556
    private static int VIDEO_PID = 166554;//155456//166554
    private static int Banner_PID = 166553;//155457//166553
    private GameObject back;
    private GameObject refresh;
    private bool canNext;
    private bool canAward;
    private string NextID;
    private string NowLevel;

    private Button skip;
    private Button Next;
    // Start is called before the first frame update
    void Awake()
    {
    
    }

    void Start()
    {
        //广告
        this.BannerAd = new BannerAd(Banner_PID, AdPosition.Bottom);
        this.videoAd = new VideoAd(VIDEO_PID);
        this.interstitialAd = new InterstitialAd(INTERSTITIAL_PID);
        interstitialAd.LoadAd();
        videoAd.LoadAd();
        BannerAd.LoadAd();
        InvokeRepeating("LoadBanner", 0.1f, 30f);//延迟1秒执行，每次执行间隔30秒

        //按钮的初始化，和添加方法
        back = GameObject.Find("Canvas").transform .Find("back").gameObject;
        refresh = GameObject.Find("Canvas").transform .Find("refresh").gameObject ;
        refresh.GetComponent< Button>().onClick.AddListener(delegate { ReInterstitial(); });
        Scene scence = SceneManager.GetActiveScene();
        NowLevel = scence.name;
        NextID = (int.Parse(NowLevel) + 1).ToString();
        skip = GameObject.Find("Skip").GetComponent<Button>();
        //利用脚本添加方法
        skip.onClick.AddListener(delegate { showVideo(); });
        skip.gameObject.SetActive(false);
        GameObject win = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        win.SetActive(true);
        Next=win.transform.Find("next").GetComponent<Button>();
        Next.onClick.AddListener(delegate { showInterstitial();});
        win.SetActive(false);

        //广告回调
        interstitialAd.InterstitialAdReceive = delegate ()
        {
            //⼴广告获取成功回调，不不要在这⾥里里展示⼴广告
            Debug.Log("差评广告，获取成功");
        };
        interstitialAd.InterstitialAdPresent = delegate ()
        {
            //⼴广告展示回调
            Debug.Log("差评广告，展示");

        };
        if (interstitialAd.InterstitialAdDismissed!=null)
        {
            Debug.Log("Next广告方法不为空");
            interstitialAd.InterstitialAdDismissed -= interstitialAd.InterstitialAdDismissed;
        }
        interstitialAd.InterstitialAdDismissed = delegate ()
        {
            //广告被⽤用户关闭回调
            Debug.Log("差评广告，关闭");
            if (canNext)
            {
                SceneManager.LoadScene(NextID);
                if (int.Parse(PlayerPrefs.GetString("Level", NowLevel)) >= int.Parse(NowLevel))
                {

                }
                else
                {
                    PlayerPrefs.SetString("Level", NowLevel);//关卡
                }
            }
            interstitialAd.LoadAd();
        };

        //激励
        videoAd.VideoAdPlayable = delegate () {
            //⼴广告已经准备好，不不要在这⾥里里播放
            GameObject.Find("Canvas").transform .Find("Skip").gameObject.SetActive(true);
            Debug.Log("视频准备好了");
        };
        videoAd.VideoAdStart = delegate ()
        {
            Debug.Log("视频广告开始");
            canAward = false;
            
        };
        if (videoAd.VideoAdClose != null)
        {
            Debug.Log("skip方法不为kong");
            videoAd.VideoAdClose -= this.videoAd.VideoAdClose;
        }
        videoAd.VideoAdClose = delegate ()
        {
            
            if (canAward)
            {
                //先设置当前关卡的星星数，在加载下一关
                if (int.Parse(PlayerPrefs.GetString("Level", NowLevel)) >= int.Parse(NextID))
                {

                }
                else
                {
                    PlayerPrefs.SetString("Level", NextID);//关卡
                }
                PlayerPrefs.SetString("Level" + NowLevel, NowLevel);
                PlayerPrefs.SetInt(PlayerPrefs.GetString("Level" + NowLevel), 3);
                Debug .Log(PlayerPrefs.GetString("Level" + NowLevel)+"现在的关卡");
                Debug.Log("skip加载下一关"+ NextID);
                SceneManager.LoadScene(NextID);

            }
            else
            {
                Debug.Log("视屏没有播放完关闭");
                back.SetActive(true);
                refresh.SetActive(true);
            }
        };
        videoAd.VideoCompleted = delegate ()
        {
            Debug.Log("视频播放完毕");
            canAward = true;
        };

    }

    // Update is called once per frame
    void Update()
    {
       
    }


    //banner
    public void LoadBanner()
    {
        BannerAd.LoadAd();
        BannerAd.Show();
    }
    //激励视频
    public void showVideo()
    {
        back.SetActive(false);
        refresh.SetActive(false);
        videoAd.PlayAdVideo();
    }
    //next插屏广告
    public void showInterstitial()
    {
        canNext = true;
        if (!interstitialAd .IsReadyToShow())
        {
            //interstitialAd.LoadAd();
           // interstitialAd.ShowAd();
            if (!interstitialAd.IsReadyToShow())
            {
                Debug .Log("加载下一关");
                SceneManager.LoadScene(NextID);
            }
        }
        else
        {
            interstitialAd.ShowAd();
        }
       
    }
    //刷新插屏广告
    public void ReInterstitial()
    {
        canNext = false;
        if (!interstitialAd.IsReadyToShow())
        {
            //interstitialAd.LoadAd();
            //interstitialAd.ShowAd();
            if (!interstitialAd.IsReadyToShow())
            {
                Debug.Log("加载当前关卡");
                SceneManager.LoadScene(NowLevel);
            }
        }
        else
        {
            interstitialAd.ShowAd();
        }

    }
    void OnApplicationFocus(bool isFocus)
    {


        if (isFocus)
        {

            Debug.Log("返回到游戏 刷新用户数据");  //  返回游戏的时候触发     执行顺序 2      

        }
        else
        {
            if (!interstitialAd.IsReadyToShow())
            {
                interstitialAd.LoadAd();
            }
            Debug.Log("离开游戏 激活推送");  //  返回游戏的时候触发     执行顺序 1
        }
    }
    //计算出玩家后台运行时间
    float  gameHTtime = 0;
    //玩家切后台的运行时间
    float  startHTtime = 0;
    //倒计时的显示停止期限 下线    保留致少是2 这样可以在倒计时玩家切回来倒计时的循环中--致少会在循环两边 这样使得循环方法中的更新相关数据！！！
    int DtimeNum = 2;

    public void OnApplicationPause(bool isPause)
    {

        if (isPause)
        {
            //将玩家游戏切后台的运行时间检测到
            startHTtime =  Time.realtimeSinceStartup;
            if (!interstitialAd.IsReadyToShow())
            {
                interstitialAd.LoadAd();
            }
            Debug.Log("游戏暂停 一切停止 "+ startHTtime+ "玩家切后台的运行时间"); // 缩到桌面的时候触发
        }
        else
        {
            //回到前台我们需要将后台的倒计时方法关闭掉
            //将游戏的运行总时间检测到   使用总的游戏时间-玩家的切入后台的时间  就是玩家在切后台的总时间
            gameHTtime = (Time.realtimeSinceStartup - startHTtime);
            if (gameHTtime>60)
            {
                canNext = false;

                if (interstitialAd.IsReadyToShow())
                {
                    interstitialAd.ShowAd();
                }
            }
            Daojishi_HT(gameHTtime);
            Debug.Log("游戏开始  万物生机 "+ gameHTtime+"后台时间"); //回到游戏的时候触发 最晚
        }
    }
    //倒计时1分钟
    public void Daojishi_HT(float  NUM)
    {
        float  a=1;
            a-= NUM;
    }
}
