using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunCamera : MonoBehaviour
{
    [SerializeField] float targetSpeed = 100f;
    [SerializeField] float acceleration = 1.5f;

    CinemachineVirtualCamera myCamera;
    CinemachineTrackedDolly dolly;

    float currentSpeed;


    void Start()
    {
        SetupCamera();
    }

    void Update()
    {
        MoveDolly();
    }

    private void SetupCamera()
    {
        myCamera = GetComponent<CinemachineVirtualCamera>();
        dolly = myCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
    }

    private void MoveDolly()
    {
        CalculateSpeed();
        dolly.m_PathPosition = dolly.m_PathPosition + currentSpeed * Time.deltaTime;
    }

    private void CalculateSpeed()
    {
        // Set speed to target speed if difference is smaller than acceleration value, or acceleration is set to 0
        if (Math.Abs(currentSpeed - targetSpeed) <= acceleration | Math.Abs(acceleration) < float.Epsilon)
        {
            currentSpeed = targetSpeed;
            return;
        }
        // Accelerate
        else if (currentSpeed < targetSpeed)
        {
            currentSpeed += acceleration;
        }
        // Decelerate
        else
        {
            currentSpeed -= acceleration;
        }
    }

}
