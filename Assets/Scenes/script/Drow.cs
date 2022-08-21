using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drow : MonoBehaviour
{
    [SerializeField] Transform field;
    [SerializeField] CardController cardPrefab;
    List<int> deck = new List<int>();
    List<int> numbers = new List<int>();
    List<int> marks = new List<int>();
    int cardID;
    List<CardController> cards = new List<CardController>();
    int hand = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCard();
        Check();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCard();
            Check();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drowcard(2);
            DisCard(2);
        }
    }
    void DisCard(int i)
    {

    }

    void StartCard() 
    {
        deck.Clear();
        hand = 0;
        foreach (CardController card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        Drowcard(5);
    }
    void Drowcard(int j)
    {
        for (int i = 0; i < j; ++i)
        {
            cardID = Random.Range(0, 52);
            deck.Add(cardID);
            Debug.Log(cardID);
        }
        for (int i = hand+0; i < hand+j; ++i)
        {
            CardController card = Instantiate(cardPrefab, field, false);
            card.Init(deck[i]);
            cards.Add(card);
        }
        hand += j;
        deck.Sort();
    }
    [SerializeField] Text typetext;
    List<string> texts = new List<string> { "High card" , "One pair" , "One pair mark" , "Two pair" , "Three of a kind" , "High card flush" , "One pair flush" , "Straight" , "Two pair twin" , "Two pair mark",
        "Three of a kind mark" ,"Full house","Full house duo","Full house torio","Two pair flush","Three of a kind flush","Four of a kind","Four of a kind half","Four of a kind rainbow","Five of a kind",
    "Straight flush","Full house mark","Full house flush","Four of a kind mark","Four of a kind flush","Five of a kind half","Five of a kind flush"};
    int power=0;
    void Check()
    {
        power = -1;
        numbers.Clear();
        marks.Clear();
        for (int i = 0; i < 5; ++i)
        {
            numbers.Add(deck[i]/4);
            marks.Add(deck[i]%4);
        }
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
    }
}
