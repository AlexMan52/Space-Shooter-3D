﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake ()
    {
        int countMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (countMusicPlayers == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }
}
