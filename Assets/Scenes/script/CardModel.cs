using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public int number;
    public int marknum;
  public CardModel(int cardID)
    {
        number = cardID / 4;
        marknum = cardID % 4;
    }


}
