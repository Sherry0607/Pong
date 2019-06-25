using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class chooseLevelSet : MonoBehaviour
{
    public Sprite Sprite;
    public Sprite Open;
    public Sprite huiStar;
    public Sprite GoadStar;

    private List <GameObject> Aelement=new List<GameObject>();
    private List<int> starCount=new List<int>();
    private int levelNum;
    
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < this .transform .childCount ; i++)
        {
            Aelement.Add(this .transform .GetChild(i).gameObject );
        }

        SetText();
        NowFunc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScence(string num)
    {
        SceneManager.LoadScene(num);
    }

    void NowFunc()
    {
        string arriveLevel = PlayerPrefs.GetString("Level","1");
        int arriveNum = int.Parse(arriveLevel);
        for (int i = 0; i < Aelement.Count; i++)
        {
            for (int j = 0; j < arriveNum; j++)
            {
           
                Aelement[j].GetComponent<Button>().interactable = true;
               
                Aelement[j].GetComponent<Image>().sprite = Open;
           
                starCount.Add(PlayerPrefs.GetInt(PlayerPrefs.GetString("Level" +(j+1).ToString())));
                print(starCount[j] + "   " + j);
                    switch (starCount [j])
                    {
                        case 0:
                            Aelement[j].transform.GetChild(1).GetComponent<Image>().sprite = huiStar;
                            Aelement[j].transform.GetChild(2).GetComponent<Image>().sprite = huiStar;
                            Aelement[j].transform.GetChild(3).GetComponent<Image>().sprite = huiStar;
                            break;
                        case 1:
                            Aelement[j].transform.GetChild(1).GetComponent<Image>().sprite = GoadStar;
                            Aelement[j].transform.GetChild(2).GetComponent<Image>().sprite = huiStar;
                            Aelement[j].transform.GetChild(3).GetComponent<Image>().sprite = huiStar;
                            break;
                        case 2:
                            Aelement[j].transform.GetChild(1).GetComponent<Image>().sprite = GoadStar;
                            Aelement[j].transform.GetChild(2).GetComponent<Image>().sprite = GoadStar;
                            Aelement[j].transform.GetChild(3).GetComponent<Image>().sprite = huiStar;
                            break;
                        case 3:
                            Aelement[j].transform.GetChild(1).GetComponent<Image>().sprite = GoadStar;
                            Aelement[j].transform.GetChild(2).GetComponent<Image>().sprite = GoadStar;
                            Aelement[j].transform.GetChild(3).GetComponent<Image>().sprite = GoadStar;
                            break;
                    }
                
            }

            for (int j = arriveNum; j < Aelement.Count; j++)
            {
                Aelement[j].GetComponent<Button>().interactable = false;//锁关
                Aelement[j].GetComponent<Image>().sprite = Sprite;//灰色
            }
        }
    }


    void SetText()
    {
        for (int i = 0; i < Aelement.Count; i++)
        {
            Aelement[i].transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();
        }
    }

   
}
