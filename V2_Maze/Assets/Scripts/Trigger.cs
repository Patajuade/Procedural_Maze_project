using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    bool State; 
    void OnTriggerEnter(Collider other)
    {
        State = true;
    }
    void OnTriggerExit(Collider other)
    {
        State = false;
    }

    public bool GetState()
    {
        return State;
    }
}
