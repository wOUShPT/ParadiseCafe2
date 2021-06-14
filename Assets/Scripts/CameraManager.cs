using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineFreeLook playerCamera;

    private CinemachineVirtualCamera brodelCamera;

    private CinemachineVirtualCamera rapeCamera;

    private CinemachineVirtualCamera prisonEndingCamera;

    private CinemachineVirtualCamera paradiseEndingcamera;

    private CinemachineVirtualCamera bustedVelhaCamera;
    
    private CinemachineVirtualCamera bustedBofiaCamera;
    
    private void Start()
    {
        playerCamera = FindObjectOfType<CinemachineFreeLook>();
        
        brodelCamera = GameObject.FindGameObjectWithTag("BordelCamera").GetComponent<CinemachineVirtualCamera>();

        rapeCamera = GameObject.FindGameObjectWithTag("RapeCamera").GetComponent<CinemachineVirtualCamera>();
        
        bustedVelhaCamera = GameObject.FindGameObjectWithTag("BustedVelhaCamera").GetComponent<CinemachineVirtualCamera>();
        
        bustedBofiaCamera = GameObject.FindGameObjectWithTag("BustedBofiaCamera").GetComponent<CinemachineVirtualCamera>();
        
        paradiseEndingcamera = GameObject.FindGameObjectWithTag("SenhorTonoCamera").GetComponent<CinemachineVirtualCamera>();
    }


    public void SwitchCamera(string nextLevel)
    {
        switch (nextLevel)
        {
            case "Exterior":

                playerCamera.Priority = 10;
                brodelCamera.Priority = 0;
                rapeCamera.Priority = 0;
                //prisonEndingCamera.Priority = 0;
                //goodEndingcamera.Priority = 0;
                break;

            case "Rape":

                playerCamera.Priority = 0;
                brodelCamera.Priority = 0;
                rapeCamera.Priority = 10;
                //prisonEndingCamera.Priority = 0;
                //goodEndingcamera.Priority = 0;
                break;

            case "CholdraEnding":
                
                playerCamera.Priority = 0;
                brodelCamera.Priority = 0;
                rapeCamera.Priority = 0;
                //prisonEndingCamera.Priority = 10;
                //goodEndingcamera.Priority = 0;
                break;
                
            case "Cafe":
                
                playerCamera.Priority = 10;
                brodelCamera.Priority = 0;
                rapeCamera.Priority = 0;
                //prisonEndingCamera.Priority = 0;
                //goodEndingcamera.Priority = 0;
                break;
            
            case "Brothel":
                
                playerCamera.Priority = 0;
                brodelCamera.Priority = 10;
                rapeCamera.Priority = 0;
                //prisonEndingCamera.Priority = 0;
                //goodEndingcamera.Priority = 0;
                break;
            
            case "ParadiseEnding":
                
                playerCamera.Priority = 0;
                brodelCamera.Priority = 0;
                rapeCamera.Priority = 0;
                //prisonEndingCamera.Priority = 0;
                paradiseEndingcamera.Priority = 10;
                break;
            
            case "BustedVelha":

                playerCamera.Priority = 0;
                bustedVelhaCamera.Priority = 10;
                break;
            
            case "BustedBofia":

                playerCamera.Priority = 0;
                bustedBofiaCamera.Priority = 10;
                break;

        }
    }
}
