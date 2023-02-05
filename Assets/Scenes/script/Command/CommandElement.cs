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
            GameObject.Find("RuleOption").GetComponent<MainRule>().ChangeLevel(elementnumber, level);
        }
        catch
        {
            Debug.Log("”¼Šp”š‚ğ“ü—Í‚µ‚Ä‚­‚¾‚³‚¢");
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
