using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateMoving : BlockStateBase
{
    public override void EnterState(BlockController BC)
    {
        BC.ChangeSprite();
        BC.LR.ColliderSwitch();

    }

    public override void Update(BlockController BC)
    {
        for (int i = 0; i < BC.blocks.Count; i++)
        {
            BC.blocks[i].GetComponent<BlockMvmt>().Move();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BC.ChangeState(BC.SelectState);
        }
    }

    public override void LeaveState(BlockController BC)
    {
        BC.LR.ColliderSwitch();
    }
}
