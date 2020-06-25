using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake ()
    {
        int countMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; //синглтон по фоновой музыке
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
