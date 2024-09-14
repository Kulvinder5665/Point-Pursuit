using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras; // Array to hold your cameras
    private int currentCameraIndex = 0; // Index to track the current camera

    private GameManager gameManagerX;

    private void Start()
    {
       
        // Deactivate all cameras except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    private void Update()
    {
        // Switch camera on mouse left button click
        if (Input.GetMouseButtonDown(0) ) // Mouse left button
        {
            // Deactivate the current camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Move to the next camera
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Activate the new camera
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
