using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CupTrigger : MonoBehaviour
{
    private GameObject WinUI;
    private GameObject Ball;

    private GameObject TopBall;
    private GameObject starShow;

    public GameObject water;
    private GameObject leftwater;
    private GameObject rightwater;
    public Transform  waterP;
    private GameObject BallP;
    private GameObject back;
    private GameObject refresh;
    private GameObject failedRe;
    private GameObject bg;
    public bool isSwitch=true;
    public int AddBlue;
    private string level;
    private string Nextlevel;
    private bool isfallover;

    public bool isEnter;
    // Start is called before the first frame update
    void Start()
    {
        WinUI = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        TopBall = GameObject.Find("BallTop");
        BallP = GameObject.Find("BallP");
        Ball = BallP .transform .GetChild( 0).gameObject;
        back = GameObject.Find("back");
        refresh = GameObject.Find("refresh");
        failedRe = GameObject.Find("Canvas").transform.Find("failedRe").gameObject;
        leftwater = waterP.Find("leftwater").gameObject;
        rightwater = waterP.Find("rightwater Variant").gameObject;
        Scene scene = SceneManager.GetActiveScene();
        int nextnum = int.Parse(scene.name)+1;
        bg = GameObject.Find("AbandonBall");
        Nextlevel = (nextnum).ToString();
        level = scene.name;
        isfallover = true;

    }

    // Update is called once per frame
    void Update()
    {
        //右
        if (transform.rotation.eulerAngles.z > 40&& transform.rotation.eulerAngles.z<180&&isfallover )
        {
             rightwater.SetActive(true);
            failedRe.SetActive(true);
            isfallover = false;
            BallP.transform.GetChild(0).GetComponent<BallForce>().enabled = false;
        }
        //左
        if (transform.rotation.eulerAngles.z >180 && transform.rotation.eulerAngles.z < 320&&isfallover)
        {
            leftwater.SetActive(true);
            failedRe.SetActive(true);
            isfallover = false;
            BallP.transform.GetChild(0).GetComponent<BallForce>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other .tag =="Ball"&& isfallover)
        {
            if (PlayerPrefs.GetInt("music") == 1)
            {
                this.GetComponent<AudioSource>().Play();
            }
            else
            {

            }
            isfallover = false;
            isSwitch = false;
            //实例化水花
            isEnter = true;
            Instantiate(water, waterP);
            StartCoroutine(Win());
            other.gameObject.GetComponent<BallForce>().enabled=false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
     
    }
    //游戏胜利
    IEnumerator  Win()
    {
        back .SetActive(false);
        refresh.SetActive(false);
        BallP.transform.GetChild(0).GetComponent<Rigidbody2D>().sharedMaterial = null;
        BallP.transform.GetChild(0).GetComponent<CircleCollider2D>().sharedMaterial = null;
        yield return new WaitForSeconds(1f);
        Destroy(bg.transform.GetChild(bg.transform.childCount - 1).gameObject);
        BallP.transform.GetChild(0).GetComponent<BallForce>().enabled = false;
        WinUI.SetActive(true);
        starShow = GameObject.Find("StarBg");
        yield return new WaitForEndOfFrame();
        //记录
        Next();
        switch (TopBall.transform.childCount)//+AddBlue)
        {
            case 3:
                starShow.transform.GetChild(0).gameObject.SetActive(true);
                starShow.transform.GetChild(0).GetComponent<Animator>().SetBool("Turn", true);
                yield return new WaitForSeconds(0.3f);
                starShow.transform.GetChild(1).gameObject.SetActive(true);
                starShow.transform.GetChild(1).GetComponent<Animator>().SetBool("Turn", true);
                yield return new WaitForSeconds(0.3f);
                starShow.transform.GetChild(2).gameObject.SetActive(true);
                starShow.transform.GetChild(2).GetComponent<Animator>().SetBool("Turn", true);
                break ;
            case 2:
                starShow.transform.GetChild(0).gameObject.SetActive(true);
                starShow.transform.GetChild(0).GetComponent<Animator>().SetBool("Turn", true);
                yield return new WaitForSeconds(0.3f);
                starShow.transform.GetChild(1).gameObject.SetActive(true);
                starShow.transform.GetChild(1).GetComponent<Animator>().SetBool("Turn", true);
                yield return new WaitForSeconds(0.3f);
                starShow.transform.GetChild(2).gameObject.SetActive(true);
                starShow.transform.GetChild(2).GetComponent<Animator>().SetBool("Turn", true);
                break;
            case 1:
                starShow.transform.GetChild(0).gameObject.SetActive(true);
                starShow.transform.GetChild(0).GetComponent<Animator>().SetBool("Turn", true);
                yield return new WaitForSeconds(0.3f);
                starShow.transform.GetChild(1).gameObject.SetActive(true);
                starShow.transform.GetChild(1).GetComponent<Animator>().SetBool("Turn", true);
                break;
            case 0:
                starShow.transform.GetChild(0).gameObject.SetActive(true);
                starShow.transform.GetChild(0).GetComponent< Animator >().SetBool("Turn",true);
                break;
        }
        
    }
     void  Next()
    {
        if (int.Parse(PlayerPrefs.GetString("Level", level)) > int.Parse(Nextlevel))
         {

         }
        else
        {
                PlayerPrefs.SetString("Level", Nextlevel); //关卡
        }
       
        PlayerPrefs.SetString("Level" + level, level);//关卡所对应星星数（当前关卡，关卡数）

        if (TopBall.transform.childCount >= PlayerPrefs.GetInt(PlayerPrefs.GetString("Level" + level)))
        {
            switch (TopBall.transform.childCount)
            {
                case 0:
                    PlayerPrefs.SetInt(PlayerPrefs.GetString("Level" + level), 1);
                    break;
                case 1:
                    PlayerPrefs.SetInt(PlayerPrefs.GetString("Level" + level), 2);
                    break;
                case 2:
                    PlayerPrefs.SetInt(PlayerPrefs.GetString("Level" + level), 3);
                    break;
                case 3:
                    PlayerPrefs.SetInt(PlayerPrefs.GetString("Level" + level), 3);
                    break;
            }
        }
        else
        {


        }
    
  
    }

}
