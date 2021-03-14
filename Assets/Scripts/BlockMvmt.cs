﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMvmt : MonoBehaviour
{
    public bool isPressed = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        isPressed = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
    }
    public void Move()
    {
        
    }
    public void Rotate(KeyCode inputKey)
    {
        Quaternion currentRotation = this.transform.rotation;
        switch(inputKey)
        {
            case KeyCode.W:
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 0);
                break;
            case KeyCode.A:
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 90);
                break;
            case KeyCode.S:
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 180);
                break;
            case KeyCode.D:
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 270);
                break;
            default:
                this.transform.rotation = currentRotation;
                break;

            
        }
    }


}
