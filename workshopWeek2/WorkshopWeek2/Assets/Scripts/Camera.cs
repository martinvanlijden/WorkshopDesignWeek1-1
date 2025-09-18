using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public GameObject player;

    private Vector3 _offset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Calculate difference between camera and player
        // Only need to do this once
        _offset = transform.position - player.transform.position;
    }

    // Runs after all the other update functions have been called
    // Good for camera movement
    private void LateUpdate()
    {
        // Each frame, set the camera's position to the player's position plus the offset
        transform.position = player.transform.position + _offset;
    }
}