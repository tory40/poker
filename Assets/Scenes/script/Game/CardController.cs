using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardView view;// �����ڂ̑���
    public CardModel model;// �f�[�^�̑���
    public bool canchoice=false;
    public bool choice = false;
    private void Awake()
    {
        view = GetComponent<CardView>();
    }

    public void Init(int cardID,int ID)
    {
        model = new CardModel(cardID,ID);
        view.Show(model);

    }
    public void Click()
    {
        if(canchoice)
        {
            if(choice)
            {
                gameObject.GetComponent<RectTransform>().position += new Vector3(0f,-20f,0f);
                choice = false;
                GameObject.Find("MyCard").GetComponent<Drow>().disdeck.Remove(model.card);
                GameObject.Find("MyCard").GetComponent<Drow>().DiscardChoice(false);
            }
            else
            {
                gameObject.GetComponent<RectTransform>().position += new Vector3(0f, 20f, 0f);
                choice = true;
                GameObject.Find("MyCard").GetComponent<Drow>().disdeck.Add(model.card);
                GameObject.Find("MyCard").GetComponent<Drow>().DiscardChoice(true);
            }
            
        }
    }
}
