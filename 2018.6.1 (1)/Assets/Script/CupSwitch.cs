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
            transform.GetChild(0).transform.GetChild(2).GetComponent<CupTrigger>().enabled = false;

            blue.SetActive(true);
            Invoke("DelayBlueTrue",0.5f);
            if (!transform.GetChild(0).transform.GetChild(2).GetComponent<CupTrigger>().isSwitch)
            {
                blue.GetComponent<BlueCupTrigger>().enabled = false;
            }

        }
        else
        {
            blue.SetActive(false);
            transform.GetChild(1).transform.GetChild(2).GetComponent<BlueCupTrigger>().enabled = false;
          
            red.SetActive(true);
            Invoke("DelayRedTrue",0.5f);
            if (!transform.GetChild(0).transform.GetChild(2).GetComponent<CupTrigger>().isSwitch)
            {
                blue.GetComponent<BlueCupTrigger>().enabled = false;
            }

        }

    }

    void DelayRedTrue()
    {
 
        red.GetComponent<CupTrigger>().enabled = true;
       
    }

    void DelayBlueTrue()
    {
       
        blue.GetComponent<BlueCupTrigger>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
