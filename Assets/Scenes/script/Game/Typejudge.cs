using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typejudge : MonoBehaviour
{
    [SerializeField] GameObject red;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject each;
    public void Blue()
    {
        blue.SetActive(true);
    }
    public void Red()
    {
        red.SetActive(true);
    }
    public void Each()
    {
        each.SetActive(true);
    }
    public void White()
    {
        blue.SetActive(false);
        red.SetActive(false);
        each.SetActive(false);
    }
}
