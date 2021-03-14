using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit: this script is based on Comp-3 Interactive's Level Generator script
//https://www.youtube.com/watch?v=-6ww4MMi7C0

//This class is used to decode the text file and read which symbol creates which prefab
[System.Serializable]
public class Decoding
{
    public char character;
    public GameObject generatedPrefab;

}
