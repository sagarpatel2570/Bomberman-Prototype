using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public interface IBack
{
    void GoBack();
}

/// <summary>
/// This class uses state machine to handle the ui transisition flow 
/// </summary>
public class GUIPanelController : MonoBehaviour
{
    public static GUIPanelController Instance;
    public static PanelType initialPanel = PanelType.HOMEPANEL;

    /// <summary>
    /// when state change is just started from current state to target state
    /// </summary>
    public static event Action<PanelType, PanelType> OnStateChangeEvent;
    /// <summary>
    /// when state change has completed from previous state to current state
    /// </summary>
    public static event Action<PanelType, PanelType> OnStateChangedEvent;

    public List<GUIPanel> guiPanels;

    Stack<PanelType> panelStack = new Stack<PanelType>();
    PanelType previousType = PanelType.NONE;
    PanelType currentType = PanelType.NONE;

    bool isChangingState;

    private void Awake()
    {
        Instance = this;
        ChangeState(initialPanel);
        initialPanel = PanelType.HOMEPANEL;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    public void ChangeState(PanelType targetType,bool addToStack = true)
    {
        if(isChangingState)
        {
            return;
        }

        if(targetType == currentType)
        {
            return;
        }

        if(addToStack)
        {
            panelStack.Push(currentType);
        }

        if(targetType == PanelType.HOMEPANEL)
        {
            panelStack.Clear();
        }

        GUIPanel currentPanel = GetPanel(currentType);
        GUIPanel targetPanel = GetPanel(targetType);
        if (currentPanel != null  && targetPanel != null && !targetPanel.isPopup || (currentPanel != null && currentPanel.isPopup))
        {
            float waitTime = currentPanel.Hide();
            if (waitTime != 0)
            {
                isChangingState = true;
                StartCoroutine(WaitForCurrentStateToHide(waitTime));
            }
        }
        OnStateChangeEvent?.Invoke(currentType, targetType);
        previousType = currentType;
        currentType = targetType;
        if(!isChangingState )
        {
            OnCurrentStateHidden();
        }
    }

    public void GoBack(bool addToStack = false)
    {
        if(isChangingState)
        {
            return;
        }

        if(currentType == PanelType.HOMEPANEL)
        {
            Application.Quit();
            panelStack.Clear();
            return;
        }

        GUIPanel currentPanel = GetPanel(currentType);
        if(currentPanel != null)
        {
            IBack back = currentPanel.GetComponent<IBack>();
            if(back != null)
            {
                back.GoBack();
                return;
            }
        }

        if (panelStack.Count > 0)
        {
            PanelType type = panelStack.Pop();

            ChangeState(type, addToStack);
        }
        else
        {
            Application.Quit();
        }
    }

    IEnumerator WaitForCurrentStateToHide(float time)
    {
        yield return new WaitForSeconds(time);
        OnCurrentStateHidden();
    }

    void OnCurrentStateHidden()
    {
        GUIPanel previousPanel = GetPanel(previousType);
        GUIPanel currentPanel = GetPanel(currentType);

        if (previousPanel != null && currentPanel != null && !currentPanel.isPopup || (previousPanel != null && previousPanel.isPopup))
        {
            previousPanel.OnHidden();
        }
        
        float waitTime = currentPanel.Show();
        if (waitTime != 0)
        {
            isChangingState = true;
            StartCoroutine(WaitForCurrentStateToShow(waitTime));
        }
        else
        {
            isChangingState = false;
        }

        if (!isChangingState)
        {
            OnCurrentStateShowned();
        }
    }

    IEnumerator WaitForCurrentStateToShow(float time)
    {
        yield return new WaitForSeconds(time);
        OnCurrentStateShowned();
    }

    void OnCurrentStateShowned()
    {
        isChangingState = false;
        GUIPanel currentPanel = GetPanel(currentType);
        currentPanel.OnShown();
        OnStateChangedEvent?.Invoke(previousType, currentType);
    }

    GUIPanel GetPanel (PanelType type)
    {
        return guiPanels.Find(a => a.type == type);
    }

}


public enum PanelType
{
    NONE = 0,
    HOMEPANEL = 1,
    GAMEPLAYPANEL = 2,
    GAMEOVERPANEL = 3,
    GAMEWINPANEL = 4,
}