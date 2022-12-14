using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view;// 見た目の操作
    CardModel model;// データの操作

    private void Awake()
    {
        view = GetComponent<CardView>();
    }

    public void Init(int cardID)
    {
        model = new CardModel(cardID);
        view.Show(model);

    }
}
