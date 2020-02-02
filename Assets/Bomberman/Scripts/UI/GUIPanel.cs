using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPanel : MonoBehaviour
{
    public PanelType type;
    public Animator animator;
    public Button backButton;
    public bool isPopup;

    private void OnEnable()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
    }

    private void OnDisable()
    {
        if (backButton != null)
        {
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }

    void OnBackButtonClicked ()
    {
        GUIPanelController.Instance.GoBack();
    }

    public float Show()
    {
        if (gameObject.activeSelf)
        {
            return 0;
        }
        gameObject.SetActive(true);
        if (animator != null)
        {
            animator.SetTrigger("In");
        }

        return GetAnimationLength("In");
    }

    public void OnShown()
    {
    }

    float GetAnimationLength (string clipName)
    {
        if (animator == null)
        {
            return 0;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0;
    }

    public float Hide()
    {
        if (animator != null)
        {
            animator.SetTrigger("Out");
        }
        return GetAnimationLength("Out");
    }

    public void OnHidden()
    {
        gameObject.SetActive(false);
    }
}
