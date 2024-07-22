using UnityEngine;

public class GravityManipulation : MonoBehaviour
{
    public GameObject hologram;
    public float hologramDistance = 2f;
    private Vector3 gravityDirection = Vector3.down;
    private bool isHologramActive = false;

    void Update()
    {
        HandleGravityDirection();
        if (Input.GetKeyDown(KeyCode.Return) && isHologramActive)
        {
            Physics.gravity = gravityDirection * 9.81f;
            hologram.SetActive(false);
            isHologramActive = false;
        }
    }

    void HandleGravityDirection()
    {
        Vector3 newDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            newDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            newDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            newDirection = Vector3.right;
        }

        if (newDirection != Vector3.zero)
        {
            isHologramActive = true;
            hologram.SetActive(true);
            hologram.transform.position = transform.position + newDirection * hologramDistance;
            gravityDirection = newDirection;
        }
    }
}
