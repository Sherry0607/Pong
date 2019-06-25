using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RandomAni : MonoBehaviour
{
    //文字动画特效
    private Vector2 vector2=new Vector2( 0,300);
    private RectTransform rectTransform;
    void OnEnable()
    {
          this.GetComponent<Image>().color = new Color((float )Random.Range(0, 255)/255,(float ) Random.Range(0, 255)/255, (float )Random.Range(0, 255)/255);
        rectTransform = this.GetComponent<RectTransform>();
        rectTransform .anchoredPosition=new Vector2(Random .Range(-270,270 ),Random .Range( -350,550));
     
        Ani();
        Destroy(this.gameObject, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Ani()
    {
      
        rectTransform.DOPunchAnchorPos(vector2, 2, 3, 0.5f);
    }
}
