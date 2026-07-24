using UnityEngine;
using UnityEngine.InputSystem;

public class InteractWithStuff : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Camera playerCamera;

    [Header("UI")]
    [SerializeField] private GameObject interactPrompt;

    private Renderer lastRenderer;
    private Material[] lastMaterials;
    private Color[] originalEmissionColors;
    private bool[] originalEmissionEnabled;

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (playerCamera == null)
        {
            Debug.LogError("InteractWithStuff: Camera not assigned.");
        }

        HidePrompt();
    }

    private void OnDisable()
    {
        ClearHighlight();
        HidePrompt();
    }

    private void Update()
    {
        if (playerCamera == null)
        {
            return;
        }

        HighlightCheck();

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            PerformInteraction();
        }
    }

    private Ray GetRay()
    {
        return new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );
    }

    private void HighlightCheck()
    {
        Ray ray = GetRay();

        if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                interactDistance,
                interactLayer))
        {
            IInteractable interactable =
                hit.collider.GetComponent<IInteractable>();

            if (interactable == null)
            {
                interactable =
                    hit.collider.GetComponentInParent<IInteractable>();
            }

            if (interactable == null)
            {
                ClearHighlight();
                HidePrompt();
                return;
            }

            Renderer rend = hit.collider.GetComponent<Renderer>();

            if (rend == null)
            {
                rend = hit.collider.GetComponentInParent<Renderer>();
            }

            if (rend != lastRenderer)
            {
                ClearHighlight();

                if (rend != null)
                {
                    ApplyHighlight(rend);
                }
            }

            ShowPrompt();
        }
        else
        {
            ClearHighlight();
            HidePrompt();
        }
    }

    private void PerformInteraction()
    {
        Ray ray = GetRay();

        if (!Physics.Raycast(
                ray,
                out RaycastHit hit,
                interactDistance,
                interactLayer))
        {
            Debug.Log("InteractWithStuff: No interactable object hit.");
            return;
        }

        IInteractable interactable =
            hit.collider.GetComponent<IInteractable>();

        if (interactable == null)
        {
            interactable =
                hit.collider.GetComponentInParent<IInteractable>();
        }

        if (interactable == null)
        {
            Debug.Log(
                $"InteractWithStuff: {hit.collider.name} has no IInteractable."
            );
            return;
        }

        Debug.Log($"Interacting with: {hit.collider.name}");
        interactable.Interact();
    }

    private void ApplyHighlight(Renderer rend)
    {
        lastRenderer = rend;
        lastMaterials = rend.materials;

        originalEmissionColors =
            new Color[lastMaterials.Length];

        originalEmissionEnabled =
            new bool[lastMaterials.Length];

        for (int i = 0; i < lastMaterials.Length; i++)
        {
            Material material = lastMaterials[i];

            originalEmissionEnabled[i] =
                material.IsKeywordEnabled("_EMISSION");

            originalEmissionColors[i] =
                material.HasProperty("_EmissionColor")
                    ? material.GetColor("_EmissionColor")
                    : Color.black;

            if (material.HasProperty("_EmissionColor"))
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor(
                    "_EmissionColor",
                    highlightColor * 2f
                );
            }
        }
    }

    private void ClearHighlight()
    {
        if (lastMaterials != null)
        {
            for (int i = 0; i < lastMaterials.Length; i++)
            {
                Material material = lastMaterials[i];

                if (material == null ||
                    !material.HasProperty("_EmissionColor"))
                {
                    continue;
                }

                material.SetColor(
                    "_EmissionColor",
                    originalEmissionColors[i]
                );

                if (!originalEmissionEnabled[i])
                {
                    material.DisableKeyword("_EMISSION");
                }
            }
        }

        lastRenderer = null;
        lastMaterials = null;
        originalEmissionColors = null;
        originalEmissionEnabled = null;
    }

    private void ShowPrompt()
    {
        if (interactPrompt != null &&
            !interactPrompt.activeSelf)
        {
            interactPrompt.SetActive(true);
        }
    }

    private void HidePrompt()
    {
        if (interactPrompt != null &&
            interactPrompt.activeSelf)
        {
            interactPrompt.SetActive(false);
        }
    }
}