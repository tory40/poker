using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonSet : MonoBehaviourPunCallbacks
{
    bool maxPlayer =false;
    bool joinRoom = false;
    int typenum;
    public int joinroom;
    public void Click(int type)
    {
        typenum = type;
        switch (type)
        {
            case 1:
                PhotonNetwork.JoinRandomRoom();
                break;
            case 2:
                break;
            case 3:

                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (!maxPlayer&&joinRoom) 
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                maxPlayer = true;
            }
        }
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
    public override void OnJoinedRoom()
    {
        joinRoom = true;
    }
}