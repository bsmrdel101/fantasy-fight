using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    
    
    private void Update()
    {
        HandleMouseInputs();
    }

    private void HandleMouseInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector2 rayOrigin = ray.origin;
            int layerMask = 1 << LayerMask.NameToLayer("Tile");
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, ray.direction, Mathf.Infinity, layerMask);
            if (!hit.collider) return;
            CharacterMovement.OnClickTileAction(hit);
        }
    }
}
