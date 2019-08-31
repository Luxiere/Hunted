using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using TMPro;

public class ChangeText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private string currentText;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        currentText = text.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = "- " + currentText + " -";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = currentText;
    }
}
