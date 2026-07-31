using UnityEngine;

public class ComputerUIManager : MonoBehaviour
{
    [Header("Main UI")]
    [SerializeField] private GameObject computerUI;

    [Header("Workspace Panels")]
    [SerializeField] private GameObject learnPanel;
    [SerializeField] private GameObject practicePanel;
    [SerializeField] private GameObject askAIPanel;
    [SerializeField] private GameObject progressPanel;

    [Header("Player Scripts")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InteractWithStuff playerInteract;

    [Header("Managers")]
    [SerializeField] private PracticeManager practiceManager;

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
    if (computerUI == null)
    {
        Debug.LogError("ComputerUIManager: ComputerUI is not assigned.");
        return;
    }

    computerUI.SetActive(true);
    IsComputerOpen = true;

    ShowLearnPanel();

    if (practiceManager != null)
    {
        practiceManager.ResetPractice();
    }

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
    if (practiceManager != null)
    {
        practiceManager.ResetPractice();
    }

    ShowLearnPanel();

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

    public void ShowLearnPanel()
    {
        ShowOnly(learnPanel);
    }

    public void ShowPracticePanel()
    {
        ShowOnly(practicePanel);

        if (practiceManager != null)
        {
            practiceManager.LoadPractice();
        }
    }

    public void ShowAskAIPanel()
    {
        ShowOnly(askAIPanel);
    }

    public void ShowProgressPanel()
    {
        ShowOnly(progressPanel);
    }

    private void ShowOnly(GameObject panelToShow)
    {
        if (learnPanel != null)
        {
            learnPanel.SetActive(panelToShow == learnPanel);
        }

        if (practicePanel != null)
        {
            practicePanel.SetActive(panelToShow == practicePanel);
        }

        if (askAIPanel != null)
        {
            askAIPanel.SetActive(panelToShow == askAIPanel);
        }

        if (progressPanel != null)
        {
            progressPanel.SetActive(panelToShow == progressPanel);
        }
    }
}