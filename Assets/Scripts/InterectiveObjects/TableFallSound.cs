using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFallSound : MonoBehaviour
{
    public AudioClip fallSound; // Звук падения
    public float minFallVelocity = 2.0f; // Минимальная скорость падения для воспроизведения звука
    public AudioSource audioSource;
    public Rigidbody2D rb;

    private float previousYVelocity; // Хранит скорость объекта по оси Y в предыдущем кадре
    private bool isFalling; // Проверка, находится ли объект в состоянии падения

    private void Awake()
    {
        // Получаем компоненты
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("На объекте отсутствует компонент Rigidbody2D!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Настройка источника звука
        audioSource.playOnAwake = false;
        audioSource.clip = fallSound;
    }

    private void Update()
    {
        // Определяем, падает ли объект
        if (rb.velocity.y < 0 && Mathf.Abs(rb.velocity.y) >= minFallVelocity)
        {
            isFalling = true; // Объект падает
        }
        else
        {
            isFalling = false; // Объект не падает
        }

        // Сохраняем текущую скорость по оси Y для анализа
        previousYVelocity = rb.velocity.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем: объект должен быть в состоянии падения и остановиться после столкновения
        if (isFalling && Mathf.Abs(previousYVelocity) >= minFallVelocity)
        {
            audioSource.PlayOneShot(fallSound); // Проигрываем звук падения
            isFalling = false; // Сбрасываем состояние падения
        }
    }
}
