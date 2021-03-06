using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateSelect : BlockStateBase
{
    public override void EnterState(BlockController BC)
    {
        BC.nextLvl = true;//set next level to true first

    }

    public override void Update(BlockController BC)
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BC.ChangeSprite();
        }
        //the part of getting key value is based on: https://answers.unity.com/questions/165878/how-to-detect-which-key-is-pressed.html

        for (int i = 0; i < BC.blocks.Count; i++)
        {
            if (!BC.blocks[i].GetComponent<BlockMvmt>().onDest)                              //if the block has not reach its destination
            {
                BC.nextLvl = false; //if any of the block is not on destination then set nextlvl back to false
                BC.blocks[i].GetComponent<BlockMvmt>().MovingDone = 0;
                if (BC.blocks[i].GetComponent<SpriteRenderer>().sprite == BC.Select)         //get the block which is selected
                {
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))          //find out which key is pressed and record the value
                    {
                        if (Input.GetKey(key))
                        {
                            BC.blocks[i].GetComponent<BlockMvmt>().Rotate(key);              //pass the key value to the block's roatate function and let it rotate
                        }
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            BC.ChangeState(BC.MovingState);
        }
        BC.LR.nextLvl = BC.nextLvl;
    }

    public override void LeaveState(BlockController BC)
    {

    }

}
