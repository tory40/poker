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
    public List<PokerType> types;
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
        startpoint = serectrule.startpoint;
        betpoint = serectrule.betpoint;
        winmine = serectrule.winmine;
        losemine = serectrule.losemine;
        winenemy = serectrule.winenemy;
        loseenemy = serectrule.loseenemy;
        winbool = serectrule.winbool;
        winscore = serectrule.winscore;
        endbattle = serectrule.endbattle;
        types = serectrule.types;
    }
    public void Default()
    {
        CommandList = serectrule.DefaultList;
        startpoint = 10000;
        betpoint = 100;
        winmine = 1;
        losemine = 0;
        winenemy = 1;
        loseenemy = 0;
        winbool = true;
        winscore = 999999;
        endbattle = 99;
        types = serectrule.defaulttypes;
    }
}
