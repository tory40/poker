using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view;// Œ©‚½–Ú‚Ì‘€ì
    CardModel model;// ƒf[ƒ^‚Ì‘€ì

    private void Awake()
    {
        view = GetComponent<CardView>();
    }

    public void Init(int cardID,int ID)
    {
        model = new CardModel(cardID,ID);
        view.Show(model);

    }
    public void OnClick()
    {
        Drow.Click(model.card);
    }
}
