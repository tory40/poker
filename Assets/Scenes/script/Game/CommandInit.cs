using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandInit : MonoBehaviour
{
    public int allin = 0;
    public string commandname = "ñ¢ìoò^";
    public int speed = 0;
    public int objectnumber = 0;
    public int canturn = 1;
    public bool beforeturn = false;
    public bool objectbool = false;
    public List<string> types = new List<string>();
    public List<bool> mines = new List<bool>();
    public List<float> levels = new List<float>();
    public int createnumber;
    public bool actmine = true;
    public bool canpush = true;
    [SerializeField] Text named;
    // Start is called before the first frame update
    void Start()
    {
        canpush = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Rename(string rename)
    {
        named.text = rename;
    }
    public void Click()
     {
        if(canpush)
        {
            GameObject.Find("Init").GetComponent<InitController>().Clickbutton(this);
            GameObject.Find("Init").transform.Find("Action").gameObject.SetActive(true);
            //ifï∂Ç≈èàóùÇï™ÇØÇÈ
            GameObject.Find("Init").transform.Find("Action/Act").gameObject.SetActive(true);
            canpush = false;
        }
    }
}
