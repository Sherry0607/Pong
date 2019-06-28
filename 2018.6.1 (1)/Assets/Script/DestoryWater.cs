using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWater : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(this.gameObject,1.2f);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
