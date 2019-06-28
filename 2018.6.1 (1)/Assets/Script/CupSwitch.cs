using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSwitch : MonoBehaviour
{
    private GameObject red;
    private GameObject blue;

    
    // Start is called before the first frame update
    void Start()
    {
        red = this.transform.GetChild(0).gameObject;
        blue = this.transform.GetChild(1).gameObject;
        blue.SetActive(false);
        InvokeRepeating("Switch", 1f, 3f);
    }

    public void Switch()
    {
      
        if (!blue.active)
        {
            red.SetActive(false);
            blue .SetActive(true );
            if (!transform .GetChild(0).GetComponent<CupTrigger>().isSwitch)
            {
                blue.GetComponent<BlueCupTrigger>().enabled = false;
            }
            
        }
        else
        {
            blue.SetActive(false);
            red.SetActive(true);
            if (!transform.GetChild(0).GetComponent<CupTrigger>().isSwitch)
            {
                blue.GetComponent<BlueCupTrigger>().enabled = false;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
