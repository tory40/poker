using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRule : MonoBehaviour
{
    public List<CommandObject> CommandList = new List<CommandObject>();
    public List<CommandObject> DefaultList = new List<CommandObject>();
    public List<DefalutRule> DefaultCommand = new List<DefalutRule>();
    DefalutRule getdefalut;
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
    public float time = 10;
    public float addtime = 60;
    public int fight = 5;
    public List<PokerType> types = new List<PokerType>();
    public List<PokerType> defaulttypes = new List<PokerType>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aaaaa");
        //ƒfƒtƒHƒ‹ƒg‚Ìƒf[ƒ^‚ğ“Ç‚İ‚Ş
        for (int i = 0; i < 27; ++i)
        {
            PokerType typenum = new PokerType();
            typenum.type = typenum.texts[i];
            typenum.strong = i;
            types.Add(typenum);
            defaulttypes.Add(typenum);
        }
        for (int i = 1; i <= 30; ++i)
        {
            CommandObject obj = new CommandObject();
            for (int j = 0; j < 7; ++j)
            {
                obj.elements.Add(new CommandElement());
            }
            obj.elements[3].type = "Cost";
            if (i - 1 < DefaultCommand.Count)
            {
                getdefalut = DefaultCommand[i - 1];
                for (int j = 0; j < 7; ++j)
                {
                    obj.elements[j].elementnumber = j;
                    obj.elements[j].type = getdefalut.types[j];
                    obj.elements[j].mine = getdefalut.mines[j];
                    obj.elements[j].level = getdefalut.levels[j];
                }
                obj.allin = getdefalut.allin;
                obj.commandname = getdefalut.commandname;
                obj.speed = getdefalut.speed;
                obj.canturn = getdefalut.canturn;
                obj.beforeturn = getdefalut.beforeturn;
                obj.objectbool = getdefalut.objectbool;
            }
            CommandList.Add(obj);
            DefaultList.Add(obj);
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
                for (int j = 0; j < 7; ++j)
                {
                    obj.elements.Add(new CommandElement());
                }
                obj.elements[3].type = "Cost";
                if(i-1<DefaultCommand.Count)
                {
                    getdefalut = DefaultCommand[i-1];
                    for(int j=0; j<7; ++j)
                    {
                        obj.elements[j].elementnumber=j;
                        obj.elements[j].type = getdefalut.types[j];
                        obj.elements[j].mine = getdefalut.mines[j];
                        obj.elements[j].level = getdefalut.levels[j];
                    }
                    obj.allin = getdefalut.allin;
                    obj.commandname = getdefalut.commandname;
                    obj.speed = getdefalut.speed;
                    obj.canturn = getdefalut.canturn;
                    obj.beforeturn = getdefalut.beforeturn;
                    obj.objectbool = getdefalut.objectbool;
                    obj.gameObject.GetComponent<Toggle>().isOn = obj.objectbool;
                    obj.transform.Find("Button").transform.Find("Text").GetComponent<Text>().text= obj.commandname;
                }
                CommandList[i-1]=obj;
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
        }
    }
    public void BeforeTurnChange()
    {
        CommandList[serectobject.objectnumber].beforeturn = !CommandList[serectobject.objectnumber].beforeturn;
        if(CommandList[serectobject.objectnumber].beforeturn)
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "ˆÈ‘O‚É";
        }
        else
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "ˆÈ~‚É";
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
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "ƒI[ƒ‹ƒCƒ“‚É‚Í”­“®‚Å‚«‚È‚¢";
                break;
            case 1:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "ƒI[ƒ‹ƒCƒ“‚É‚µ‚©”­“®‚Å‚«‚È‚¢";
                break;
            case 2:
                GameObject.Find("Allin").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "§ŒÀ‚È‚µ";
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
    public void ChangeBool(int number)
    {
        try
        {
            CommandList[number].objectbool = !CommandList[number].objectbool;
        }
        catch
        {

        }
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
            endbattleinput.text = endbattle.ToString();
        }
    }
    [SerializeField] InputField timeinput;
    [SerializeField] InputField addtimeinput;
    [SerializeField] InputField fightinput;

    public void ChangeTime()
    {
        try
        {
            time = float.Parse(timeinput.text);
            timeinput.text = time.ToString();
        }
        catch
        {
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
            timeinput.text = time.ToString();
        }
    }
    public void ChangeAddTime()
    {
        try
        {
            addtime = float.Parse(addtimeinput.text);
            addtimeinput.text = addtime.ToString();
        }
        catch
        {
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
            addtimeinput.text = addtime.ToString();
        }
    }

    public void ChangeFight()
    {
        try
        {
            fight = (int)(float.Parse(fightinput.text));
            fightinput.text = fight.ToString();
        }
        catch
        {
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
            fightinput.text = fight.ToString();
        }
    }
    public void ChangeWinbool()
    {
        winbool = !winbool;
        if(winbool)
        {
            wintext.text = "Ÿ—˜ğŒ";
            losetext.text = "”s–kğŒ";
        }
        else
        {
            wintext.text = "”s–kğŒ";
            losetext.text = "Ÿ—˜ğŒ";
        }
    }
    public void ChangeStrong(int input)
    {
        try
        {
            types[input].strong = float.Parse(inputstrong[input].text);
        }
        catch
        {
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
            inputstrong[input].text = types[input].strong.ToString();
        }
    }
    public void ChangeStrongEnter(int input)
    {
        try
        {
            inputstrong[input].text = types[input].strong.ToString("0.00");
        }
        catch
        {
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
            inputstrong[input].text = types[input].strong.ToString();
        }
    }
}
