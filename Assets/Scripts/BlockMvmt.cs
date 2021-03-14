using System.Collections;
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
        Debug.Log(this.gameObject.tag);
    }
    private void OnMouseUp()
    {
        isPressed = false;
    }

}
