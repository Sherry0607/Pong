using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnbalePlay : MonoBehaviour
{
//获胜界面的声音控制
    void OnEnable()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            this.GetComponent<AudioSource>().Play();
        }
        else
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once  per frame
    void Update()
    {
        
    }

}
