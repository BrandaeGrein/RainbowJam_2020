using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehavior : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private GameObject Player;

    [SerializeField]
    private float radius = 5;

    [SerializeField]
    private float height = 3;

    private float rotationAngle;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        height = transform.position.y;
        rotationAngle = 0;
        MoveCamera();
    }

    public void MoveCamera()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 targetPosition = CalculateCameraPosition();
        Debug.Log("targetPosition = " + targetPosition);
        Vector3 cameraTargetPosition = playerPosition + targetPosition;//playerPosition + (new Vector3(0, 3, 5));
        transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, 0.3f);
        transform.LookAt(playerPosition);
    }

    public void RotateCamera_Left()
    {
        rotationAngle -= 1f;
        MoveCamera();
    }

    public void RotateCamera_Right()
    {
        rotationAngle += 1f;
        MoveCamera();
    }

    private Vector3 CalculateCameraPosition()
    {
        Vector3 playerPosition = Player.transform.position;
        float x = (Mathf.Cos((rotationAngle * Mathf.PI) / 180))*radius ;
        float z = (Mathf.Sin((rotationAngle * Mathf.PI) / 180))*radius ;

        Debug.Log("X Value for Cos func is " + Mathf.Cos(rotationAngle));
        return new Vector3(x, height, z);
    }

}
