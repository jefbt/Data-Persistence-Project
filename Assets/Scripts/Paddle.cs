using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isMouseControl = true;
    float lastMouseX = 0;

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        
        if (Input.GetMouseButtonDown(0) || mouseX != lastMouseX) isMouseControl = true;
        else if (Input.GetKeyDown(KeyCode.Space) || input != 0f) isMouseControl = false;

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (isMouseControl)
            pos.x = mouseX;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
        lastMouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
}
