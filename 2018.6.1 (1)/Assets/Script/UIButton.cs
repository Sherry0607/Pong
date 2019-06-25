using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    private GameObject set;
    private GameObject Tset;
    private GameObject setback;
    public Sprite Openshake;
    public Sprite Closeshake;
    public Sprite Openmusic;
    public Sprite Closemusic;
    public Sprite OpenRec;
    public Sprite CloseRec;

    void Awake()
    {
        if (PlayerPrefs.GetInt("music",1) == 1)
        {
            this.GetComponent<AudioSource>().volume=1;
            PlayerPrefs .SetInt("music",1);
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 0;
            PlayerPrefs.SetInt("music", 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        set = GameObject.Find("bg").transform.Find("Set").gameObject;
        Tset = GameObject.Find("bg").transform.Find("Tset").gameObject;
        setback = Tset.transform.Find("setback").gameObject;
        
    }

    public void one()
    {
        Handheld.Vibrate();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        string level=PlayerPrefs.GetString("Level","1");
        SceneManager.LoadScene(level);
    }

    public void ChooseButton()
    {
        SceneManager.LoadScene("Choose");
    }
    public void SetButton()
    {
        set.transform .GetChild( 0).GetComponent<Animator>().SetBool("Turn",true );
        Invoke("TsetOpen", 0.3f);
    }

    void TsetOpen()
    {
        set.SetActive(false);
        Tset.SetActive(true);
        set.transform.GetChild(0).GetComponent<Animator>().SetBool("Turn", false );
    }

    public void SetBackButton()
    {
        setback.GetComponent<Animator>().SetBool("Turn", true);
        Invoke("SetOpen", 0.3f);
    }
    void SetOpen()
    {
        Tset.SetActive(false);
        set.SetActive(true);
        setback.GetComponent<Animator>().SetBool("Turn", false);
    }
    //是否开启震动反馈
    public void Rec()
    {
        Image  Rec = GameObject.Find("REC").GetComponent<Image>();
        int b = PlayerPrefs.GetInt("Rec",1);
        if (b==1)
        {
            Rec.sprite = CloseRec;
            PlayerPrefs.SetInt("Rec", 0);
        }
        else
        {
            Rec.sprite = OpenRec;
            PlayerPrefs.SetInt("Rec", 1);
        }
    }

    public void music()
    {
        Image Music = GameObject.Find("music").GetComponent<Image>();
        int b = PlayerPrefs.GetInt("music", 1);
        if (b == 1)
        {   
            Music.sprite=Closemusic;
            this.GetComponent<AudioSource>().volume = 0;
            PlayerPrefs.SetInt("music", 0);
        }
        else
        {
            Music.sprite = Openmusic;
            this.GetComponent<AudioSource>().volume=1;
            PlayerPrefs.SetInt("music", 1);
        }        
    }

    public void shake()
    {
        Image Shake= GameObject.Find("shake").GetComponent<Image>();
        int b = PlayerPrefs.GetInt("shake", 1);
        if (b == 1)
        {
            Shake.sprite = Closeshake;
            PlayerPrefs.SetInt("shake", 0);
        }
        else
        {
            Shake.sprite = Openshake;
            PlayerPrefs.SetInt("shake", 1);
        }
    }

    public void shore()
    {
        int b = PlayerPrefs.GetInt("shore", 1);
        if (b == 1)
        {
            PlayerPrefs.SetInt("shore", 0);
        }
        else
        {
            PlayerPrefs.SetInt("shore", 1);
        }
    }
}
