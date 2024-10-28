using UnityEngine;

public class CameraOrientationHandler : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera

    [SerializeField]
    public Vector3 menuPosition = new Vector3(0, 5, -10); // Position for menu

    [SerializeField]
    public Vector3 menuHorizontalPosition = new Vector3(0, 10, -10); // Position for horizontal menu

    [SerializeField]
    public Vector3 settingsPosition = new Vector3(0, 5, -10); // Position for settings
    
    [SerializeField]
    public Vector3 settingsHorizontalPosition = new Vector3(0, 10, -10); // Position for horizontal settings

    private DeviceOrientation lastOrientation;

    public int currentScreen = 0;
    // 0 for main menu, 1 for settings

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        lastOrientation = Input.deviceOrientation;
        UpdateCameraPosition(lastOrientation);
    }

    void Update()
    {
        DeviceOrientation currentOrientation = Input.deviceOrientation;
        Debug.Log(currentOrientation);


        if (currentOrientation != lastOrientation)
        {
            Debug.Log("Orientation changed");
            UpdateCameraPosition(currentOrientation);
            lastOrientation = currentOrientation;
        }
    }

    void UpdateCameraPosition(DeviceOrientation orientation)
    {
        if (currentScreen == 0)
        {
            switch (orientation)
            {
                case DeviceOrientation.Portrait:
                case DeviceOrientation.PortraitUpsideDown:
                    mainCamera.transform.position = menuPosition;
                    break;

                case DeviceOrientation.LandscapeLeft:
                case DeviceOrientation.LandscapeRight:
                    mainCamera.transform.position = menuHorizontalPosition;
                    break;

                default:
                    // Keep the current position if the orientation is FaceUp, FaceDown, or Unknown
                    break;
            }
        }
        else
        {
            switch (orientation)
            {
                case DeviceOrientation.Portrait:
                case DeviceOrientation.PortraitUpsideDown:
                    mainCamera.transform.position = settingsPosition;
                    break;

                case DeviceOrientation.LandscapeLeft:
                case DeviceOrientation.LandscapeRight:
                    mainCamera.transform.position = settingsHorizontalPosition;
                    break;

                default:
                    // Keep the current position if the orientation is FaceUp, FaceDown, or Unknown
                    break;
            }
        }
    }
}
