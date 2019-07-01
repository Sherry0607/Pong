using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amethyst : MonoBehaviour
{
    public int childNum;
    public AudioClip[] amethystlist;
    private AudioSource audio;
    private Transform parent;
    //水晶特效
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        parent = GameObject.Find("AmethystParent").transform;
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
                PlayAudio();
            }
            else
            {

            }
            Destroy(this.gameObject,0.1f);
          
        }

    }

    void PlayAudio()
    {
        if (childNum == 5)
        {
            switch (parent.childCount)
            {
                case 1:
                    //播放声音5
                    audio.PlayOneShot(amethystlist[4]);
                    break;
                case 2:
                    //播放声音4
                    audio.PlayOneShot(amethystlist[3]);
                    break;
                case 3:
                    audio.PlayOneShot(amethystlist[2]);
                    //播放声音4
                    break;
                case 4:
                    audio.PlayOneShot(amethystlist[1]);
                    //播放声音4
                    break;
                case 5:
                    audio.PlayOneShot(amethystlist[0]);
                    //播放声音4
                    break;
            }
        }
        else if (childNum == 4)
        {
            switch (parent.childCount)
            {
                case 1:
                    //播放声音5
                    audio.PlayOneShot(amethystlist[3]);
                    break;
                case 2:
                    //播放声音4
                    audio.PlayOneShot(amethystlist[2]);
                    break;
                case 3:
                    audio.PlayOneShot(amethystlist[1]);
                    //播放声音4
                    break;
                case 4:
                    audio.PlayOneShot(amethystlist[0]);
                    //播放声音4
                    break;
            }
        }
    }
}

