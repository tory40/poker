using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drow : MonoBehaviour
{
    [SerializeField] Text changetext;
    [SerializeField] GameObject changetext2;
    [SerializeField] GameObject changebutton;
    int discard;
    bool free=false;
    [SerializeField] Transform field;
    [SerializeField] CardController cardPrefab;
    public List<int> deck = new List<int>();
    List<int> numbers = new List<int>();
    List<int> marks = new List<int>();
    int cardID;
    List<CardController> cards = new List<CardController>();
    int hand = 0;
    public List<int> disdeck = new List<int>();
    public bool fastadd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drowcard(2);
            DisCard(2);
        }
    }
    public void DisCard(int i)
    {
        disdeck.Clear();
        changetext.gameObject.SetActive(true);
        changetext.text = "残り"+i.ToString()+"枚";
        free = false;
        discard = i;
        for(int j=0;j<cards.Count;++j)
        {
            cards[j].canchoice = true;
        }
        changetext.gameObject.SetActive(true);
        changetext2.SetActive(true);
        GameObject.Find("Gamemaneger").GetComponent<GameManager>().Countdownstart();
    }
    public void FreeDisCard()
    {
        fastadd = false;
        disdeck.Clear();
        changetext.gameObject.SetActive(true);
        changetext.text = "制限なし";
        free = true;
        discard = 0;
        for (int j = 0; j < cards.Count; ++j)
        {
            cards[j].canchoice = true;
        }
        changetext2.SetActive(true);
        changebutton.SetActive(true);
        GameObject.Find("Gamemaneger").GetComponent<GameManager>().Countdownstart();
    }
    public void DiscardChoice(bool add)
    {
        GameObject.Find("Gamemaneger").GetComponent<GameManager>().countdown = false;
        if (!free)
        {
            if(discard == disdeck.Count)
            {
                changetext.gameObject.SetActive(false);
                changebutton.SetActive(true);
            }
            else
            {
                changetext.gameObject.SetActive(true);
                changebutton.SetActive(false);
                changetext.text = "残り" + (discard-disdeck.Count).ToString() + "枚";
            }

        }
        else
        {
            if (add)
            {
                discard += 1;
            }
            else
            {
                discard -= 1;
            }
        }
    }
    [SerializeField] public Drow reverse;
    public void DiscardInit()
    {
        disdeck.Sort();
        disdeck.Reverse();
        for(int i=0;i<disdeck.Count;++i)
        {
            deck.RemoveAt(disdeck[i]);
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().EnemyDiscard(disdeck[i]);
        }
        //敵の処理も追加
        SortCard(false);
        if(!fastadd)
        {
            AddCard(discard);
        }
        else
        {
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().Next();
        }
    }
    public void AddCard(int i)
    {
        for(int j=0;j<i;++j)
        {
            int cardnumber = Random.Range(0, 52);
            Drowcard(cardnumber);
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().EnemyAddcard(cardnumber);
        }
        SortCard(false);
        if(fastadd)
        {
            discard = i;
            DisCard(i);
        }
        else
        {
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().Next();
        }
    }
    public void StartCard(int card1,int card2,int card3,int card4,int card5,bool hide) 
    {
        deck.Clear();
        hand = 0;
        foreach (CardController card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        Drowcard(card1);
        Drowcard(card2);
        Drowcard(card3);
        Drowcard(card4);
        Drowcard(card5);
        SortCard(hide);
    }
    public void Drowcard(int j)
    {
        deck.Add(j);
        Debug.Log(j);
    }
    public void SortCard(bool hide)
    {
        deck.Sort();
        foreach (CardController card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        for (int i = 0; i < deck.Count; ++i)
        {
            CardController card = Instantiate(cardPrefab, field, false);
            card.Init(deck[i], i);
            if(hide)
            {
                card.view.Hide();
            }
            cards.Add(card);
        }
        Layout(layout);
    }
    public void OpenCard()
    {
        for(int i=0;i<5;++i)
        {
            cards[i].view.Open();
        }
    }
    public void HideCard()
    {
        for (int i = 0; i < 5; ++i)
        {
            cards[i].view.Hide();
        }
        GameObject.Find("Gamemaneger").GetComponent<GameManager>().countdown = false;
        GameObject.Find("Gamemaneger").GetComponent<GameManager>().Next();
    }
    [SerializeField] Text typetext;
    List<string> texts = new List<string> { "High card" , "One pair" , "One pair mark" , "Two pair" , "Three of a kind" , "High card flush" , "One pair flush" , "Straight" , "Two pair twin" , "Two pair mark",
        "Three of a kind mark" ,"Full house","Full house duo","Full house torio","Two pair flush","Three of a kind flush","Four of a kind","Four of a kind half","Four of a kind rainbow","Five of a kind",
    "Straight flush","Full house mark","Full house flush","Four of a kind mark","Four of a kind flush","Five of a kind half","Five of a kind flush"};
    public int power=0;
    public string level;
   public  void Check()
    {
        //ストレートを定義
        if(numbers[0]+1 == numbers[1] && numbers[1]+1 == numbers[2] && numbers[2]+1 == numbers[3] && numbers[3]+1 == numbers[4])
        {
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 20;
            }
            else
            {
                power = 7;
            }
        }
        //ファイブカードを定義
        else if (numbers[0]==numbers[1]&&numbers[1]== numbers[2]&& numbers[2]== numbers[3]&& numbers[3]== numbers[4]) 
        {
            //five of a kind flush
            if(marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 26;
            }
            //five of a kind half
            else if ((marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3])|| (marks[0] == marks[1] && marks[1] == marks[2] && marks[3] == marks[4]) || (marks[0] == marks[1] && marks[2] == marks[3] && marks[3] == marks[4]) || (marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4]))
            {
                power = 25;
            }
            else
            {
                power = 19;
            }
        }
        //フォーカードを定義
        else if((numbers[0] == numbers[1] && numbers[1] == numbers[2] && numbers[2] == numbers[3])|| (numbers[1] == numbers[2] && numbers[2] == numbers[3] && numbers[3] == numbers[4]))
        {
            //four of a kind flush
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 24;
            }
            else if(!(numbers[0]==numbers[1]))
            {
                //four of a kind mark
                if (marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
                {
                    power = 23;
                }
                else if((marks[1] == marks[2] && marks[2] == marks[3])|| (marks[1] == marks[2] && marks[3] == marks[4]) || (marks[2] == marks[3] && marks[3] == marks[4]))
                {
                    power = 17;
                }
                else if (marks[1]==0&&marks[2]==1 && marks[3] == 2 && marks[4] == 3)
                {
                    power = 18;
                }
                else
                {
                    power = 16;
                }

            }
            else if(!(numbers[3] == numbers[4]))
            {
                //four of a kind mark
                if(marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3])
                {
                    power = 23;
                }
                else if ((marks[0] == marks[1] && marks[1] == marks[2]) || (marks[0] == marks[1] && marks[2] == marks[3]) || (marks[1] == marks[2] && marks[2] == marks[3]))
                {
                    power = 17;
                }
                else if (marks[0] == 0 && marks[1] == 1 && marks[2] == 2 && marks[3] == 3)
                {
                    power = 18;
                }
                else
                {
                    power = 16;
                }
            }
        }
        //フルハウスを定義
        else if ((numbers[0] == numbers[1] && numbers[1] == numbers[2] && numbers[3] == numbers[4]) || (numbers[0] == numbers[1] && numbers[2] == numbers[3] && numbers[3] == numbers[4]))
        {
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 22;
            }
            else if(!(numbers[1] == numbers[2]))
            {
                if(marks[0] == marks[1] && marks[2] == marks[3] && marks[3] == marks[4])
                {
                    power = 21;
                }
                else if (marks[2] == marks[3] && marks[3] == marks[4])
                {
                    power = 13;
                }
                else if(marks[0] == marks[1])
                {
                    power = 12;
                }
                else
                {
                    power = 11;
                }
            }
            else if (!(numbers[2] == numbers[3])) 
            {
                if (marks[0] == marks[1] && marks[1] == marks[2] && marks[3] == marks[4])
                {
                    power = 21;
                }
                else if (marks[0] == marks[1] && marks[1] == marks[2])
                {
                    power = 13;
                }
                else if (marks[3] == marks[4])
                {
                    power = 12;
                }
                else
                {
                    power = 11;
                }
            }
        }
        //スリーカードを定義
        else if ((numbers[0] == numbers[1] && numbers[1] == numbers[2])|| (numbers[1] == numbers[2] && numbers[2] == numbers[3]) || (numbers[2] == numbers[3] && numbers[3] == numbers[4]))
        {
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 15;
            }
            else if(numbers[0] == numbers[1])
            {
                if(marks[0] == marks[1] && marks[1] == marks[2])
                {
                    power = 10;
                }
                else
                {
                    power = 4;
                }
            }
            else if(numbers[3] == numbers[4])
            {
                if (marks[2] == marks[3] && marks[3] == marks[4])
                {
                    power = 10;
                }
                else
                {
                    power = 4;
                }
            }
            else
            {
                if (marks[1] == marks[2] && marks[2] == marks[3])
                {
                    power = 10;
                }
                else
                {
                    power = 4;
                }
            }
        }
        //ツーペアーを定義
        else if((numbers[0] == numbers[1] && numbers[2] == numbers[3]) || (numbers[0] == numbers[1] && numbers[3] == numbers[4]) || (numbers[1] == numbers[2] && numbers[3] == numbers[4]))
        {
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 14;
            }
            else if (!(numbers[0] == numbers[1]))
            {
                if(marks[1]==marks[2]&& marks[3] == marks[4])
                {
                    power = 9;
                }
                else if(marks[1] == marks[3] && marks[2] == marks[4])
                {
                    power = 8;
                }
                else
                {
                    power = 3;
                }
            }
            else if (!(numbers[3] == numbers[4]))
            {
                if (marks[0] == marks[1] && marks[2] == marks[3])
                {
                    power = 9;
                }
                else if (marks[0] == marks[2] && marks[1] == marks[3])
                {
                    power = 8;
                }
                else
                {
                    power = 3;
                }
            }
            else
            {
                if (marks[0] == marks[1] && marks[3] == marks[4])
                {
                    power = 9;
                }
                else if (marks[0] == marks[3] && marks[1] == marks[4])
                {
                    power = 8;
                }
                else
                {
                    power = 3;
                }
            }
        }
        //ワンペアを定義
        else if ((numbers[0] == numbers[1])|| (numbers[1] == numbers[2]) || (numbers[2] == numbers[3]) || (numbers[3] == numbers[4]))
        {
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 6;
            }
            else if(numbers[0] == numbers[1])
            {
                if(marks[0] == marks[1])
                {
                    power = 2;
                }
                else
                {
                    power = 1;
                }
            }
            else if (numbers[1] == numbers[2])
            {
                if (marks[1] == marks[2])
                {
                    power = 2;
                }
                else
                {
                    power = 1;
                }
            }
            else if (numbers[2] == numbers[3])
            {
                if (marks[2] == marks[3])
                {
                    power = 2;
                }
                else
                {
                    power = 1;
                }
            }
            else
            {
                if (marks[3] == marks[4])
                {
                    power = 2;
                }
                else
                {
                    power = 1;
                }
            }
        }
        //ハイカードを定義
        else
        {
            if (marks[0] == marks[1] && marks[1] == marks[2] && marks[2] == marks[3] && marks[3] == marks[4])
            {
                power = 5;
            }
            else
            {
                power = 0;
            }
        }
        typetext.text = texts[power];
        level= texts[power];
    }
    [SerializeField] GridLayoutGroup layout;

    public void Layout(GridLayoutGroup layout)
    {
        int vec = (int)((700 - (cards.Count * 50)) / (cards.Count + 1)*0.6);
        layout.spacing = new Vector2(vec,0);
    }
}
