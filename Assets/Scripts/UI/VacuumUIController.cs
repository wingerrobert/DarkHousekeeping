using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VacuumUIController : MonoBehaviour
{
    public GameObject[] reservoirUIElements;
    public GameObject vacuumMask;
    public GameObject vacuumInner;
    public Text vacuumFillText;

    Color initialInnerColor;
    Image innerImage;

    Vector3 initialMaskPos;
    Vector3 initialInnerPos;

    VacuumController vacuum;
    GameObject player;
    PlayerInventory playerInventory;

    float imageHeight;

    private void Start()
    {
        initialMaskPos = vacuumMask.transform.localPosition;
        initialInnerPos = vacuumInner.transform.localPosition;

        innerImage = vacuumInner.GetComponent<Image>();
        initialInnerColor = innerImage.color;

        imageHeight = vacuumMask.GetComponent<Image>().preferredHeight;

        player = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.Player]);
    }

    // Update is called once per frame
    void Update()
    {
        if (vacuum == null)
        {
            vacuum = player.GetComponentInChildren<VacuumController>();

            if (vacuum != null)
            { 
                vacuum.reservoirUIElements = reservoirUIElements;
            }
        }
        else 
        {
            UpdateVacuumIcon();
            UpdateVacuumText();
        }
    }

    void UpdateVacuumIcon()
    {
        innerImage.color = Color.Lerp(initialInnerColor, Color.red, vacuum.vacuumFill);

        vacuumMask.transform.localPosition = new Vector3(initialMaskPos.x, initialMaskPos.y + (vacuum.vacuumFill * imageHeight), initialMaskPos.z);
        vacuumInner.transform.localPosition = new Vector3(initialInnerPos.x, initialInnerPos.y - (vacuum.vacuumFill * imageHeight), initialInnerPos.z);
    }

    void UpdateVacuumText()
    {
        vacuumFillText.text = Mathf.FloorToInt(vacuum.vacuumFill * 100).ToString();
    }
}
