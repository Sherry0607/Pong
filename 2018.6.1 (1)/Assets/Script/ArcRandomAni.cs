using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ArcRandomAni : MonoBehaviour
{
    /// <summary>
    /// 文字动画
    /// </summary>
    private Image image;
    private RectTransform rectTransform;
    void OnEnable()
    {
        image = this.GetComponent<Image>();
        image.color = new Color((float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255);
        rectTransform = this.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(Random.Range(-270, 270), Random.Range(-350, 550));
        Ani();
        Destroy(this.gameObject, 1.5f);
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
        Sequence se = DOTween.Sequence();
        Vector2 one=new Vector2(rectTransform .anchoredPosition .x-200,rectTransform .anchoredPosition.y+150);
        se.Append(rectTransform.DOAnchorPos(one, 1));
        se.Join(rectTransform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1));
        se.Join(image .DOFade(0,1f ));
        se.Play();
    }
}
