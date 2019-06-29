using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;

public class GameUIButton : MonoBehaviour
{
    private GameObject TopBall;
    private  string level;
    private Button WinRefresh;
    private Button Refailed;

    public Dictionary< string,int>Dictionary=new Dictionary<string, int>();
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        TopBall = GameObject.Find("BallTop");
        Scene scene = SceneManager.GetActiveScene();
        level = scene.name;

        GameObject parent = GameObject.Find("Canvas");
        WinRefresh = parent.transform.Find("Win").transform.Find("refresh").GetComponent<Button>();
        Refailed = parent.transform.Find("failedRe").GetComponent<Button>();
        WinRefresh.onClick.AddListener(() => { refresh(); });
        Refailed.onClick.AddListener(() => { refresh(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseBallFunc()
    {
        GameObject.FindWithTag("Ball").GetComponent<BallForce>().enabled = false;
    }
    public void OpenBallFunc()
    {
        GameObject.FindWithTag("Ball").GetComponent<BallForce>().enabled = true;
    }
    public void back()
    {
        SceneManager.LoadScene("start");
    }

    public void refresh()
    {
        SceneManager.LoadScene(level );
    }
}
