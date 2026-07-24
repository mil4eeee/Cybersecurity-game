using UnityEngine;
using UnityEngine.InputSystem;

public class InteractWithStuff : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public Color highlightColor = Color.yellow;
    public Camera playerCamera;

    [Header("UI")]
    public GameObject interactPrompt;

    private Renderer lastRenderer;
    private Material[] lastMaterials;
    private Color[] originalEmissionColors;
    private bool[] originalEmissionEnabled;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (playerCamera == null)
            Debug.LogError("Camera not assigned.");

        HidePrompt();
    }

    private void Update()
    {
        if (playerCamera == null) return;

        if (MonitorScript.IsComputerOpen)
        {
            ClearHighlight();
            HidePrompt();

           // if (Keyboard.current.eKey.wasPressedThisFrame)
            //    MonitorScript.CurrentOpenMonitor?.Interact();

            return;
        }

        HighlightCheck();

        if (Keyboard.current.eKey.wasPressedThisFrame)
            PerformInteraction();
    }

    private Ray GetRay()
    {
        return new Ray(playerCamera.transform.position, playerCamera.transform.forward);
    }

    private void HighlightCheck()
    {
        Ray ray = GetRay();

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();

            if (rend == null)
            {
                ClearHighlight();
                HidePrompt();
                return;
            }

            if (rend != lastRenderer)
            {
                ClearHighlight();
                ApplyHighlight(rend);
            }

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable == null)
                interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
                ShowPrompt();
            else
                HidePrompt();
        }
        else
        {
            ClearHighlight();
            HidePrompt();
        }
    }

    private void PerformInteraction()
    {
        Debug.Log("PerformInteraction called!"); // додај ова
        Ray ray = GetRay();

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Debug.Log("Hit object: " + hit.collider.name);

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable == null)
                interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                Debug.Log("INTERACT FOUND");
                interactable.Interact();
            }
            else
            {
                Debug.Log("NO INTERACTABLE");
            }
        }
        else
        {
            Debug.Log("NO HIT");
        }
    }

    private void ApplyHighlight(Renderer rend)
    {
        lastRenderer = rend;
        lastMaterials = rend.materials;

        originalEmissionColors = new Color[lastMaterials.Length];
        originalEmissionEnabled = new bool[lastMaterials.Length];

        for (int i = 0; i < lastMaterials.Length; i++)
        {
            originalEmissionEnabled[i] = lastMaterials[i].IsKeywordEnabled("_EMISSION");
            originalEmissionColors[i] = lastMaterials[i].GetColor("_EmissionColor");

            lastMaterials[i].EnableKeyword("_EMISSION");
            lastMaterials[i].SetColor("_EmissionColor", highlightColor * 2f);
        }
    }

    private void ClearHighlight()
    {
        if (lastMaterials != null)
        {
            for (int i = 0; i < lastMaterials.Length; i++)
            {
                lastMaterials[i].SetColor("_EmissionColor", originalEmissionColors[i]);

                if (!originalEmissionEnabled[i])
                    lastMaterials[i].DisableKeyword("_EMISSION");
            }
        }

        lastRenderer = null;
        lastMaterials = null;
        originalEmissionColors = null;
        originalEmissionEnabled = null;
    }

    private void ShowPrompt()
    {
        if (interactPrompt != null && !interactPrompt.activeSelf)
            interactPrompt.SetActive(true);
    }

    private void HidePrompt()
    {
        if (interactPrompt != null && interactPrompt.activeSelf)
            interactPrompt.SetActive(false);
    }
}