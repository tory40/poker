using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public int number;
    public int marknum;
    public int card;
  public CardModel(int cardID,int ID)
    {
        card = ID;
        number = cardID / 4;
        marknum = cardID % 4;
    }


}
