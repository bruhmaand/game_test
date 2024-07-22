using UnityEngine;
using TMPro;

public class CubeCollector : MonoBehaviour
{
    private int totalCubes;
    private int collectedCubes;
    public GameObject winCanvas; // Reference to the Win Canvas

    void Start()
    {
        totalCubes = GameObject.FindGameObjectsWithTag("CollectibleCube").Length;
        collectedCubes = 0;
        winCanvas.SetActive(false); // Make sure the Win Canvas is hidden initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectibleCube"))
        {
            collectedCubes++;
            Destroy(other.gameObject);

            if (collectedCubes >= totalCubes)
            {
                ShowWinMessage();
            }
        }
    }

    void ShowWinMessage()
    {
        Debug.Log("You collected all the cubes!");
        winCanvas.SetActive(true); // Show the Win Canvas
        // Implement additional win condition logic here, such as stopping the timer
    }
}
