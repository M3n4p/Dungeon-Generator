using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string[] Tags;

    public Exit[] GetExits()
    {
        return GetComponentsInChildren<Exit>();
    }

    public Exit GetRandomExit()
    {
        var exits = GetExits();
        int randomIndex = Random.Range(0, exits.Length);
        return exits[randomIndex];
    }
}
