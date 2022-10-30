using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    public static void Info(object message, string label = "")
    {
        if (string.IsNullOrEmpty(label))
        {
            Debug.Log(message);
        }
        else
        {
            Debug.Log("[" + label + "] " + message);
        }
    }

    public static void Error(object message, string label = "")
    {
        if (string.IsNullOrEmpty(label))
        {
            Debug.LogError(message);
        }
        else
        {
            Debug.LogError("[" + label + "] " + message);
        }
    }
}
