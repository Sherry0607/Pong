using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAP;
public class ColliderFunc : MonoBehaviour
{
    private GameObject BounceP;
    private GameObject PPstar;
    public GameObject Bounce;
    public GameObject Pstar;
    private AndroidJavaObject javaObject;
   // private VibratorShot vibrator;
    // Start is called before the first frame update
    void Start()
    {
        BounceP = GameObject.Find("BounceP");
        PPstar = GameObject.Find("Canvas");
        //vibrator=new VibratorShot();
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        javaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag  == "Ball")
        {
            if (PlayerPrefs.GetInt("music")==1)
            {
                this.GetComponent<AudioSource>().Play();
            }
            else
            {
                
            }
            if (PlayerPrefs.GetInt("shake",1) == 1)
            {
                Debug .Log("碰撞");
               // vibrator.vibrator(50.ToString());
                javaObject.Call("UnityCallShake", "50");
            }
            else
            {

            }


            if (BounceP .transform .childCount >0)
            {
                return;
            }
            else
            {
                Instantiate(Bounce, BounceP.transform);
            }

            GameObject star = Instantiate(Pstar, PPstar.transform);
            star.transform.position = new Vector3(other .transform .position .x, other.transform.position.y, other.transform.position.z-0.5f);

        }
    }
}
