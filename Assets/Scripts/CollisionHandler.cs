using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Player hit smth");
        StartDeathSequence();
    }

    void StartDeathSequence()
    {
        gameObject.SendMessage("OnPlayerDeath"); // запуск метода в другом скрипте, который применен к тому же объекту
        //FindObjectOfType<PlayerController>().OnPlayerDeath(); // запуск метода в скрипте PlayerController, но метод должен быть public!
        FindObjectOfType<RunCamera>().ChangeLiveStatus(); 
    }
}
