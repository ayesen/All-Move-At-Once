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

    private void Update()
    {
       
    }
    private void OnMouseDown()
    {
        if (!onDest)
        {
            isPressed = true;
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
        if(GameObject.Find(gameObject.tag + "Dest(Clone)").transform.position == this.transform.position)
        {
            onDest = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Yellow" | collision.tag == "Blue"| collision.tag == "Red")
        {
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
    public IEnumerator MoveGo()
    {
        if (MovingDone == 0)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1f);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1f);
            if (hit)
            {
                if (collided == true)
                {
                    this.transform.position = currentPos;
                    yield return new WaitForSeconds(.2f);
                    yield return Move();
                }
                else
                {
                    if (hit.collider.GetComponent<BlockMvmt>().MovingDone == 1)
                    {
                        MovingDone++;
                        yield break;
                    }
                    else
                    {
                        yield return new WaitForSeconds(.2f);
                        yield return Move();

                    }
                }
            }
            else
            {
                MovingDone++;
                yield break;
            }
        }
        else
        {
            yield break;
        }
    }

    public IEnumerator Move()
    {
       
        if (MovingDone == 0)
        {
            Vector3 tempPos = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1f);
            Down = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1f);
            if (hit)
            {
                if (hit.collider.tag == "Grid")
                {
                    targetPos = hit.collider.transform.position;
                    transform.position = targetPos;

                    if (collided == false)
                    {

                        yield return new WaitForSeconds(.2f);
                        yield return Move();
                    }
                }
                else if (hit.collider.GetComponent<BlockMvmt>().MovingDone == 0)
                {
                    yield return new WaitForSeconds(.2f);
                    yield return Move();
                }

            }
            if (collided == true)
            {
                transform.position = Down.collider.transform.position;
                MovingDone = 1;
                yield break;
            }
            else
            {
                MovingDone = 1;
                yield break;
            }
        }
        else
        {
            yield break;
        }
    }




}
