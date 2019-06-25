using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCloseFunc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //实时监测的鼠标按下，抬起的影响控制。
    //也可用Canvas level试试
    private Transform Ballp;
    void Start()
    {
        Ballp = GameObject.Find("BallP").transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

       Ballp.GetChild(0).GetComponent<BallForce>().enabled  = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       Invoke("Delay", 0.1f);
    }

    void Delay()
    {
        Ballp.GetChild(0).GetComponent<BallForce>().enabled   = true;
    }
}
