using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

//Credit: some of the script is based on Comp-3 Interactive's Level Generator script
//https://www.youtube.com/watch?v=-6ww4MMi7C0
public class LevelReader : MonoBehaviour
{
    [SerializeField]
    private Decoding[] decodeData; //for each decode data, there will be a pair of symbol related to the prefab
    [SerializeField]
    private TextAsset[] textLevels;//there will be multiple levels
    [SerializeField]
    private Vector2 position = new Vector2(0,0);//the position where the level will be spawn
    [SerializeField]
    private GameObject blockControl;

    public static float margin = 0.6f;

    private float blockCount;
    private int rowCount = 0;
    private int colCount = 0;

    public Text Instruction;
    private Color textColor;

    public List<GameObject> GridBase = new List<GameObject>();

    public int CurrentLevel = 0;

    public bool nextLvl = false;

    void Awake()
    {
        GenLevel(0); //start at level_00
        textColor = Instruction.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Restart()); //if R is pressed then restart this level
        }
        if (nextLvl == true)
        {

            if (CurrentLevel < textLevels.Length - 1)
            {
                Instruction.text = "Press N to Next Level After Solving Current Level";
                Instruction.color = Color.yellow;
                if (Input.GetKeyDown(KeyCode.N)) //if all three blocks are on destination then next level is ready, player can press n to start next level
                {
                    CurrentLevel++;
                    StartCoroutine(Restart());
                }
            }
            else
            {
                Instruction.color = Color.yellow;
                Instruction.text = "Nice! All Levels Solved!";
            }
        }
        else
        {
            Instruction.color = textColor;
            Instruction.text = "Press N to Next Level After Solving Current Level";
        }
    }

    public void GenLevel(int levelNum)  //function used to generate level
    {
        
        Vector2 startPosition = position;
        //split the text row by row and read the letter
        string[] rows = Regex.Split(textLevels[levelNum].text, "\r\n|\r|\n");
        foreach (string row in rows)
        {
            foreach (char c in row)
            {
                foreach(Decoding data in decodeData)
                {
                    if(c == data.character)
                    {
                        GameObject gridBlock =  Instantiate(data.generatedPrefab, position, Quaternion.identity); 
                        gridBlock.transform.SetParent(this.transform);
                        if(gridBlock.tag == "Grid")
                        {
                            GridBase.Add(gridBlock); //if the object generated is a grid object then put it into the grid list
                        }
                        
                    }
                }
                rowCount++;
                position = new Vector2(position.x + margin, position.y);
            }
            colCount++;
            position = new Vector2(startPosition.x, position.y - margin);
        }
        blockCount = rowCount / colCount;
        GameObject BC = Instantiate(blockControl, new Vector2(0, 0), Quaternion.identity);  //instantiate new block controller for different levels
        BC.transform.SetParent(this.transform);
        //set the position of the grid to the middle of the screen
        this.transform.position = new Vector2(-1 * ((margin * 2) * ((blockCount - 1) / 2)), (margin * 2) * ((blockCount - 1) / 2));


    }


    public void ClearLevel() //remove all items in current level
    {
        GridBase.Clear();
        position = new Vector2(0, 0);
        this.transform.position = position;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        rowCount = 0;
        colCount = 0;
    }

    public void ColliderSwitch() //turn gird object's collider on/off
    {
        foreach (GameObject gridBlock in GridBase)
        {
            gridBlock.GetComponent<Collider2D>().enabled = !gridBlock.GetComponent<Collider2D>().enabled;
        }
    }


    public IEnumerator Restart() //use this function to restart current level or to generate next level
    {
        nextLvl = false;
        ClearLevel();
        yield return new WaitForSeconds(0.2f);
        GenLevel(CurrentLevel);
        yield break;
    }

}
