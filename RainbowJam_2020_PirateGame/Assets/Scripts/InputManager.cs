using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraBehavior))]
public class InputManager : MonoBehaviour
{
    private static InputManager _inputManager;

    //Player character is mananged in the inspector
    [SerializeField]
    private GameObject Player;

    private Movement playerMovementScript;
    private CameraBehavior cameraBehavior;

    public List<ConversationTrigger> touchedTriggers;

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
            float horizontal_movement = 0f, forward_movement = 0f;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                horizontal_movement = -1;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                horizontal_movement = 1;
            }
            else if(Input.GetAxisRaw("Horizontal") == 0)
            {
               // playerMovementScript.resetForward();
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                forward_movement = -1;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                forward_movement = 1;
            }
            else if (Input.GetAxisRaw("Vertical") == 0)
            {
                //playerMovementScript.resetForward();
            }
            playerMovementScript.MoveCharacter(horizontal_movement, forward_movement);
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
        else
        {
            cameraBehavior.ResetCamera_OriginalAngle();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            playerMovementScript.Stabilize();
        }

        else if (Input.GetKey(KeyCode.F))
        {
            if(touchedTriggers.Count == 1)
            {
                // Check character it is attached to 
                // pass character name to FriendshipManager
            }
        }

        else {
            
            // This is then where we would add in other checks such as a key code for 
            // opening dialogue, or inventory, or what have you
        }
    }

    static private InputManager inputManager
    {
        get
        {
            if (_inputManager == null)
            {
                Debug.LogError("InputManager:inputManager getter - Attempt to get value of inputManager before it has been set.");
                return null;
            }
            return _inputManager;
        }
        set
        {
            if (_inputManager != null)
            {
                Debug.LogError("InputManager:inputManager setter - Attempt to set inputManager when it has already been set.");
            }
            _inputManager = value;
        }
    }
}
