using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Commandobject", menuName = "create commandobject")]
public class DefalutRule : ScriptableObject
{
    public List<string> types = new List<string> {"None", "None", "None", "Cost", "None", "None", "None"};
    public List<bool> mines = new List<bool> {true, true, true, true, true, true, true,};
    public List<float> levels = new List<float> {0, 0, 0, 1, 0, 0, 0};
    public int allin = 0;
    public string commandname = "–¢“o˜^";
    public int speed = 0;
    public int canturn = 1;
    public bool beforeturn = false;
    public bool objectbool = true;
}
