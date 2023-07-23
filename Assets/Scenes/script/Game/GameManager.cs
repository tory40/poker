using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Drow mydrow;
    [SerializeField] Drow enemydrow;
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
        enemypointtext.text = enemypoint.ToString();
        types = mainrule.types;
        types.Sort((a,b)=>(int)((b.strong-a.strong)*10000));
        for (int i=0;i<27;++i)
        {
            Typejudge judge = Instantiate(typesample, typeplace, false);
            judge.White();
            judge.transform.Find("Typetext").GetComponent<Text>().text=types[i].type;
            typestrongs.Add(judge);
        }
        Game();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Game()
    {
        int card1=  Random.Range(0, 52);
        int card2 = Random.Range(0, 52);
        int card3 = Random.Range(0, 52);
        int card4 = Random.Range(0, 52);
        int card5 = Random.Range(0, 52);
        mydrow.StartCard(card1,card2,card3,card4,card5);
        photonView.RPC(nameof(EnemyGame), RpcTarget.Others,card1,card2,card3,card4,card5);
        mydrow.Check();
    }
    [PunRPC]
    public void EnemyGame(int card1,int card2,int card3,int card4,int card5)
    {
        enemydrow.StartCard(card1, card2, card3, card4, card5);
    }
}
