using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRule : MonoBehaviour
{
    public List<CommandObject> CommandList = new List<CommandObject>();
    public List<CommandObject> DefaultCommand = new List<CommandObject>();
    public CommandObject serectobject;
    public int startpoint=10000;
    public int betpoint=100;
    public float winmine=1;
    public float losemine=0;
    public float winenemy=1;
    public float loseenemy=0;
    public bool winbool=true;
    public int winscore=999999;
    public int endbattle=99;
    public List<PokerType> types = new List<PokerType>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 30; ++i)
        {
            CommandObject obj = new CommandObject();
            DefaultCommand.Add(obj);
        }
        for (int i=0; i<27;++i)
        {
            PokerType typenum = new PokerType();
            typenum.type = typenum.texts[i];
            typenum.strong = i;
            types.Add(typenum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool startget =true;
    public void CommandGet()
    {
        if (startget)
        {
            startget = false;
            for (int i = 1; i <= 30; ++i)
            {
                CommandObject obj = GameObject.Find("Toggle (" + i.ToString() + ")").GetComponent<CommandObject>();
                CommandList.Add(obj);
            }
        }
    }
    public void ChangeMine(int element)
    {
        CommandList[serectobject.objectnumber].elements[element].mine = !CommandList[serectobject.objectnumber].elements[element].mine;
        CommandList[serectobject.objectnumber].Display(element);
    }
    public void ChangeLevel(int element, float levels)
    {
        CommandList[serectobject.objectnumber].elements[element].level = levels;
        CommandList[serectobject.objectnumber].Display(element);
    }
    public void NameChange()
    {
        CommandList[serectobject.objectnumber].commandname = GameObject.Find("Name").GetComponent<InputField>().text;
        CommandList[serectobject.objectnumber].gameObject.transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = GameObject.Find("Name").GetComponent<InputField>().text;
    }
    public void SpeedChange()
    {
        try
        {
            CommandList[serectobject.objectnumber].speed = (int)(float.Parse(GameObject.Find("Speed").GetComponent<InputField>().text));
            GameObject.Find("Speed").GetComponent<InputField>().text = CommandList[serectobject.objectnumber].speed.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
        }
    }
    public void CanTurnChange()
    {
        try
        {
            CommandList[serectobject.objectnumber].canturn = (int)(float.Parse(GameObject.Find("CanTurn").transform.Find("InputField").GetComponent<InputField>().text));
            GameObject.Find("CanTurn").transform.Find("InputField").GetComponent<InputField>().text = CommandList[serectobject.objectnumber].canturn.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
        }
    }
    public void BeforeTurnChange()
    {
        CommandList[serectobject.objectnumber].beforeturn = !CommandList[serectobject.objectnumber].beforeturn;
        if(CommandList[serectobject.objectnumber].beforeturn)
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "�ȑO��";
        }
        else
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "�ȍ~��";
        }
    }
    public void AllinChange()
    {
        CommandList[serectobject.objectnumber].allin++;
        if(CommandList[serectobject.objectnumber].allin>2)
        {
            CommandList[serectobject.objectnumber].allin = 0;
        }
        switch (CommandList[serectobject.objectnumber].allin)
        {
            case 0:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "�I�[���C�����ɂ͔����ł��Ȃ�";
                break;
            case 1:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "�I�[���C�����ɂ��������ł��Ȃ�";
                break;
            case 2:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "�����Ȃ�";
                break;
            default:
                return;

        }
    }
    public void MainChange(int elementnum)
    {
        switch (CommandList[serectobject.objectnumber].elements[elementnum].type)
        {
            case "Change":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "FreeChange";
                break;
            case "Cost":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Draw";
                break;
            case "Draw":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Change";
                break;
            case "Fight":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Fold";
                break;
            case "Fold":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Cost";
                break;
            case "FreeChange":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Fight";
                break;
            default:
                return;
        }
        CommandList[serectobject.objectnumber].Display(elementnum);
    }
    public void SubChange(int elementnum)
    {
        switch (CommandList[serectobject.objectnumber].elements[elementnum].type)
        {
            case "Change":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "FreeChange";
                break;
            case "Cost":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Draw";
                break;
            case "Draw":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Change";
                break;
            case "FreeChange":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Open";
                break;
            case "None":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "Cost";
                break;
            case "Open":
                CommandList[serectobject.objectnumber].elements[elementnum].type = "None";
                break;
            default:
                return;
        }
        CommandList[serectobject.objectnumber].Display(elementnum);
    }
    [SerializeField] InputField startinput;
    [SerializeField] InputField betinput;
    [SerializeField] InputField winmineinput;
    [SerializeField] InputField winenemyinput;
    [SerializeField] InputField losemineinput;
    [SerializeField] InputField loseenemyinput;
    [SerializeField] InputField winscoreinput;
    [SerializeField] InputField endbattleinput;
    [SerializeField] Text wintext;
    [SerializeField] Text losetext;
    [SerializeField] InputField[] inputstrong;
    public void ChangeStart()
    {
        try
        {
            startpoint = (int)(float.Parse(startinput.text));
            startinput.text = startpoint.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            startinput.text = startpoint.ToString();
        }
    }
    public void ChangeBet()
    {
        try
        {
            betpoint = (int)(float.Parse(betinput.text));
            betinput.text = betpoint.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            betinput.text = betpoint.ToString();
        }
    }
    public void ChangeWinmine()
    {
        try
        {
            winmine = (float.Parse(winmineinput.text));
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            winmineinput.text = winmine.ToString();
        }
    }
    public void ChangeWinenemy()
    {
        try
        {
            winenemy = (float.Parse(winenemyinput.text));
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            winenemyinput.text = winenemy.ToString();
        }
    }
    public void ChangeLosemine()
    {
        try
        {
            losemine = (float.Parse(losemineinput.text));
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            losemineinput.text = losemine.ToString();
        }
    }
    public void ChangeLoseenemy()
    {
        try
        {
            loseenemy = (float.Parse(loseenemyinput.text));
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            loseenemyinput.text = loseenemy.ToString();
        }
    }
    public void ChangeWinscore()
    {
        try
        {
            winscore = (int)(float.Parse(winscoreinput.text));
            winscoreinput.text = winscore.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            winscoreinput.text = winscore.ToString();
        }
    }
    public void ChangeEndBattle()
    {
        try
        {
            endbattle = (int)(float.Parse(endbattleinput.text));
            endbattleinput.text = endbattle.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            endbattleinput.text = endbattle.ToString();
        }
    }
    public void ChangeWinbool()
    {
        winbool = !winbool;
        if(winbool)
        {
            wintext.text = "��������";
            losetext.text = "�s�k����";
        }
        else
        {
            wintext.text = "�s�k����";
            losetext.text = "��������";
        }
    }
    public void ChangeStrong(int input)
    {
        try
        {
            types[input].strong = float.Parse(inputstrong[input].text);
            inputstrong[input].text = types[input].strong.ToString();
        }
        catch
        {
            Debug.Log("���p��������͂��Ă�������");
            inputstrong[input].text = types[input].strong.ToString();
        }
    }
}