using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ComputerUIController : MonoBehaviour
{
    public bool isShowing = false;

    public GameObject postProcessingController;
    public GameObject startMenu;

    public GameObject[] uiElements;

    DepthOfField _depthOfField;
    bool _startMenuShowing = false;
    TextFileUIController _textFileUIController;

    // Start is called before the first frame update
    void Start()
    {
        _depthOfField = (DepthOfField)postProcessingController.GetComponent<Volume>().profile.components.Find(c => c.GetType() == typeof(DepthOfField));
        _depthOfField.active = isShowing;

        DisableUiElements();

        startMenu.SetActive(_startMenuShowing);

        _textFileUIController = uiElements[(int)GlobalValues.ComputerUIScreen.TextFile].GetComponent<TextFileUIController>();
    }

    public void SetComputerUIVisibility(bool visible)
    {
        isShowing = visible;
        Time.timeScale = visible ? 0 : 1;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;

        uiElements[(int)GlobalValues.ComputerUIScreen.Computer].SetActive(visible);
        _depthOfField.active = visible;
    }

    public void SetStoreUIVisibility(bool visible)
    {
        uiElements[(int)GlobalValues.ComputerUIScreen.Store].SetActive(visible);
    }

    public void InitializeTextFileUI(TextFileData data)
    {
        _textFileUIController.Initialize(data);
    }

    public void SetTextFileUIVisibility(bool visible)
    {
        uiElements[(int)GlobalValues.ComputerUIScreen.TextFile].SetActive(visible);
    }

    public void ToggleStartMenu()
    {
        startMenu.SetActive(_startMenuShowing = !_startMenuShowing);
    }

    void DisableUiElements() 
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }
    }
}
