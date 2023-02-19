using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRule : MonoBehaviour
{
    public List<CommandObject> CommandList = new List<CommandObject>();
    public List<CommandObject> DefaultCommand = new List<CommandObject>();
    public CommandObject serectobject;
    public int startpoint;
    public int betpoint;
    public float winmine;
    public float losemine;
    public float winenemy;
    public float loseenemy;
    public bool winbool;
    public int winpoint;
    public int endbattle;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 30; ++i)
        {
            CommandObject obj = new CommandObject();
            DefaultCommand.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CommandGet()
    {
         for (int i = 1; i <= 30; ++i)
         {
             CommandObject obj = GameObject.Find("Toggle ("+i.ToString()+")").GetComponent<CommandObject>();
             CommandList.Add(obj);
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
            Debug.Log("半角数字を入力してください");
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
            Debug.Log("半角数字を入力してください");
        }
    }
    public void BeforeTurnChange()
    {
        CommandList[serectobject.objectnumber].beforeturn = !CommandList[serectobject.objectnumber].beforeturn;
        if(CommandList[serectobject.objectnumber].beforeturn)
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "以前に";
        }
        else
        {
            GameObject.Find("CanTurn").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "以降に";
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
}
