using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class eVENT : MonoBehaviour
{
   [Header("Callbacks")]
   public UnityEvent onClick;

   private void Update()
   {
      
      
         // Check if the left mouse button was clicked
         if (Input.GetMouseButtonDown(0))
         {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // Check if the ray hits this game object
            if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>())
            {
               OnClick();
            }
         }
      
   }

   private void OnClick()
   {
      onClick.Invoke();
      // Set this to false when you want to switch back to 3rd Person
     // GameManager.Instance.playerRef.UpdateOrientation();
   }

}
