using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    void Update()
    {
        // PC için sol tıklama
        if (Input.GetMouseButtonDown(0))
        {
            DetectObject(Input.mousePosition);
        }

        // Mobil için dokunma
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DetectObject(Input.GetTouch(0).position);
        }
    }

    void DetectObject(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Ballon")
            {
                Debug.Log(" Ballon: " + hit.collider.gameObject.name);
                BallonController ballonController = hit.collider.gameObject.GetComponent<BallonController>();
                if (ballonController != null)
                {
                    ballonController.Pop();
                }
            }
        }
    }
}
