using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMouseCheck : MonoBehaviour
{
    private SelectedUnits selectedUnits;

    private void Start()
    {
        selectedUnits = SelectedUnits.instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform.gameObject == gameObject)
                {
                    foreach (SelectUnit unit in selectedUnits.currentSelection)
                    {
                        unit.selectionMarker.gameObject.SetActive(false);
                        unit.isSelected = false;
                    }
                    selectedUnits.currentSelection.Clear();
                }
            }
        }
    }
}
