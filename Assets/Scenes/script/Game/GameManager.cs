using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    RuleInit mainrule = RuleInit.rule;
    [SerializeField] Text mypointtext;
    [SerializeField] Text mybettext;
    [SerializeField] Text enemypointtext;
    [SerializeField] Text enemybettext;
    int mypoint;
    int mybet;
    int enemypoint;
    int enemybet;

    // Start is called before the first frame update
    void Start()
    {
        mypoint = mainrule.startpoint;
        mypointtext.text = mypoint.ToString();
        enemypoint = mainrule.startpoint;
        mypointtext.text = enemypoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
