using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public interface IButton
{
    void OnButtonClick();
}

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    public Button button;

    private void OnEnable()
    {
        button.interactable = true;
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick ()
    {
        button.interactable = false;
        transform.DOPunchScale(Vector3.one * 0.1f, 0.1f).OnComplete(()=> { button.interactable = true; });
        IButton[] b = GetComponents<IButton>();
        foreach (var item in b)
        {
            item.OnButtonClick();
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}
