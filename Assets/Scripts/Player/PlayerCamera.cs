using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public PlayerData playerData;

    // How far up and down the camera can be rotated
    public float rotationLimit = 89f;
    // Object to rotate camera left and right
    [SerializeField] private Transform cameraYaw;
    // Object to rotate camera up and down
    [SerializeField] private Transform cameraPitch;

    // Vector to track rotation
    private Vector2 rotation;

    // Subscribe to events
    private void OnEnable()
    {
        playerData.Events.OnLookInput += UpdateLookInput;
    }

    // Unsubscribe to events
    private void OnDisable()
    {
        playerData.Events.OnLookInput -= UpdateLookInput;
    }


    void Update()
    {
        // Rotate camera left and right
        cameraYaw.Rotate(Vector3.up * rotation.x);

        // Rotate camera up and down
        // Convert rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.right);
        // Set camera pitch
        cameraPitch.localRotation = yQuat;
    }

    private void UpdateLookInput(Vector2 lookInput)
    {
        rotation.x = lookInput.x;
        rotation.y -= lookInput.y;
        // Limit how far up and down the camera can rotate
        rotation.y = Mathf.Clamp(rotation.y, -rotationLimit, rotationLimit);
    }
}
