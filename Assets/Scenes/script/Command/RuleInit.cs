using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleInit : MonoBehaviour
{
    public List<CommandObject> CommandList = new List<CommandObject>();
    public static RuleInit rule;
    public int startpoint = 10000;
    public int betpoint = 100;
    public float winmine = 1;
    public float losemine = 0;
    public float winenemy = 1;
    public float loseenemy = 0;
    public bool winbool = true;
    public int winscore = 999999;
    public int endbattle = 99;
    [SerializeField] MainRule serectrule;

    // Start is called before the first frame update
    private void Awake()
    {
        
        if (rule == null)
        {
            rule = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Change()
    {
        CommandList = serectrule.CommandList;
    }
}
