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
    [SerializeField] Typejudge typesample;
    [SerializeField] Transform typeplace;
    List<PokerType> types;
    List<Typejudge> typestrongs = new List<Typejudge>();

    // Start is called before the first frame update
    void Start()
    {
        mypoint = mainrule.startpoint;
        mypointtext.text = mypoint.ToString();
        enemypoint = mainrule.startpoint;
        mypointtext.text = enemypoint.ToString();
        types = mainrule.types;
        types.Sort((a,b)=>(int)((b.strong-a.strong)*10000));
        for (int i=0;i<26;++i)
        {
            Typejudge judge = Instantiate(typesample, typeplace, false);
            judge.White();
            typestrongs.Add(judge);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
