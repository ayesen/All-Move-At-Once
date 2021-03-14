using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public List<GameObject> blocks = new List<GameObject>();

    [SerializeField]
    private Sprite select;
    [SerializeField]
    private Sprite notSelect;

    // Start is called before the first frame update
    void Start()
    {
        blocks.Add(GameObject.FindGameObjectWithTag("Blue"));
        blocks.Add(GameObject.FindGameObjectWithTag("Yellow"));
        blocks.Add(GameObject.FindGameObjectWithTag("Red"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if(blocks[i].GetComponent<BlockMvmt>().isPressed)
            {
                blocks[i].GetComponent<SpriteRenderer>().sprite = select;
            }
            else if(blocks[i].GetComponent<BlockMvmt>().isPressed == false)
            {
                blocks[i].GetComponent<SpriteRenderer>().sprite = notSelect;
            }
        }
    }

}
