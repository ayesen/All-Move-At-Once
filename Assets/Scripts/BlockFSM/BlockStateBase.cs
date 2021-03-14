using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockStateBase
{
    public abstract void EnterState(BlockController BC);
    public abstract void Update(BlockController BC);
    public abstract void LeaveState(BlockController BC);
}
