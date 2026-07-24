using UnityEngine;

public class MonitorScript : MonoBehaviour, IInteractable
{
    [Header("Computer UI")]
    public GameObject computerCanvas;
    public GameObject desktopPanel;

    public static bool IsComputerOpen { get; private set; }
    public static MonitorScript CurrentOpenMonitor { get; private set; }

    private bool isUsingComputer = false;

    private void Start()
    {
        CloseComputerInstant();
    }

    public void Interact()
    {
        if (isUsingComputer)
            CloseComputer();
        else
            OpenComputer();
    }

    private void OpenComputer()
    {
        isUsingComputer = true;
        IsComputerOpen = true;
        CurrentOpenMonitor = this;

        if (computerCanvas != null)
            computerCanvas.SetActive(true);
        else
            Debug.LogError("Computer Canvas is not assigned!");

        if (desktopPanel != null)
            desktopPanel.SetActive(true);
        else
            Debug.LogError("Desktop Panel is not assigned!");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Computer opened");
    }

    private void CloseComputer()
    {
        isUsingComputer = false;
        IsComputerOpen = false;

        if (CurrentOpenMonitor == this)
            CurrentOpenMonitor = null;

        CloseComputerInstant();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Computer closed");
    }

    private void CloseComputerInstant()
    {
        if (desktopPanel != null)
            desktopPanel.SetActive(false);

        if (computerCanvas != null)
            computerCanvas.SetActive(false);
    }
}