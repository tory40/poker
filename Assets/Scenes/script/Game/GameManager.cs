using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;


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
    [SerializeField] Text endbattletext;
    [SerializeField] Text fighttext;
    [SerializeField] Text nowbattletext;
    [SerializeField] Text nowturntext;
    int endbattle;
    int fight;
    int nowbattle=0;
    int nowturn;
    [SerializeField] Text addtimetext;
    [SerializeField] Text oncetimetext;
    float addtime;
    float oncetime;
    float counttime;
    [SerializeField] Transform commandtrans;
    [SerializeField] CommandInit commandprefab;
    List<CommandInit> commandlist = new List<CommandInit>();
    public bool countdown =false;
    // Start is called before the first frame update
    void Start()
    {
        mypoint = mainrule.startpoint;
        mypointtext.text = mypoint.ToString();
        enemypoint = mainrule.startpoint;
        enemypointtext.text = enemypoint.ToString();
        types = mainrule.types;
        endbattle = mainrule.endbattle;
        endbattletext.text = endbattle.ToString();
        fight = mainrule.fight;
        fighttext.text = fight.ToString();
        types.Sort((a,b)=>(int)((b.strong-a.strong)*10000));
        addtime = mainrule.addtime;
        addtimetext.text = addtime.ToString();
        oncetime = mainrule.time;
        oncetimetext.text = oncetime.ToString();
        for (int i=0;i<27;++i)
        {
            Typejudge judge = Instantiate(typesample, typeplace, false);
            judge.White();
            judge.transform.Find("Typetext").GetComponent<Text>().text=types[i].type;
            typestrongs.Add(judge);
        }

        for(int i = 0; i < 30; ++i)
        {
            CommandInit command = Instantiate(commandprefab, commandtrans, false);
            command.allin = mainrule.CommandList[i].allin;
            command.commandname = mainrule.CommandList[i].commandname;
            command.speed = mainrule.CommandList[i].speed;
            command.objectnumber = mainrule.CommandList[i].objectnumber;
            command.canturn = mainrule.CommandList[i].canturn;
            command.beforeturn = mainrule.CommandList[i].beforeturn;
            command.objectbool = mainrule.CommandList[i].objectbool;
            command.createnumber = i;
            for(int j = 0; j < 7; ++j)
            {
                command.types.Add (mainrule.CommandList[i].elements[j].type);
                command.mines.Add(mainrule.CommandList[i].elements[j].mine);
                command.levels.Add(mainrule.CommandList[i].elements[j].level);
            }
            command.Rename(command.commandname);
            commandlist.Add(command);
            if (!command.objectbool)
            {
                command.gameObject.SetActive(false);
            }
         }
        var hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable["Score"] = 1;
        hashtable["Message"] = "こんにちは";
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        Countdownstart();
    }
    public void Countdownstart()
    {
        oncetime = mainrule.time;
        countdown = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(countdown)
        {
            if(oncetime>0)
            {
                oncetime -= Time.deltaTime;
                if(oncetime < 0)
                {
                    oncetime = 0;
                }
                oncetimetext.text = oncetime.ToString("0.00");
            }
            else
            {
                addtime -= Time.deltaTime;
                addtimetext.text = addtime.ToString("0.00");
                if(addtime<0)
                {
                    //タイムアウト1
                }
            }
        }
        if(mycommandchoice&&enemycommandchoice)
        {
            mycommandchoice = false;
            enemycommandchoice = false;
            CommandStart();
        }
    }
    [PunRPC]
    public void Game()
    {
        ++nowbattle;
        nowbattletext.text = nowbattle.ToString();
        nowturn = 1;
        nowturntext.text = nowturn.ToString();
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
    int count;
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // カスタムプロパティが更新されたプレイヤーのプレイヤー名とIDをコンソールに出力する
        Debug.Log($"{targetPlayer.NickName}({targetPlayer.ActorNumber})");

        

        // 更新されたプレイヤーのカスタムプロパティのペアをコンソールに出力する
        foreach (var prop in changedProps)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
            if (prop.Key.ToString() == "Score")
            {
                if((int)prop.Value == 1)
                {
                    count++;
                    Debug.Log(count);
                }
            }
        }
        if (count == 2)
        {
            Game();
            photonView.RPC(nameof(Game), RpcTarget.Others);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // 更新されたルームのカスタムプロパティのペアをコンソールに出力する
        foreach (var prop in propertiesThatChanged)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }
    }
    public void Endbattle()
    {
        //終了か確認
        Game();
    }
    public CommandInit mycommand;
    public CommandInit enemycommand;
    [SerializeField] GameObject commandpanel;
    public bool mycommandchoice=false;
    public bool enemycommandchoice=false;
    public float mylandom;
    public float enemylandom;
    public void ChoiceCommand(CommandInit command)
    {
        mycommand = command;
        mylandom = Random.Range(0f, 1f);
        photonView.RPC(nameof(SendCommand), RpcTarget.Others,command.createnumber,mylandom);
        commandpanel.SetActive(false);
        mycommandchoice = true;
    }
    [PunRPC]
    public void SendCommand(int num,float landom)
    {
        enemylandom = landom;
        enemycommand = commandlist[num];
        enemycommand.actmine = false;
        enemycommandchoice = true;
    }
    public List<int> actinit;
    public void CommandStart()
    {
        if(mycommand.types[3] == "Fight")
        {
            mylandom += 1;
        }
        if (enemycommand.types[3] == "Fight")
        {
            enemylandom += 1;
        }
        if (mycommand.types[3] == "Fold")
        {
            mylandom -= 1;
        }
        if (enemycommand.types[3] == "Fold")
        {
            enemylandom -= 1;
        }
        if (mycommand.speed==enemycommand.speed)
        {
            //(同速)
            if(mylandom<enemylandom)
            {
                actinit = new List<int> {0,1,2,7,8,9,3,10,4,5,6,11,12,13};
            }
            else
            {
                actinit = new List<int> {7,8,9,0,1,2,10,3,11,12,13,4,5,6};
            }
        }
        else if(mycommand.speed > enemycommand.speed)
        {
            //(先行)
            actinit = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13};
        }
        else
        {
            //(後攻)
            actinit =new List<int> {7,8,9,10,11,12,13,0,1,2,3,4,5,6};
        }
        loop = 0;
        LoopInit();

    }
    public int loop = 0;
    public void LoopInit()
    {
        if (loop==14)
        {
            Countdownstart();
        }
        else
        {
            OnceInit();
        }
    }
    public void OnceInit()
    {
        if(actinit[loop]<7)
        {
            //自分の処理
            switch (mycommand.types[actinit[loop]])
            {
                case "FreeChange":
                    FreeChange();
                    break;
                case "Draw":
                    
                    break;
                case "Change":
                    
                    break;
                case "Fold":
                    
                    break;
                case "Cost":
                    
                    break;
                case "Fight":
                    
                    break;
                case "Open":
                    
                    break;
                case "None":
                    
                    break;
                default:
                    return;
            }
        }
    }
    
    public void EnemyDiscard(int i)
    {
        photonView.RPC(nameof(EnemyDiscard2), RpcTarget.Others, i);
    }
    [PunRPC]
    public void EnemyDiscard2(int i)
    {
        enemydrow.deck.RemoveAt(i);
        enemydrow.SortCard();
    }
    public void EnemyAddcard(int i)
    {
        photonView.RPC(nameof(EnemyAddcard2), RpcTarget.Others, i);
    }
    [PunRPC]
    public void EnemyAddcard2(int i)
    {
        enemydrow.Drowcard(i);
        enemydrow.SortCard();
    }
    public void FreeChange()
    {
        mydrow.FreeDisCard();
    }
    
}
