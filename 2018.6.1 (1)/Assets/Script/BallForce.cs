using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour
{
    [Tooltip("给力增加一个倍数")]
    private  float power=0.005f;//
    public float maxForce=2000;//避免穿透，限制最大力量
    private List<Vector2> mouseP = new List<Vector2>();//将鼠标按下到抬起的所有位置添加到list
    private Rigidbody2D rigidbody2;
    private Vector2 start;
    private Vector2 end;
    private float distance;//距离
    private Vector2 direction;//方向
    private float AngleDirection;//角度
    //绘制辅助虚线
    public GameObject parent;
    public GameObject prefab;
    private bool canCreateHelper;
    private List<Vector3> helpBall = new List<Vector3>();
    //特效
    public GameObject TextEffect;
    public Transform Effectparent;
    private float maxLength = 5;
    private bool FirstClick = true;
    //
    private GameObject TopBall;
    private GameObject BallP;
    public bool canHelp=true;
    private GameObject failedRe;
    private void Awake()
    {
        canCreateHelper = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        BallP = GameObject.Find("BallP");
        TopBall = GameObject.Find("BallTop");
        rigidbody2 = this.GetComponent<Rigidbody2D>();
        failedRe = GameObject.Find("Canvas").transform.Find("failedRe").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canHelp = true;
        }
        
            if (Input.GetMouseButton(0)&&canHelp ) //如果鼠标一直按下 
            {
            MoveFunc();
            StartCoroutine(HelpLine());
            }

            if (Input.GetMouseButtonUp(0)&&canHelp )
            {
                MouseUp();
                StartCoroutine(Birth());
            }
        

    }
    

    IEnumerator Birth()
    {
        yield return new WaitForSeconds(1f);
        BallP.transform.GetChild(0).GetComponent<BallForce>().enabled = false;
        BallP.transform.GetChild(0).SetParent(GameObject.Find("AbandonBall").transform);
        TopBall.transform.GetChild(TopBall.transform.childCount - 1).SetParent(BallP.transform);
        BallP.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        BallP.transform.GetChild(0).localScale = new Vector3(0.85f, 0.85f, 0.85f);
        //功能
        if (TopBall.transform.childCount !=0)
        {
            BallP.transform.GetChild(0).GetComponent<BallForce>().enabled = true;
        }
        else
        {
            BallP.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(3f);
            Debug.Log(GameObject.Find("Cub").GetComponent<CupTrigger>().isEnter+"是True？");
            if (!GameObject.Find("Cub").GetComponent<CupTrigger>().isEnter)
            {
                Debug.Log("发球失败");
                failedRe.SetActive(true);
            }
            
        }
    }

    void MouseUp()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            this.transform .parent.GetComponent<AudioSource>().Play();
        }
        else
        {

        }
        rigidbody2.simulated = true;
        rigidbody2.AddForce(direction.normalized * distance * power, ForceMode2D.Impulse);
        rigidbody2.gravityScale = 1f;
        mouseP.Clear();

        Instantiate(TextEffect, Effectparent);
    }

    void MoveFunc()
    {

        mouseP.Add(Input.mousePosition);
        end = mouseP[mouseP.Count - 1];
        start = mouseP[0];
        AngleDirection = Mathf.Atan2((start.x - end.x), (start.y - end.y)) * (180 / Mathf.PI);
        distance = Vector2.Distance(start, end);
        if (distance > maxForce)
        {
            distance = maxForce;
        }
        direction = start - end;
    }
    //辅助线
    IEnumerator HelpLine()
    {
        if (!canCreateHelper)
            yield break;
        canCreateHelper = false;
        if (FirstClick == true)
        {
            
            for (int i = 24; i >= 0; i--)
            {
                GameObject t = Instantiate(prefab, this.transform.position, Quaternion.identity);
                t.GetComponent<HelpBall>().ExistTime = i * 0.05f;
                //0.05为间隔时间
                //0.05=生成间隔时间*(Time.deltaTime*0.1f)中的speed0.1f;
                t.transform.SetParent(parent.transform);
                t.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }

            FirstClick = false;
        }
        else
        {
            GameObject t = Instantiate(prefab, this.transform.position, Quaternion.identity);
            //t.GetComponent<HelpBall>().ExistTime = i * 0.05f;
            t.transform.SetParent(parent.transform);
            t.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        if (parent.transform.childCount>25)
        {
            Destroy(parent.transform.GetChild(0).gameObject, 0);
        }

        
        yield return new WaitForSeconds(0.5f);//生成间隔时间
        canCreateHelper = true;

    }
    public Vector3 GetForce()
    {
        return direction.normalized * distance * power;
    }
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
    public float GetMass()
    {
        return this.GetComponent<Rigidbody2D>().mass;
    }


}
