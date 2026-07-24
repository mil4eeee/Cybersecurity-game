// using UnityEngine;

// public class PlayerInteract : MonoBehaviour
// {
//     [Header("Interaction")]
//     public float interactDistance = 3f;
//     public LayerMask interactLayer;

//     private Camera cam;
//     private InputManager input;

//     private void Start()
//     {
//         cam = Camera.main;
//         input = GetComponent<InputManager>();

//         if (cam == null)
//         {
//             Debug.LogError("PlayerInteract: Main Camera not found.");
//         }

//         if (input == null)
//         {
//             Debug.LogError("PlayerInteract: InputManager component not found on player.");
//         }
//     }

//     private void Update()
//     {
//         if (input == null || cam == null) return;

//         if (input.InteractPressedThisFrame)
//         {
//             TryInteract();
//         }
//     }

//     private void TryInteract()
//     {
//         Ray ray = new Ray(cam.transform.position, cam.transform.forward);

//         Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 1f);

//         if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
//         {
//             IInteractable interactable = hit.collider.GetComponent<IInteractable>();

//             if (interactable != null)
//             {
//                 interactable.Interact();
//             }
//             else
//             {
//                 Debug.Log("Hit object is on interact layer, but has no IInteractable: " + hit.collider.name);
//             }
//         }
//     }
// }
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
}