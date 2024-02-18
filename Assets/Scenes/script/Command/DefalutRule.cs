using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Commandobject", menuName = "create commandobject")]
public class DefalutRule : ScriptableObject
{
    public enum Types
    {
        None,
        Cost,
        Change,
        Draw,
        FreeChange,
        Open,
        Fight,
        Fold,
    }
    public enum AllIn
    {
        Defalt,
        Allin,
        Free,
    }
    [SerializeField] List<Types> types = new List<Types> { Types.None, Types.None, Types.None, Types.Cost, Types.None, Types.None, Types.None, };
    //public List<string> types = new List<string> {"None", "None", "None", "Cost", "None", "None", "None"};
    public List<bool> mines = new List<bool> {true, true, true, true, true, true, true,};
    public List<float> levels = new List<float> {0, 0, 0, 1, 0, 0, 0};
    //èCê≥
    [SerializeField] AllIn allin = AllIn.Allin;
    public string commandname = "ñ¢ìoò^";
    public int speed = 0;
    public int canturn = 1;
    public bool beforeturn = false;
    public bool objectbool = true;
    public  string Gettype(int i)
    {
        return types[i].ToString();
    }
    int j;
    public int GetAllin()
    {

        switch(allin)
        {
            case AllIn.Defalt:
                j=0 ;
                break;
            case AllIn.Allin:
                j=1;
                break;
            case AllIn.Free:
                j=2;
                break;
        }
        return j;
    }
}
