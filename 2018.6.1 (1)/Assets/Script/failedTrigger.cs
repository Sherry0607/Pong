using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class failedTrigger : MonoBehaviour
{
    private GameObject TopBall;

    private GameObject failedRe;

    private GameObject BallP;

    private bool intNew=true;
    // Start is called before the first frame update
    void Start()
    {
        TopBall = GameObject.Find("BallTop");
        failedRe = GameObject.Find("Canvas").transform .Find("failedRe").gameObject ;
        BallP = GameObject.Find("BallP");
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            //FailFunc();
            StartCoroutine(FailFunc());
        }

    }

    IEnumerator  FailFunc()
    {
        if (TopBall .transform .childCount==1)
        {
            TopBall.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            failedRe.SetActive(true);
        }
    }
}
