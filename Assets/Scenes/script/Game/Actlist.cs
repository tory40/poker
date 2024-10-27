using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actlist : MonoBehaviour
{
    [SerializeField] public Image imagecolor;
    public string type;
    public float level;
    public bool mine;
    public bool actmine;
    public bool actfinish;
    public Text typetext;
    public Text minetext;
    public Text leveltext;

    // Start is called before the first frame update
    void Start()
    {
        typetext = GameObject.Find("Init2").transform.Find("Panel").transform.Find("Type").transform.Find("Text").GetComponent<Text>();
        minetext = GameObject.Find("Init2").transform.Find("Panel").transform.Find("Mine").transform.Find("Text").GetComponent<Text>();
        leveltext = GameObject.Find("Init2").transform.Find("Panel").transform.Find("Level").transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColorRed()
    {
        imagecolor.color = Color.red;
    }
    public void ColorBlue()
    {
        imagecolor.color = Color.blue;
    }
    public void ColorGray()
    {
        imagecolor.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void ColorWhaitRed()
    {
        imagecolor.color = new Color(1f, 0.5f, 0.5f, 1f);
    }
    public void ColorWhaitBlue()
    {
        imagecolor.color = new Color(0.5f, 0.5f, 1f, 1f);
    }

    public void Click()
    {
        typetext.text = type;
        if(mine)
        {
            minetext.text = "����";
        }
        else
        {
            minetext.text = "����";
        }
        leveltext.text = level.ToString();
    }
}
