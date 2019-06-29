using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainChoose : MonoBehaviour
{
    private List<GameObject> Aelement = new List<GameObject>();
    private List<int> starCount = new List<int>();
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Aelement.Add(this.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            SetText(i);
            starCount.Clear();
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextChoooseScence(string name)
    {
        SceneManager.LoadScene(name);
    }

    void SetText(int index)
    {
        for (int i = 1; i <= 30; i++)
        {
            starCount .Add(PlayerPrefs .GetInt( (PlayerPrefs .GetString("Level"+(i+30*index).ToString()))));
            Aelement[index].transform.GetChild(0).GetComponent<Text>().text = starCount.Sum().ToString() + "/90";
          

        }
    }
}
