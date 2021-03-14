using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateMoving : BlockStateBase
{
    public override void EnterState(BlockController BC)
    {
        BC.ChangeSprite();
    }

    public override void Update(BlockController BC)
    {

    }

    public override void LeaveState(BlockController BC)
    {

    }
}
