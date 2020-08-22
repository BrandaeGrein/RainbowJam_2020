using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    [Tooltip("This value changes how much the character 'bounces' as they walk.")]
    [SerializeField]
    private float amplitude = 0.75f;

    [Tooltip("This value changes how quickly the character 'bounces' as they walk.")]
    [SerializeField]
    private float frequency = 0.75f;

    /// <summary>
    /// This is a basic move script that moves the player character around a space. 
    /// </summary>
    /// <param name="input_x">Value of either 1, 0, or -1 depending on direction on the X axis</param>
    /// <param name="input_z">Value of either 1, 0, or -1 depending on direction on the Z axis</param>
    public void MoveCharacter(float input_x, float input_z)
    {
        float y_movement = transform.position.y * Mathf.Sin(Time.time * frequency) * amplitude;
        Vector3 movement = new Vector3(input_x, y_movement, input_z);
        transform.position += movement * playerSpeed * Time.deltaTime;
    }


}
