using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickUIOpen : MonoBehaviour,IButton
{
    public PanelType type;

    public void OnButtonClick()
    {
        GUIPanelController.Instance.ChangeState(type);
    }
}
