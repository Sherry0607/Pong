using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLevel : MonoBehaviour
{
    private GameObject Toplevel;
    private GameObject Win;
    private GameObject Winlevel;
   
    public string NowLevel;
    public Sprite Sprite;
    // Start is called before the first frame update
    void Awake()
    {
        Toplevel = GameObject.Find("level");
        Toplevel.GetComponent<Text>().text = NowLevel;
        Win = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        Win.transform.Find("show").GetComponent<Image>().sprite = Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
