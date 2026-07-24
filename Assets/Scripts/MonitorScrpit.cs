using UnityEngine;

public class MonitorScript : MonoBehaviour, IInteractable
{
    [SerializeField] private ComputerUIManager computerUIManager;

    public void Interact()
    {
        if (computerUIManager == null)
        {
            Debug.LogError("MonitorScript: ComputerUIManager is not assigned.");
            return;
        }

        computerUIManager.OpenComputer();
    }
}