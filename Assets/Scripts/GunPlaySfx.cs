using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlaySfx : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        PlayShooting();
    }

    void PlayShooting()
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
}
