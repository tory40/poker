using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleInit : MonoBehaviour
{
    public static RuleInit rule;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (rule == null)
        {
            rule = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
