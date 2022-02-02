using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Computer/Files/TextFileData")]
public class TextFileData : ScriptableObject
{
    public string fileName;
    public string fileContent;
}
