using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitController : MonoBehaviour
{
    [SerializeField] List<Text> texts;
    [SerializeField] Text speed;
    [SerializeField] Text canaction;
    CommandInit holdcommand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clickbutton(CommandInit command)
    {
        holdcommand = command;
        speed.text = command.speed.ToString();
        string turn = command.canturn.ToString();
        string before;
        if (command.beforeturn)
        {
            before = "�ȑO";
        }
        else
        {
            before = "�ȍ~";
        }
        string allin ="";
        switch (command.allin)
        {
            case 0:
                allin = "��I�[���C�����g�p�\";
                break;
            case 1:
                allin = "�I�[���C�����g�p�\";
                break;
            case 2:
                allin = "�g�p�\";
                break;
            default:
                Debug.Log("���݂��Ȃ��R�[�h�ł�");
                return;
        }
        canaction.text = turn + "�^�[��" + before + allin;
        for (int i=0;i<7;++i)
        {
            string mine;
            float level;
            if(command.mines[i])
            {
                mine = "����";
            }
            else
            {
                mine = "����";
            }
            level = command.levels[i];
            switch (command.types[i])
            {
                case "FreeChange":
                    texts[i].text = mine + "�̎�D���D���Ȃ�����������";
                    break;
                case "Draw":
                    texts[i].text = mine + "�̓J�[�h��" + ((int)level).ToString() + "������";
                    break;
                case "Change":
                    texts[i].text = mine + "�͎�D��" + ((int)level).ToString() + "����������";
                    break;
                case "Fold":
                    texts[i].text = "�������~���";
                    break;
                case "Cost":
                    texts[i].text = mine + "�̓R�X�g��" + level.ToString() + "�{����";
                    break;
                case "Fight":
                    texts[i].text = "����������";
                    break;
                case "Open":
                    texts[i].text = mine + "�͎�D�����J����";
                    break;
                case "None":
                    texts[i].text = "�Ȃɂ����Ȃ�";
                    break;
                default:
                    return;
            }

        }
    }
}
