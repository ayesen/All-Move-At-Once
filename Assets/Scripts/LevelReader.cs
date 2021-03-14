using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

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
    private float margin;
    [SerializeField]
    private GameObject blockControl;

    public int rowCount = 0;
    private int colCount = 0;

    void Awake()
    {
        //GenLevel(0);

    }

    private void Update()
    {
        Test();
    }

    public void GenLevel(int levelNum)
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
                    }
                }
                rowCount++;
                position = new Vector2(position.x + margin, position.y);
            }
            colCount++;
            position = new Vector2(startPosition.x, position.y - margin);
        }
        float blockCount = rowCount / colCount;
        GameObject BC = Instantiate(blockControl, new Vector2(0, 0), Quaternion.identity);
        BC.transform.SetParent(this.transform);
        //set the position of the grid to the middle of the screen
        this.transform.position = new Vector2(-1 * ((margin * 2) * ((blockCount - 1) / 2)), (margin * 2) * ((blockCount - 1) / 2));

    }

    public void ClearLevel()
    {
        position = new Vector2(0, 0);
        this.transform.position = position;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        rowCount = 0;
        colCount = 0;
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenLevel(0);
        }
        if (Input.GetKey(KeyCode.T))
        {
            ClearLevel();
        }
    }

}
