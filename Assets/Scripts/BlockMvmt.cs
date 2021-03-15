using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMvmt : MonoBehaviour
{
    public bool isPressed = false;
    public int MovingDone = 0;
    public bool onDest = false;
    public Vector3 targetPos;
    public Vector3 currentPos;
    public Vector3 lastPos;
    public bool collided = false;
    public RaycastHit2D Down;
    public int order;

    private void OnMouseDown()
    {
        if (!onDest)
        {
            isPressed = true;  //detect wheter the block is selected
        }
    }
    private void OnMouseUp()
    {
        if (!onDest)
        {
            isPressed = false;  
        }
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

    public void checkDest()
    {
        //if the block move to its destination then stop movement and selection
        if(GameObject.Find(gameObject.tag + "Dest(Clone)").transform.position == this.transform.position)
        {
            onDest = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Yellow" | collision.tag == "Blue"| collision.tag == "Red")
        {
            //if collided with other blocks then both step back
            collided = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Yellow" | collision.tag == "Blue" | collision.tag == "Red")
        {
            collided = false;
        }
    }

    //due to time limit, this is the fastest way i can solve the moving problem, but there sure will be more elegant ways
    public IEnumerator Move()
    {
       
        if (MovingDone == 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1f); //detect the block forward
            Down = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1f);  //detect the block backward
            if (hit) //if forward block is in grid
            {
                if (hit.collider.tag == "Grid") //if forward block does not have another block on it
                {
                    targetPos = hit.collider.transform.position; //let block move forward
                    transform.position = targetPos;

                    if (collided == false)  //if no other block is moving toward this place
                    {

                        yield return new WaitForSeconds(.2f); //keep checking for movement
                        yield return Move();
                    }
                }
                else if (hit.collider.GetComponent<BlockMvmt>().MovingDone == 0) //if other block is around
                {
                    if (order > hit.collider.GetComponent<BlockMvmt>().order) //check moving order, block with higher order goes first
                    {
                        yield return new WaitForSeconds(.2f);
                        yield return Move();
                    }
                    else
                    {
                        MovingDone = 1;
                    }
                }

            }
            if (collided == true) //if blocks collided
            {
                transform.position = Down.collider.transform.position; //both step backward
                MovingDone = 1;
                yield break;
            }
            else
            {
                MovingDone = 1; //if the direction is out of grid's bound then don't move
                yield break;
            }
        }
        else
        {
            yield break; //if is on destination and can't move then don't move
        }
    }




}
