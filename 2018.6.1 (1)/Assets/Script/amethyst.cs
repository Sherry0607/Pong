using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amethyst : MonoBehaviour
{
    //水晶特效
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Ball")
        {
            if (PlayerPrefs.GetInt("music") == 1)
            {
                this.GetComponent<AudioSource>().Play();
            }
            else
            {

            }
            Destroy(this.gameObject,0.25f);
          
        }

    }
}
