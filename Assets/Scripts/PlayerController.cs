using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Vector3 shipVelocity;
    [SerializeField] AudioClip shipBoost;

    [Header("Screen-based parameters")]
    [SerializeField] float xRangeToMoveOnScreen = 8f;
    [SerializeField] float zRangeToMoveOnScreen = 4.2f;

    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Input-based parameters")]
    [SerializeField] float inputPitchFactor = -10f;
    [SerializeField] float inputRollFactor = 10f;

    [Header("Effects")]
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject [] guns;
    [SerializeField] AudioClip laserClip;


    float horizontalMove, verticalMove;
    bool isControlEnabled = true;
    
    void Update()
    {
        if (isControlEnabled)
        {
            Movement();
            Rotation();
            PlayShipMovementSFX();
            PlayShootingSFX();
            deathFX.SetActive(false);
            Shooting();
        }
    }

    void Movement()
    {
        //rb_ship.angularVelocity = Vector3.zero;
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        float xOffset = shipVelocity.x * horizontalMove * Time.deltaTime; // получаем значение сдвига с учетом ввода и независимо от кадровой частосты
        float zOffset = shipVelocity.z * verticalMove * Time.deltaTime;

        Vector3 startPos = transform.localPosition;
        float rawNewXPos = startPos.x + xOffset; // получаем новую позицию по Х, неограниченную в пространстве
        float rawNewZPos = startPos.z + zOffset; // получаем новую позицию по Y, неограниченную в пространстве

        float newXPos = Mathf.Clamp(rawNewXPos, -xRangeToMoveOnScreen, xRangeToMoveOnScreen); // получаем позицию Х, ограниченную границами
        float newZPos = Mathf.Clamp(rawNewZPos, -zRangeToMoveOnScreen + 2, zRangeToMoveOnScreen + 2); // получаем позицию Y, ограниченную границами (+2 т.к. объект смещен на -2 по оси z для выставления камеры!)

        Vector3 currentPos = new Vector3(newXPos, startPos.y, newZPos); //создаем переменную с новой позицией объекта
        transform.localPosition = currentPos; //двигаем объект на новую позицию
    }

    private void Rotation()
    {
        //т.к. в префабе перепутаны оси: x = pitch, y = roll, z = yaw. Обычно y = yaw, z = roll!
        float pitchDueToPosition = transform.localPosition.z * positionPitchFactor;
        float pitchDueToInput = verticalMove * inputPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToInput; 

        float yaw = transform.localPosition.x * positionYawFactor; 

        float roll = horizontalMove * inputRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, roll, yaw);
    }

    void PlayShipMovementSFX()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<AudioSource>().PlayOneShot(shipBoost);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
        {
            GetComponent<AudioSource>().Stop();
        }
    }
    void PlayShootingSFX()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<AudioSource>().Stop();
        }
    }


    private void DestroyShipBodyParts() // убрать детали корабля, чтоб после взрыва его не было видно
    {
        var shipDestroyables = GameObject.FindGameObjectsWithTag("ShipDestroyables");
        for (int i = 0; i < shipDestroyables.Length; i++)
        {
            Destroy(shipDestroyables[i]);
        }
        GetComponent<MeshRenderer>().enabled = false; //выключаем рендер текстуры корабля
        GetComponent<AudioSource>().enabled = false; //выключаем звук двигателя
    }
    void OnPlayerDeath() //called by string reference in CollisionHandler
    {
        isControlEnabled = false;
        deathFX.SetActive(true);
        DestroyShipBodyParts();
        FindObjectOfType<LevelLoader>().ReloadScene();
    }

    void Shooting() //todo make nice sfx
    {
        foreach (GameObject gun in guns)
        {
            var bulletEmission = gun.GetComponent<ParticleSystem>().emission;
            if (Input.GetButton("Fire1"))
            {
                bulletEmission.enabled = true;
                //laserSound.PlayOneShot(laserClip);
            }
            else
            {
                bulletEmission.enabled = false;
                //laserSound.Stop();
            }
        }
    }
}
