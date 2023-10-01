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
            before = "以前";
        }
        else
        {
            before = "以降";
        }
        string allin ="";
        switch (command.allin)
        {
            case 0:
                allin = "非オールイン時使用可能";
                break;
            case 1:
                allin = "オールイン時使用可能";
                break;
            case 2:
                allin = "使用可能";
                break;
            default:
                Debug.Log("存在しないコードです");
                return;
        }
        canaction.text = turn + "ターン" + before + allin;
        for (int i=0;i<7;++i)
        {
            string mine;
            float level;
            if(command.mines[i])
            {
                mine = "自分";
            }
            else
            {
                mine = "相手";
            }
            level = command.levels[i];
            switch (command.types[i])
            {
                case "FreeChange":
                    texts[i].text = mine + "の手札を好きなだけ交換する";
                    break;
                case "Draw":
                    texts[i].text = mine + "はカードを" + ((int)level).ToString() + "枚引く";
                    break;
                case "Change":
                    texts[i].text = mine + "は手札を" + ((int)level).ToString() + "枚交換する";
                    break;
                case "Fold":
                    texts[i].text = "勝負を降りる";
                    break;
                case "Cost":
                    texts[i].text = mine + "はコストを" + level.ToString() + "倍する";
                    break;
                case "Fight":
                    texts[i].text = "勝負をする";
                    break;
                case "Open":
                    texts[i].text = mine + "は手札を公開する";
                    break;
                case "None":
                    texts[i].text = "なにもしない";
                    break;
                default:
                    return;
            }

        }
    }
}
