using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonSet : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField input;
    [SerializeField] Text text;
    bool maxPlayer =false;
    bool joinRoom = false;
    int typenum;
    public int joinroom;
    public void Click(int type)
    {
        typenum = type;
        switch (type)
        {
            //�t���[�}�b�`
            case 1:
                PhotonNetwork.JoinRandomRoom();
                break;
            //���[���I���W�i��
            case 2:
                int i = Random.Range(10000, 99999);
                text.text = i.ToString();
                PhotonNetwork.CreateRoom(i.ToString());
                break;
            //���[���f�t�H���g
            case 3:
                int j = Random.Range(10000, 99999);
                text.text = j.ToString();
                PhotonNetwork.CreateRoom(j.ToString());
                break;
            //�W���C�����[��
            case 4:
                PhotonNetwork.JoinRoom(input.text);
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
    // �����_���ŎQ���ł��郋�[�������݂��Ȃ��Ȃ�A�V�K�Ń��[�����쐬����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ���[���̎Q���l����2�l�ɐݒ肷��
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