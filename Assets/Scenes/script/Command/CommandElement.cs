using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandElement : MonoBehaviour
{
    public int elementnumber;
    public string type = "None";
    public bool mine = true;
    public float level = 1;
    [SerializeField] public Text minetext;
    [SerializeField] public InputField leveltext;
    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickLevel()
    {
        try
        {
            inputField = transform.Find("InputField").GetComponent<InputField>();
            level = float.Parse(inputField.text);
            switch(type)
            {
                case "Change":
                    if(level < 0)
                    {
                        level = 0;
                    }
                    else if (level > 5)
                    {
                        level = 5;
                    }
                    break;
                case "Draw":
                    if (level < 0)
                    {
                        level = 0;
                    }
                    else if (level > 9)
                    {
                        level = 9;
                    }
                    break;
                case "Cost":
                    if (level < 0)
                    {
                        level = 0;
                    }
                    break;
            }
            GameObject.Find("RuleOption").GetComponent<MainRule>().ChangeLevel(elementnumber, level);
        }
        catch
        {
            Debug.Log("îºäpêîéöÇì¸óÕÇµÇƒÇ≠ÇæÇ≥Ç¢");
        }
    }
    public void ClickMine()
    {
        mine = !mine;
        GameObject.Find("RuleOption").GetComponent<MainRule>().ChangeMine(elementnumber);
        if (mine)
        {
            
        }
        else
        {

        }
    }
}
