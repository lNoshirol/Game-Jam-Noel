using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CopyText : MonoBehaviour
{
    public void OnClick()
    {
        GUIUtility.systemCopyBuffer = GetComponent<TextMeshProUGUI>().text;
    }
}
