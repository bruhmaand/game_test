using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 0.125f;

    private Vector3 customUp = Vector3.up;

    void LateUpdate()
    {
        // Calculate the desired position behind the player
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotate the camera to look at the player, considering the new up direction
        Vector3 lookDirection = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, customUp);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
    }

    public void SetCustomUp(Vector3 newCustomUp)
    {
        customUp = newCustomUp;
    }
}
