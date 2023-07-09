using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerType
{
    public List<string> texts = new List<string> { "High card" , "One pair" , "One pair mark" , "Two pair" , "Three of a kind" , "High card flush" , "One pair flush" , "Straight" , "Two pair twin" , "Two pair mark",
        "Three of a kind mark" ,"Full house","Full house duo","Full house torio","Two pair flush","Three of a kind flush","Four of a kind","Four of a kind half","Four of a kind rainbow","Five of a kind",
    "Straight flush","Full house mark","Full house flush","Four of a kind mark","Four of a kind flush","Five of a kind half","Five of a kind flush"};
    public float strong;
    public string type;
}
