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
    bool isAlive = true; //для остановки камеры при смерти игрока

    void Start()
    {
        SetupCamera();
    }

    void Update()
    {
        if (isAlive)
        {
            MoveDolly();
        }
    }

    private void SetupCamera() // задаем начальную позицию камеры
    {
        myCamera = GetComponent<CinemachineVirtualCamera>();
        dolly = myCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
    }

    private void MoveDolly() //задаем позицию с учетом скорости движения
    {
        CalculateSpeed();
        dolly.m_PathPosition = dolly.m_PathPosition + currentSpeed * Time.deltaTime;
    }

    private void CalculateSpeed() //считаем скорость и задаем ускорение для плавного движения камеры (ускорение убрал, синемашина сама нормально двигает)
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

    public void ChangeLiveStatus() //показываем что игрок умер, чтобы остановить камеру (вызывается в PlayerController)
    {
        isAlive = false;
    }

}
