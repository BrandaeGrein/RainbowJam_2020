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

    private Rigidbody rb;

    //Params for stabilizing
    private bool stabilizing;
    private Quaternion stabilizingOriginRotation;
    private float rotationPercentage;
    private float rotationRate = 0.08f;
    private float rotationHopSize = 4.5f;

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

    /// <summary>
    /// Stabilizes the player if they have fallen over
    /// </summary>
    public void Stabilize()
    {
        if (!stabilizing && (Mathf.Abs(transform.rotation.eulerAngles.x) > 45 || Mathf.Abs(transform.rotation.eulerAngles.z) > 45))
        {
            rb.AddForce(0, rotationHopSize, 0, ForceMode.Impulse);
            stabilizingOriginRotation = transform.rotation;
            stabilizing = true;
            rotationPercentage = 0;
        }
    }

    //Sets rotation based on original position when stabilizing begun
    private void SetRotation(float position)
    {
        float x = Mathf.Lerp(stabilizingOriginRotation.x, 0, position);
        float z = Mathf.Lerp(stabilizingOriginRotation.z, 0, position);
        Quaternion newRotation = new Quaternion(x, transform.rotation.y, z, transform.rotation.w);
        transform.rotation = newRotation;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Movement::Start() -- The rigidbody is null");
        }
    }

    private void FixedUpdate()
    {
        if (stabilizing)
        {
            rotationPercentage += (rotationRate * (1 - (rotationPercentage / 1.3f)));
            SetRotation(rotationPercentage);
            if (rotationPercentage >= 1) stabilizing = false;
        }
    }
}
