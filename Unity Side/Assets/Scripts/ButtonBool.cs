using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBool : MonoBehaviour
{
    public Button button { get; private set; }

    public static bool AnyButtonClicked;
    public bool ButtonIsClicked { get; set; } = false;

    public void SetButtonClicked()
    {
        if (button == null)
            button = GetComponent<Button>();
        ButtonIsClicked = true;
        if (!AnyButtonClicked)
            AnyButtonClicked = true;
    }

    private void LateUpdate()
    {
        ButtonIsClicked = false;
        if (AnyButtonClicked)
            AnyButtonClicked = false;
    }
}
