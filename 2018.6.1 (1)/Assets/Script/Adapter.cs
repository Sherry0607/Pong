using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : MonoBehaviour
{
    //UI适配
    private Canvas canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvas.scaleFactor = 0.9f;
        canvas.scaleFactor = 1f;
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
