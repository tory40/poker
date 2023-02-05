using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRule : MonoBehaviour
{
    public List<CommandObject> CommandList = new List<CommandObject>();
    public List<CommandObject> DefaultCommand = new List<CommandObject>();
    public CommandObject serectobject;
    public static MainRule rule;
    private void Awake()
    {
        if(rule==null)
        {
            rule = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 30; ++i)
        {
            CommandObject obj = new CommandObject();
            DefaultCommand.Add(obj);
        }
        CommandGet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CommandGet()
    {
         for (int i = 1; i <= 30; ++i)
         {
             Debug.Log("true");
             CommandObject obj = GameObject.Find("Toggle ("+i.ToString()+")").GetComponent<CommandObject>();
             CommandList.Add(obj);
         }
    }
    public void ChangeMine(int element)
    {
        Debug.Log(CommandList.Count);
        Debug.Log(CommandList[serectobject.objectnumber].elements.Count);
        Debug.Log(CommandList[serectobject.objectnumber].elements[element].mine);
        CommandList[serectobject.objectnumber].elements[element].mine = !CommandList[serectobject.objectnumber].elements[element].mine;
        CommandList[serectobject.objectnumber].Display(element);
    }
    public void ChangeLevel(int element, float levels)
    {
        CommandList[serectobject.objectnumber].elements[element].level = levels;
        CommandList[serectobject.objectnumber].Display(element);
    }
}
