using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCamera : MonoBehaviour
{
    public Camera cameras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            cameras.orthographicSize += 10;
            // Здесь можно добавить ваш код для обработки нажатия клавиши пробел
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            cameras.orthographicSize -= 10;
            // Здесь можно добавить ваш код для обработки нажатия клавиши пробел
        }
    }
}
