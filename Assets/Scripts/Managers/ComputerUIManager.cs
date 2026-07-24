using UnityEngine;

public class ComputerUIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject computerUI;

    [Header("Player scripts")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InteractWithStuff playerInteract;

    public bool IsComputerOpen { get; private set; }

    private void Start()
    {
        if (computerUI != null)
        {
            computerUI.SetActive(false);
        }

        IsComputerOpen = false;
    }

    public void OpenComputer()
    {
        Debug.Log("ComputerUIManager: Opening computer.");

        if (computerUI == null)
        {
            Debug.LogError("ComputerUIManager: ComputerUI is not assigned.");
            return;
        }

        computerUI.SetActive(true);
        IsComputerOpen = true;

        if (playerInteract != null)
        {
            playerInteract.enabled = false;
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseComputer()
    {
        if (computerUI != null)
        {
            computerUI.SetActive(false);
        }

        IsComputerOpen = false;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        if (playerInteract != null)
        {
            playerInteract.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}