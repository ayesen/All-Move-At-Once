using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public List<GameObject> blocks = new List<GameObject>();
    public LevelReader LR;
    [SerializeField]
    private Sprite select;
    public Sprite Select
    {
        get
        {
            return select;
        }
        set
        {
            select = value;
        }
    }

    [SerializeField]
    private Sprite notSelect;
    public Sprite NotSelect
    {
        get
        {
            return notSelect;
        }
        set
        {
            notSelect = value;
        }
    }

    //State Machine
    private BlockStateBase currentState;
    public BlockStateBase CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }
    private BlockStateSelect selectState = new BlockStateSelect();
    public BlockStateSelect SelectState
    {
        get
        {
            return selectState;
        }
        set
        {
            selectState = value;
        }
    }
    private BlockStateMoving movingState = new BlockStateMoving();
    public BlockStateMoving MovingState
    {
        get
        {
            return movingState;
        }
        set
        {
            movingState = value;
        }
    }

    public void ChangeState(BlockStateBase newState)
    {
        if (currentState != null)
        {
            currentState.LeaveState(this);
        }

        currentState = newState;
        Debug.Log(currentState);

        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    void Awake()
    {
        LR = GameObject.FindGameObjectWithTag("LE").GetComponent<LevelReader>();
        //find the 3 color blocks
        blocks.Add(GameObject.FindGameObjectWithTag("Blue"));
        blocks.Add(GameObject.FindGameObjectWithTag("Yellow"));
        blocks.Add(GameObject.FindGameObjectWithTag("Red"));
    }
    void Start()
    {
        ChangeState(selectState);
    }

    void Update()
    {
        currentState.Update(this);
    }


    

    //when the block is pressed, highlight the block and un highlight other blocks
    public void ChangeSprite()
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
