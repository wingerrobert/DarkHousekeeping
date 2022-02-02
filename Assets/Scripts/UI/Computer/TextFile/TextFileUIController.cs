using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFileUIController : MonoBehaviour
{
    public static TextFileData currentTextFile; 

    public Text fileNameElement;
    public InputField fileContentElement;

    public void Initialize(TextFileData data)
    {
        currentTextFile = data;
        fileNameElement.text = $"{data.fileName} - Notes";
        fileContentElement.text = System.Text.RegularExpressions.Regex.Unescape(data.fileContent);
    }

    public void UpdateTextData()
    {
        currentTextFile.fileContent = fileContentElement.text;
    }
}
