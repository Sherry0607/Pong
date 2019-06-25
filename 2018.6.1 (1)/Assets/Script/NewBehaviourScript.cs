using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private AndroidJavaObject javaObject;

    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaClass androidJavaClass=new AndroidJavaClass("com.unity3d.player.UnityPlayer");
     
        javaObject=androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
  
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ceshi1()
    {
        Debug.Log("按钮被按下 ");
        javaObject.Call("UnityCallShake", "50");
    }
    public void ceshi2()
    {
        Debug.Log("按钮被按下 ");
        javaObject.Call("UnityCallShake", "30");
    }
    public void ceshi3()
    {
        Debug.Log("按钮被按下 ");
        javaObject.Call("UnityCallShake", "20");
        
    }
}
