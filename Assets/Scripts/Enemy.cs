﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathVFX;
    [SerializeField] Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider() // добавляем коллайдер с выключенным триггером к каждому врагу через скрипт, на случай если будут изменены ассеты
    {
        Collider nonTriggerBoxCollider = gameObject.AddComponent<BoxCollider>();
        nonTriggerBoxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject deathVFXInstance = Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
        deathVFXInstance.transform.parent = parent; // вносим создаваемые объекты под 1 родительский объект в иерархии
        Destroy(gameObject); // уничтожаем вражеский корабль
        FindObjectOfType<ScoreBoard>().AddToScore();
    }
}
