using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    //Binds input manager to this trigger
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();

        if (inputManager == null)
        {
            Debug.LogError("ConversationTrigger::Start() -- The inputManager is null");
        }
    }

    //If player enters conversation trigger, add it to the list of touched triggers
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            inputManager.touchedTriggers.Add(this);
        }
    }

    //If player exits conversation trigger, remove it from list of touched triggers
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            inputManager.touchedTriggers.Remove(this);
        }
    }
}
