using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCupTrigger : MonoBehaviour
{
    public GameObject water;
    public Transform waterP;
    private GameObject TopBall;
    private GameObject failedRe;
    private GameObject BallP;
    private GameObject leftwater;
    private GameObject rightwater;
    private GameObject AbandonBall;
    private bool isfallover;
    // Start is called before the first frame update
    void Start()
    {
        AbandonBall = GameObject.Find("AbandonBall");
        TopBall = GameObject.Find("BallTop");
        failedRe = GameObject.Find("Canvas").transform.Find("failedRe").gameObject;
        BallP = GameObject.Find("BallP");
        leftwater = waterP.Find("leftwater").gameObject;
        rightwater = waterP.Find("rightwater Variant").gameObject;
        isfallover = true;
    }

    // Update is called once per frame
    void Update()
    {
        //右
        if (transform.rotation.eulerAngles.z > 40 && transform.rotation.eulerAngles.z < 180 && isfallover)
        {
            rightwater.SetActive(true);
            //failedRe.SetActive(true);
            isfallover = false;
        }
        //左
        if (transform.rotation.eulerAngles.z > 180 && transform.rotation.eulerAngles.z < 320 && isfallover)
        {
            leftwater.SetActive(true);
            //failedRe.SetActive(true);
            isfallover = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Ball")
        {
            this.GetComponent<AudioSource>().Play();
            //实例化水花
            Instantiate(water, waterP);

            StartCoroutine(FailFunc());
            
        }

    }
    IEnumerator FailFunc()
    {
        yield return new WaitForSeconds(1f);
        if (TopBall.transform.childCount >= 1)
        {
            Destroy(AbandonBall.transform.GetChild(0).gameObject);
            //Destroy(AbandonBall.transform.GetChild(AbandonBall.transform.childCount).gameObject);
            //// 父物体
            //TopBall.transform.GetChild(TopBall.transform.childCount - 1).SetParent(BallP.transform);

            //// 位置
            //BallP.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            //BallP.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            ////功能
            //BallP.transform.GetChild(0).GetComponent<BallForce>().enabled = true;
        }
        else
        {
            Debug.Log("失败");
            failedRe.SetActive(true);
            Destroy(AbandonBall.transform.GetChild(0).gameObject);
        }

    }
}
