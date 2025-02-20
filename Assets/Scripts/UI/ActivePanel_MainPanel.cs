using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePanel_MainPanel : ActivePanel
{
    public PlayerInput playerInput;
    public PlayerInputHandler playerInputHandler;

    public override void PanelActive(IActivePanel previousPanel)
    {
        this.previousPanel = previousPanel;
        previousPanel.DisablePanel();
        gameObject.SetActive(true);
        UIManager.Instance.activePanel = this;
    }

    public override void PanelInactive()
    {
        UIManager.Instance.activePanel = previousPanel;
        gameObject.SetActive(false);
        if(previousPanel != null) { previousPanel.EnablePanel(); }        
    }
    public override void DisablePanel()
    {
        if (_hasAnimator) animator.Play("Disable_Panel");
    }
    public override void EnablePanel()
    {
        if(_hasAnimator) animator.Play("Enable_Panel");
    }

    public void TimePause()
    {
        Time.timeScale = 0f;
        playerInput.enabled = false;
        playerInputHandler.cursorLocked = false;
        playerInputHandler.cursorInputForLook = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TimeResume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerInput.enabled = true;
        playerInputHandler.cursorInputForLook = true;
    }
}
