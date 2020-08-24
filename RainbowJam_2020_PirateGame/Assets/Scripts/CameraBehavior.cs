using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehavior : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private GameObject Player;

    [Tooltip("How far the camera is behind the character")]
    [SerializeField]
    private float radius = 5;

    [Tooltip("How high the camera is off the ground")]
    [SerializeField]
    private float height = 5;

    // This value will determine where the camera is around the player based on Unit Circle calculations
    private float rotationAngle;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("InputManager::Start() -- Player object is null.");
        }
        rotationAngle = 90;
        MoveCamera();
    }

    /// <summary>
    /// This method moves the camera to follow the player based on the calculations
    /// </summary>
    public void MoveCamera()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 cameraTargetPosition = playerPosition + CalculateCameraPosition() ;
        transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, 0.3f);
        transform.LookAt(playerPosition);
    }

    public void RotateCamera_Left()
    {
        rotationAngle += 1f;
        MoveCamera();
    }

    public void RotateCamera_Right()
    {
        rotationAngle -= 1f;
        MoveCamera();
    }

    /// <summary>
    /// Calculates where the camera should be based on unit circle calculations. 
    /// </summary>
    /// <returns>Returns the Vector3 that is then added to the the current player position to move the camera</returns>
    private Vector3 CalculateCameraPosition()
    {
        Vector3 playerPosition = Player.transform.position;
        float x = (Mathf.Cos((rotationAngle * Mathf.PI) / 180))*radius ;
        float z = (Mathf.Sin((rotationAngle * Mathf.PI) / 180))*radius ;
        return new Vector3(x, height, z);
    }

}
