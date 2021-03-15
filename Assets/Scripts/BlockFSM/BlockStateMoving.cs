using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateMoving : BlockStateBase
{
    
    public override void EnterState(BlockController BC)
    {
        BC.ChangeSprite();                    //change all sprite to non select sprite
        BC.LR.ColliderSwitch();               //enable all colliders so that the blocks can move
        for (int i = 0; i < BC.blocks.Count; i++)
        {
            BC.blocks[i].GetComponent<BlockMvmt>().StartCoroutine(BC.blocks[i].GetComponent<BlockMvmt>().Move());  //call the move function
        }

    }

    public override void Update(BlockController BC)
    {
        int ChangeToSelect = 0;                            //if all three blocks stop moving corountine then go on the select state
        for (int i = 0; i < BC.blocks.Count; i++)
        {
            ChangeToSelect += BC.blocks[i].GetComponent<BlockMvmt>().MovingDone;
            Debug.Log(ChangeToSelect);
        }
        if (ChangeToSelect == 3)
        {
            for (int i = 0; i < BC.blocks.Count; i++)
            {
                BC.blocks[i].GetComponent<BlockMvmt>().MovingDone = 0;
            }
            BC.ChangeState(BC.SelectState);
        }
    }

    public override void LeaveState(BlockController BC)
    {
        for (int i = 0; i < BC.blocks.Count; i++)  //count how many blocks are on destination
        {
            BC.blocks[i].GetComponent<BlockMvmt>().checkDest();
            if (BC.blocks[i].GetComponent<BlockMvmt>().onDest)
            {
                BC.blocks[i].GetComponent<BlockMvmt>().MovingDone = 1;
            }
            else
            {
                BC.blocks[i].GetComponent<BlockMvmt>().MovingDone = 0;
            }
        }
        BC.LR.ColliderSwitch();    //turn grid's collider off so that the blocks won't move in select state

    }
}
