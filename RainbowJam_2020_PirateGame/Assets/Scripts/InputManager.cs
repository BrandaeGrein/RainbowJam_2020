using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraBehavior))]
public class InputManager : MonoBehaviour
{
    //Player character is mananged in the inspector
    [SerializeField]
    private GameObject Player;

    private Movement playerMovementScript;
    private CameraBehavior cameraBehavior;

    //sets the player movement script and checks for null values
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("InputManager::Start() -- Player object is null.");
        }

        playerMovementScript = Player.GetComponent<Movement>();

        if(playerMovementScript == null)
        {
            Debug.LogError("InputManager::Start() -- The playerMovementScript is null");
        }

        cameraBehavior = GetComponent<CameraBehavior>();
    }

    //Very simply checks for any kind of player input every frame
    void Update()
    {
        if (Input.anyKey)
        {
            InputDelegation();
        }
    }

    void InputDelegation()
    {
        //"GetAxisRaw("Horizontal")" returns values between -1 and 1 for either the AD keys or arrow keys
        // Same with "GetAxisVertical
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            float input_x = 0f, input_z = 0f;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                input_x = -1;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                input_x = 1;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                input_z = -1;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                input_z = 1;
            }
            playerMovementScript.MoveCharacter(input_x, input_z);
            cameraBehavior.MoveCamera();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cameraBehavior.RotateCamera_Left();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            cameraBehavior.RotateCamera_Right();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            playerMovementScript.Stabilize();
        }

        else
        {
            // This is then where we would add in other checks such as a key code for 
            // opening dialogue, or inventory, or what have you
        }
    }
}
