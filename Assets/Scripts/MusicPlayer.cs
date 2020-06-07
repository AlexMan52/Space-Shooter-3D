using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
