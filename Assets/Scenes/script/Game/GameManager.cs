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
        hashtable["Message"] = "����ɂ���";
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
                    //�^�C���A�E�g1
                }
            }
        }
        if(mycommandchoice&&enemycommandchoice)
        {
            mycommandchoice = false;
            enemycommandchoice = false;
            Debug.Log("�f�o�b�O");
            CommandStart();
        }
    }

    [PunRPC]
    public void Game()
    {
        if(mypoint<=mainrule.betpoint)
        {
            allin = true;
            mybet = mypoint;
            mypoint = 0;
        }
        else
        {
            mybet = mainrule.betpoint;
            mypoint -= mainrule.betpoint;
        }
        if (enemypoint <= mainrule.betpoint)
        {
            enemybet = enemypoint;
            enemypoint = 0;
        }
        else
        {
            enemybet = mainrule.betpoint;
            enemypoint -= mainrule.betpoint;
        }
        ++nowbattle;
        nowbattletext.text = nowbattle.ToString();
        nowturn = 1;
        nowturntext.text = nowturn.ToString();
        int card1=  Random.Range(0, 52);
        int card2 = Random.Range(0, 52);
        int card3 = Random.Range(0, 52);
        int card4 = Random.Range(0, 52);
        int card5 = Random.Range(0, 52);
        mydrow.StartCard(card1,card2,card3,card4,card5,false);
        photonView.RPC(nameof(EnemyGame), RpcTarget.Others,card1,card2,card3,card4,card5);
        mydrow.Check();
    }
    [PunRPC]
    public void EnemyGame(int card1,int card2,int card3,int card4,int card5)
    {
        enemydrow.StartCard(card1, card2, card3, card4, card5,true);
    }
    int count;
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // �J�X�^���v���p�e�B���X�V���ꂽ�v���C���[�̃v���C���[����ID���R���\�[���ɏo�͂���
        Debug.Log($"{targetPlayer.NickName}({targetPlayer.ActorNumber})");

        

        // �X�V���ꂽ�v���C���[�̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
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
        // �X�V���ꂽ���[���̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
        foreach (var prop in propertiesThatChanged)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }
    }
    public void Endbattle()
    {
        //�I�����m�F
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
        //�l��ύX
        mylandom = Random.Range(0f,1f);
        photonView.RPC(nameof(SendCommand), RpcTarget.Others,command.createnumber,mylandom);
        commandpanel.SetActive(false);
        mycommandchoice = true;
    }
    [PunRPC]
    public void SendCommand(int num,float landom)
    {
        Debug.Log("landom"+landom);
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
            //(����)
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
            //(��s)
            actinit = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13};
        }
        else
        {
            //(��U)
            actinit =new List<int> {7,8,9,10,11,12,13,0,1,2,3,4,5,6};
        }
        loop = 0;
        Invoke(nameof(LoopInit), 2f);
    }
    public int loop = 0;
    [PunRPC]
    public void LoopInit()
    {
        ++loop;
        if (loop>=14)
        {
            loop = 0;
            //���̏�����ǉ�
            Countdownstart();
        }
        else
        {
            OnceInit();
        }
    }
    [SerializeField] GameObject hidebutton;
    public void OnceInit()
    {
        Debug.Log(actinit.Count);
        Debug.Log(loop);
        if(actinit[loop]<7)
        {
            //�����̏���
            switch (mycommand.types[actinit[loop]])
            {
                case "FreeChange":
                    if(mycommand.mines[actinit[loop]])
                    {
                        FreeChange();
                    }
                    else
                    {
                        photonView.RPC(nameof(FreeChange), RpcTarget.Others);
                    }
                    break;
                case "Draw":
                    if (mycommand.mines[actinit[loop]])
                    {
                        Draw((int)mycommand.levels[actinit[loop]]);
                    }
                    else
                    {
                        photonView.RPC(nameof(Draw), RpcTarget.Others, (int)mycommand.levels[actinit[loop]]);
                    }
                    break;
                case "Change":
                    if (mycommand.mines[actinit[loop]])
                    {
                        Change((int)mycommand.levels[actinit[loop]]);
                    }
                    else
                    {
                        photonView.RPC(nameof(Change), RpcTarget.Others, (int)mycommand.levels[actinit[loop]]);
                    }
                    break;
                case "Fold":
                    Fold(true);
                    photonView.RPC(nameof(Fold), RpcTarget.Others,false);
                    break;
                case "Cost":
                    if (mycommand.mines[actinit[loop]])
                    {
                        Cost(mycommand.levels[actinit[loop]]);
                    }
                    else
                    {
                        photonView.RPC(nameof(Cost), RpcTarget.Others, mycommand.levels[actinit[loop]]);
                    }
                    break;
                case "Fight":
                    Fight();
                    photonView.RPC(nameof(Fight), RpcTarget.Others);
                    break;
                case "Open":
                    
                    break;
                case "None":
                    Next();
                    break;
                default:
                    return;
            }
        }
    }
    public void Next()
    {
        LoopInit();
        photonView.RPC(nameof(LoopInit), RpcTarget.Others);
    }
    
    public void EnemyDiscard(int i)
    {
        photonView.RPC(nameof(EnemyDiscard2), RpcTarget.Others, i);
    }
    [PunRPC]
    public void EnemyDiscard2(int i)
    {
        enemydrow.deck.RemoveAt(i);
        enemydrow.SortCard(true);
    }
    public void EnemyAddcard(int i)
    {
        photonView.RPC(nameof(EnemyAddcard2), RpcTarget.Others, i);
    }
    [PunRPC]
    public void EnemyAddcard2(int i)
    {
        enemydrow.Drowcard(i);
        enemydrow.SortCard(true);
    }
    [PunRPC]
    public void FreeChange()
    {
        mydrow.FreeDisCard();
    }
    [PunRPC]
    public void Draw(int i)
    {
        mydrow.fastadd = true;
        mydrow.AddCard(i);
    }
    [PunRPC]
    public void Change(int i)
    {
        mydrow.fastadd = false;
        mydrow.DisCard(i);
    }
    public bool allin=false;
    [PunRPC]
    public void Cost(float level)
    {
        Debug.Log("�R�X�g");
        if(mypoint<=mybet*(level-1))
        {
            mybet += mypoint;
            mypoint = 0;
            allin = true;
        }
        else
        {
            int addbet = (int)(mybet * (level - 1));
            mybet += addbet;
            mypoint -= addbet;
        }
        mybettext.text = mybet.ToString();
        mypointtext.text = mypoint.ToString();
        photonView.RPC(nameof(EnemyCost), RpcTarget.Others, level);
    }
    [PunRPC]
    public void EnemyCost(float level)
    {
        if (enemypoint <= enemybet * (level - 1))
        {
            enemybet += enemypoint;
            enemypoint = 0;
        }
        else
        {
            int addbet = (int)(enemybet * (level - 1));
            enemybet += addbet;
            enemypoint -= addbet;
        }
        enemybettext.text = enemybet.ToString();
        enemypointtext.text = enemypoint.ToString();

        Next();
    }
    [PunRPC]
    public void Opencard()
    {
        hidebutton.SetActive(true);
        Countdownstart();
        enemydrow.OpenCard();
    }
    [PunRPC]
    public void Fight()
    {
        mydrow.Check();
        enemydrow.Check();
        if(mydrow.power<enemydrow.power)
        {
            //����̏���
            enemypoint += mybet + enemybet;
            mybet = 0;
            enemybet = 0;
        }
        else if(mydrow.power > enemydrow.power)
        {
            //�����̏���
            mypoint += mybet + enemybet;
            mybet = 0;
            enemybet = 0;
        }
        else
        {
            //�����킯
            mypoint += mybet;
            enemypoint += enemybet;
            mybet = 0;
            enemybet = 0;
        }
        //��������
        if(mypoint<=0||enemypoint<=0)
        {
            //����
        }
        else
        {
            //���s
            Game();
            Countdownstart();
        }
    }
    [PunRPC]
    public void Fold(bool mine)
    {
        if(mycommand.types[3]== "Fold"&& enemycommand.types[3] == "Fold")
        {
            //�����킯
            mypoint += mybet;
            enemypoint += enemybet;
            mybet = 0;
            enemybet = 0;
        }
        else if(mine)
        {
            //����̏���
            enemypoint += mybet + enemybet;
            mybet = 0;
            enemybet = 0;
        }
        else
        {
            //�����̏���
            mypoint += mybet + enemybet;
            mybet = 0;
            enemybet = 0;
        }
        //��������
        if (mypoint <= 0 || enemypoint <= 0)
        {
            //����
        }
        else
        {
            //���s
            Game();
            Countdownstart();
        }
    }
}
