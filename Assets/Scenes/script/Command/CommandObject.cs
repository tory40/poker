using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandObject : MonoBehaviour
{
    public List<CommandElement> elements =new List <CommandElement>();
    public List<Transform> transforms = new List<Transform>();
    public int allin=0;
    public string commandname = "未登録";
    public int speed = 0;
    public int objectnumber=0;
    [SerializeField] CommandElement[] objects;
    public int canturn =1;
    public bool beforeturn = false;
    public bool objectbool = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 7; ++i)
        {
            CommandElement command = new CommandElement();
            elements.Add(command);
        }
        for (int i = 1; i <= 7; ++i)
        {
            GameObject tmp = GameObject.Find("Position(" + i.ToString() + ")");
            if (tmp)
            {
                Transform trans = tmp.GetComponent<Transform>();
                transforms.Add(trans);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        GameObject.Find("RuleOption").GetComponent<MainRule>().serectobject = this;
        for(int i = 0; i < 7; ++i)
        {
            Debug.Log(elements[i].type);
            Display(i);
        }
        GameObject.Find("Name").GetComponent<InputField>().text= GameObject.Find("RuleOption").GetComponent<MainRule>().CommandList[objectnumber].commandname;
        GameObject.Find("Speed").GetComponent<InputField>().text = GameObject.Find("RuleOption").GetComponent<MainRule>().CommandList[objectnumber].speed.ToString();
        GameObject.Find("CanTurn").transform.Find("InputField").GetComponent<InputField>().text = GameObject.Find("RuleOption").GetComponent<MainRule>().CommandList[objectnumber].canturn.ToString();
        if(beforeturn)
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "以前に";
        }
        else
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "以降に";
        }
        switch(allin)
        {
            case 0:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "オールイン時には発動できない";
                break;
            case 1:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "オールイン時にしか発動できない";
                break;
            case 2:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "制限なし";
                break;
            default:
                return;

        }
    }
    public void Display(int i)
    {
        if (i<7)
        {
            int j;
            foreach (Transform child in transforms[i])
            {
                Destroy(child.gameObject);
            }
            switch(elements[i].type)
            {
                case "Change":
                    j = 0;
                    break;
                case "Cost":
                    j = 1;
                    break;
                case "Draw":
                    j = 2;
                    break;
                case "Fight":
                    j = 3;
                    break;
                case "Fold":
                    j = 4;
                    break;
                case "FreeChange":
                    j = 5;
                    break;
                case "None":
                    j = 6;
                    break;
                case "Open":
                    j = 7;
                    break;
                default:
                    return;
            }
            CommandElement commandobject = Instantiate(objects[j], transforms[i], false);
            commandobject.elementnumber = i;
            switch(elements[i].type)
            {
                case "Fight":
                case "Fold":
                case "None":
                    break;

                case "FreeChange":
                case "Open":
                    if(elements[i].mine)
                    {
                        commandobject.minetext.text = "自分の";
                    }
                    else
                    {
                        commandobject.minetext.text = "相手の";
                    }
                    break;

                case "Change":
                case "Draw":
                    if (elements[i].mine)
                    {
                        commandobject.minetext.text = "自分の";
                    }
                    else
                    {
                        commandobject.minetext.text = "相手の";
                    }
                    commandobject.leveltext.text = elements[i].level.ToString("0.");
                    break;

                case "Cost":
                    if (elements[i].mine)
                    {
                        commandobject.minetext.text = "自分の";
                    }
                    else
                    {
                        commandobject.minetext.text = "相手の";
                    }
                    commandobject.leveltext.text = elements[i].level.ToString("0.00");
                    break;

                default:
                    return;
            }
        }
    }
    public void Changebool()
    {
        Debug.Log(objectnumber);
        GameObject.Find("RuleOption").GetComponent<MainRule>().ChangeBool(objectnumber);
    }
}
