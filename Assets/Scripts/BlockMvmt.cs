using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMvmt : MonoBehaviour
{
    public bool isPressed = false;
    public int MovingDone = 0;

    private void Awake()
    {
        
    }
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

    public void Rotate(KeyCode inputKey)
    {
        Quaternion currentRotation = this.transform.rotation; //get the original rotation of the block
        switch(inputKey)
        {
            case KeyCode.W: //if any of the following key is pressed then rotate to corresponding direction
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

    public IEnumerator MoveCo()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 10f);
        if (hit)
        {
            if(hit.collider.tag == "Grid")
            {
                transform.position = hit.collider.transform.position;
                yield return new WaitForSeconds(.5f);
                yield return MoveCo();
            }
            else 
            {
                MovingDone++;
                yield break;
            }
        }
        else
        {
            MovingDone++;
            yield break;
        }
    }

    public void Move()
    {


    }




}
