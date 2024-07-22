using TMPro.Examples;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    public GameObject hologramPrefab;
    private GameObject hologramInstance;
    private Vector3 projectionDirection;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) { ProjectHologram(Vector3.forward); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { ProjectHologram(Vector3.back); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { ProjectHologram(Vector3.left); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { ProjectHologram(Vector3.right); }

        if (Input.GetKeyDown(KeyCode.Return) && hologramInstance != null)
        {
            MoveToHologram();
        }
    }

    void ProjectHologram(Vector3 direction)
    {
        if (hologramInstance != null) Destroy(hologramInstance);

        projectionDirection = direction;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            hologramInstance = Instantiate(hologramPrefab, hit.point, Quaternion.identity);
            hologramInstance.transform.up = hit.normal; // Align hologram with the wall normal
        }
    }

    void MoveToHologram()
    {
        transform.position = hologramInstance.transform.position;
        transform.rotation = Quaternion.LookRotation(-hologramInstance.transform.forward, hologramInstance.transform.up);

        Vector3 newGravityDirection = -hologramInstance.transform.up;
        GetComponent<PlayerMovement>().SetGravity(newGravityDirection);

        // Update the camera's custom up direction
        Camera.main.GetComponent<CameraController>().SetCustomUp(hologramInstance.transform.up);

        Destroy(hologramInstance);
    }
}
