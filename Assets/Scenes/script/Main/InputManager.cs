using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    InputField inputField;
    public float count;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayInit()
    {
        try
        {
            count = float.Parse(inputField.text);
            GameObject.Find("PhotonSet").GetComponent<PhotonSet>().joinroom = (int)count;
            GameObject.Find("PhotonSet").GetComponent<PhotonSet>().Click(3);
        }
        catch
        {
            Debug.Log("ÉGÉâÅ[Ç™î≠ê∂ÇµÇ‹ÇµÇΩ");
        }
    }
}
