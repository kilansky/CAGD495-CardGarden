using UnityEngine;



/*
    ********** PUT THIS SCRIPT ON THE MAIN CAMERA FOR WATER SHADER TO WORK **********
*/



public class CameraDepthTextureMode : MonoBehaviour 
{
    [SerializeField]
    [Tooltip("Change this to \"Depth\"")]
    public DepthTextureMode depthTextureMode;

    private void OnValidate()
    {
        SetCameraDepthTextureMode();
    }

    private void Awake()
    {
        SetCameraDepthTextureMode();
    }

    private void SetCameraDepthTextureMode()
    {
        GetComponent<Camera>().depthTextureMode = depthTextureMode;
    }
}
