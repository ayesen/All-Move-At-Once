using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateMoving : BlockStateBase
{
    
    public override void EnterState(BlockController BC)
    {
        BC.ChangeSprite();
        BC.LR.ColliderSwitch();
        for (int i = 0; i < BC.blocks.Count; i++)
        {
            BC.blocks[i].GetComponent<BlockMvmt>().StartCoroutine(BC.blocks[i].GetComponent<BlockMvmt>().MoveCo());
        }

    }

    public override void Update(BlockController BC)
    {
        int ChangeToSelect = 0;
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
        BC.LR.ColliderSwitch();
    }
}
