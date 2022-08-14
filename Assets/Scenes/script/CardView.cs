using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] Text numbertext;
    [SerializeField] Image markimage;
    List<string> texts = new List<string> {"2","3","4","5","6","7","8","9","10","J","Q","K","A" };
    [SerializeField] Sprite[] markmodel;
    public void Show(CardModel cardModel)
    {
        numbertext.text = texts[cardModel.number];
        markimage.sprite = markmodel[cardModel.marknum];
    }
}
