using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class Drow : MonoBehaviour
{
    [SerializeField] Text changetext;
    [SerializeField] GameObject changetext2;
    [SerializeField] GameObject changebutton;
    int discard;
    bool free=false;
    [SerializeField] public Transform field;
    [SerializeField] CardController cardPrefab;
    public List<int> deck = new List<int>();
    List<int> numbers = new List<int>();
    List<int> marks = new List<int>();
    int cardID;
    public List<CardController> cards = new List<CardController>();
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
            changetext.gameObject.SetActive(false);
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
            cards.Sort((a, b) => a.model.card - b.model.card);
            deck.RemoveAt(disdeck[i]);
            Destroy(cards[disdeck[i]].gameObject);
            cards.RemoveAt(disdeck[i]);
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().EnemyDiscard(disdeck[i]);
        }

        //敵の処理も追加
        if(!fastadd)
        {
            AddCard(discard);
        }
        else
        {
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().Next();
        }
    }
    public void AddCardDefo(int i)
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
    public void AddCard(int i)
    {
        for (int j = 0; j < i; ++j)
        {
            int cardnumber = Random.Range(0, 52);
            DrowcardCopy(cardnumber,false);
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().EnemyAddcard(cardnumber);
        }
        if (fastadd)
        {
            discard = i;
            DisCard(i);
        }
        else
        {
            GameObject.Find("Gamemaneger").GetComponent<GameManager>().Next();
        }
    }
    int memory;
    public async void DrowcardCopy(int j,bool hide)
    {
        memory = 0;
        for(int i=0; i < deck.Count;++i)
        {
            if(deck[i] < j)
            {
                memory = i+1;
            }
            else
            {
                cards[i].model.card += 1;
            }
        }
        deck.Insert(memory,j);
        layout.enabled = false;
        CardController card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, transform.parent);
        card.transform.DOMove(field.position, 0.2f).SetEase(Ease.OutQuint);
        card.transform.SetParent(field, false);
        // CardController card = Instantiate(cardPrefab, field, false);
        card.Init(deck[memory], memory);
        card.model.card = memory;
        if (hide)
        {
            card.view.Hide();
        }
        cards.Add(card);
        card.transform.SetSiblingIndex(memory);
        await UniTask.Delay(200);
        layout.enabled = true;
        Layout(layout);
        await UniTask.Delay(100);
        Debug.Log(j);
        await UniTask.Delay(250);
        layout.enabled = true;
        Layout(layout);
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
    public async void SortCard(bool hide)
    {
        deck.Sort();
        foreach (CardController card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        for (int i = 0; i < deck.Count; ++i)
        {
            layout.enabled = false;
            CardController card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, transform.parent);
            card.transform.DOMove(field.position, 0.2f).SetEase(Ease.OutQuint);
            card.transform.SetParent(field, false);
            // CardController card = Instantiate(cardPrefab, field, false);
            card.Init(deck[i], i);
            if (hide)
            {
                card.view.Hide();
            }
            cards.Add(card);
            await UniTask.Delay(200);
            layout.enabled = true;
            Layout(layout);
            await UniTask.Delay(100);
        }
        await UniTask.Delay(250);
        layout.enabled = true;
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
        Debug.Log(deck.Count+"デッキ枚数");
        numbers.Clear();
        marks.Clear();
        for(int i =0;i<deck.Count;++i)
        {
            numbers.Add(deck[i]/4);
            marks.Add(deck[i] % 4);
        }
        Debug.Log(numbers[0]);
        Debug.Log(numbers[1]);
        Debug.Log(numbers[2]);
        Debug.Log(numbers[3]);
        Debug.Log(numbers[4]);
        //ストレートを定義
        if (numbers[0]+1 == numbers[1] && numbers[1]+1 == numbers[2] && numbers[2]+1 == numbers[3] && numbers[3]+1 == numbers[4])
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
