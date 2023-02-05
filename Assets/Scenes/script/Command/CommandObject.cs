using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandObject : MonoBehaviour
{
    public List<CommandElement> elements =new List <CommandElement>();
    public List<Transform> transforms = new List<Transform>();
    public int allin=0;
    public string commandname = "–¢“o˜^";
    public int speed = 0;
    public int objectnumber=0;
    [SerializeField] CommandElement[] objects;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; ++i)
        {
            CommandElement command = new CommandElement();
            elements.Add(command);
        }
        elements[3].type = "Cost";
        elements[8].type = "Turn";
        for (int i = 1; i <= 9; ++i)
        {
            Transform trans = GameObject.Find("Position(" + i.ToString() + ")").GetComponent<Transform>();
            transforms.Add(trans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        GameObject.Find("RuleOption").GetComponent<MainRule>().serectobject = this;
        for(int i = 0; i < 9; ++i)
        {
            Display(i);
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
                        commandobject.minetext.text = "Ž©•ª‚Ì";
                    }
                    else
                    {
                        commandobject.minetext.text = "‘ŠŽè‚Ì";
                    }
                    break;

                case "Change":
                case "Draw":
                    if (elements[i].mine)
                    {
                        commandobject.minetext.text = "Ž©•ª‚Ì";
                    }
                    else
                    {
                        commandobject.minetext.text = "‘ŠŽè‚Ì";
                    }
                    commandobject.leveltext.text = elements[i].level.ToString("0.");
                    break;

                case "Cost":
                    if (elements[i].mine)
                    {
                        commandobject.minetext.text = "Ž©•ª‚Ì";
                    }
                    else
                    {
                        commandobject.minetext.text = "‘ŠŽè‚Ì";
                    }
                    commandobject.leveltext.text = elements[i].level.ToString("0.00");
                    break;

                default:
                    return;
            }
        }
    }
}
