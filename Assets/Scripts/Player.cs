using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 shipVelocity;
    //[SerializeField] float shipRotationSpeed;

    [SerializeField] float xRangeToMoveOnScreen = 8f;
    [SerializeField] float zRangeToMoveOnScreen = 4.2f;

    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float inputPitchFactor = -10f;

    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float inputRollFactor = 10f;



    float horizontalMove, verticalMove;

    [SerializeField] AudioClip shipBoost;

    Rigidbody rb_ship;



    // Start is called before the first frame update
    void Start()
    {
        rb_ship = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        PlaySFX();
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
        float newZPos = Mathf.Clamp(rawNewZPos, -zRangeToMoveOnScreen, zRangeToMoveOnScreen); // получаем позицию Y, ограниченную границами

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

    void PlaySFX()
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


}
