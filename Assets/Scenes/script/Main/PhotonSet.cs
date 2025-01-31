using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class PhotonSet : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField input;
    [SerializeField] Text text;
    bool maxPlayer = false;
    bool joinRoom = false;
    int typenum;
    public int joinroom;
    public bool quit = false;
    public bool roomhost = false;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panel2;
    [SerializeField] CommandObject commandoprafab;
    [SerializeField] CommandElement elementoprafab;
    public void Click(int type)
    {
        typenum = type;
        switch (type)
        {
            //フリーマッチ
            case 1:
                PhotonNetwork.JoinRandomRoom();
                break;
            //ルームオリジナル
            case 2:
                int i = UnityEngine.Random.Range(10000, 99999);
                text.text = i.ToString();
                var roomOptions1 = new RoomOptions();
                roomOptions1.MaxPlayers = 2;
                PhotonNetwork.CreateRoom(i.ToString(), roomOptions1);
                break;
            //ルームデフォルト
            case 3:
                int j = UnityEngine.Random.Range(10000, 99999);
                text.text = j.ToString();
                var roomOptions2 = new RoomOptions();
                roomOptions2.MaxPlayers = 2;
                PhotonNetwork.CreateRoom(j.ToString(), roomOptions2);
                break;
            //ジョインルーム
            case 4:
                panel.SetActive(true);
                panel2.SetActive(false);
                GameObject.Find("Rule").GetComponent<RuleInit>().Default();
                text.text = input.text.ToString();
                PhotonNetwork.JoinRoom(input.text);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (!maxPlayer && joinRoom)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                maxPlayer = true;
                if (typenum == 1)
                {
                    GameObject.Find("Rule").GetComponent<RuleInit>().Default();
                    JumpScene();
                }
                if (typenum == 3)
                {
                    if (roomhost)
                    {
                        photonView.RPC(nameof(TypeDf), RpcTarget.Others);
                        GameObject.Find("Rule").GetComponent<RuleInit>().Default();
                        SharePro();
                    }

                }
                if (typenum == 2)
                {
                    if (roomhost)
                    {
                        photonView.RPC(nameof(TypeDf), RpcTarget.Others);
                        GameObject.Find("Rule").GetComponent<RuleInit>().Change();
                        SharePro();
                    }
                }
            }
        }
    }
    public void Chancel()
    {
        PhotonNetwork.LeaveRoom();
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ルームの参加人数を2人に設定する
        if (typenum == 1)
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;

            PhotonNetwork.CreateRoom(null, roomOptions);
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (typenum == 2)
        {
            Click(2);
        }
        if (typenum == 3)
        {
            Click(3);
        }
    }
    public override void OnJoinedRoom()
    {
        joinRoom = true;

        var hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable["Score"] = 0;
        hashtable["Message"] = "こんにちは";
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        if (!quit)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnLeftRoom()
    {
        roomhost = false;
    }
    public override void OnCreatedRoom()
    {
        roomhost = true;
    }
    [SerializeField] RuleInit ruleinit;
    public void SharePro()
    {

        photonView.RPC(nameof(ShareInit), RpcTarget.Others, ruleinit.startpoint, ruleinit.betpoint, ruleinit.winmine, ruleinit.losemine, ruleinit.winenemy, ruleinit.loseenemy, ruleinit.winbool.ToString(), ruleinit.winscore, ruleinit.endbattle, ruleinit.time, ruleinit.addtime, ruleinit.fight);
        for (int i = 0; i < 27; ++i)
        {
            photonView.RPC(nameof(ShareRist1), RpcTarget.Others, ruleinit.types[i].strong, i);
        }
        for (int i = 0; i < 30; ++i)
        {
            photonView.RPC(nameof(ShareRist2), RpcTarget.Others, i, ruleinit.CommandList[i].allin, ruleinit.CommandList[i].commandname, ruleinit.CommandList[i].speed, ruleinit.CommandList[i].canturn, ruleinit.CommandList[i].beforeturn.ToString(), ruleinit.CommandList[i].objectbool.ToString());
            for (int j = 0; j < 7; ++j)
            {
                photonView.RPC(nameof(ShareRist3), RpcTarget.Others, i, j, ruleinit.CommandList[i].elements[j].type, ruleinit.CommandList[i].elements[j].mine.ToString(), ruleinit.CommandList[i].elements[j].level);
            }
        }
        photonView.RPC(nameof(JumpScene), RpcTarget.All);
    }
    [PunRPC]
    public void ShareInit(int startPoint, int betPoint, float winMine, float loseMine, float winEnemy, float loseEnemy, string winBool, int winScore, int endBattle, float tIme, float addTime, int fIght)
    {
        ruleinit.startpoint = startPoint;
        ruleinit.betpoint = betPoint;
        ruleinit.winmine = winMine;
        ruleinit.losemine = loseMine;
        ruleinit.winenemy = winEnemy;
        ruleinit.loseenemy = loseEnemy;
        ruleinit.winbool = Convert.ToBoolean(winBool);
        ruleinit.winscore = winScore;
        ruleinit.endbattle = endBattle;
        ruleinit.time = tIme;
        ruleinit.addtime = addTime;
        ruleinit.fight = fIght;
    }
    [PunRPC]
    public void ShareRist1(float typestrong, int i)
    {
        if (ruleinit.types.Count <= i)
        {
            PokerType aaa = new PokerType();
            ruleinit.types.Add(aaa);
        }
        ruleinit.types[i].strong = typestrong;
    }
    [SerializeField] CommandElement elementprefabs;
    [PunRPC]
    public void ShareRist2(int i, int allIn, string commandName, int sPeed, int canTurn, string beforeTurn, string objectBool)
    {
        
        CommandObject aaa = Instantiate(commandoprafab);
        ruleinit.CommandList[i] = aaa;

        ruleinit.CommandList[i].allin = allIn;
        ruleinit.CommandList[i].commandname = commandName;
        ruleinit.CommandList[i].speed = sPeed;
        ruleinit.CommandList[i].canturn = canTurn;
        ruleinit.CommandList[i].beforeturn = Convert.ToBoolean(beforeTurn);
        ruleinit.CommandList[i].objectbool = Convert.ToBoolean(objectBool);
    }
    [PunRPC]
    public void ShareRist3(int i, int j, string tYpe, string mIne, float lEvel)
    {
        CommandElement aaa = Instantiate(elementprefabs);
        ruleinit.CommandList[i].elements.Add(aaa);

        ruleinit.CommandList[i].elements[j].type = tYpe;
        ruleinit.CommandList[i].elements[j].mine = Convert.ToBoolean(mIne);
        ruleinit.CommandList[i].elements[j].level = lEvel;
    }
    [PunRPC]

    public void JumpScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    [PunRPC]
    public void TypeDf()
    {
        GameObject.Find("Rule").GetComponent<RuleInit>().Default();
    }
}