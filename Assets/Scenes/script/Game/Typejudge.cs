using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typejudge : MonoBehaviour
{
    [SerializeField] Image red;
    [SerializeField] Image blue;
    public void Blue()
    {
        blue.enabled = true;
    }
    public void Red()
    {
        red.enabled = true;
    }
    public void White()
    {
        blue.enabled = false;
        red.enabled = false;
    }

}
