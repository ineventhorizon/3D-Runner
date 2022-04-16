using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : SceneBasedMonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera gameplayCam;
    [SerializeField] private CinemachineVirtualCamera finalCam;
    [SerializeField] public CinemachineVirtualCamera ActiveCam => CameraSwitcher.GetActiveCamera();
    private void OnEnable()
    {
        Debug.Log(ActiveCam);
        CameraSwitcher.AddCamera(gameplayCam);
        CameraSwitcher.AddCamera(finalCam);
    }

    public void SwitchCam(string cameraName)
    {
        //TODO ENUM SWITCH
        if (cameraName.Equals("GameplayCam") && !CameraSwitcher.IsActiveCamera(gameplayCam))
        {
            CameraSwitcher.SwitchCamera(gameplayCam);
        }
        else if (cameraName.Equals("FinalCam") && !CameraSwitcher.IsActiveCamera(finalCam))
        {
            CameraSwitcher.SwitchCamera(finalCam);
        }
    }
}