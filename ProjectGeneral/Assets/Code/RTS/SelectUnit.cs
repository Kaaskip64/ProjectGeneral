using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectUnit : MonoBehaviour, ISelectable
{
    public MeshRenderer selectionMarker;

    [HideInInspector]
    public bool isSelected = false;

    //[HideInInspector]
    public bool isActive = true;

    private SelectedUnits selectedUnits;
    private NavMeshAgent agent;


    private void Start()
    {
        selectedUnits = SelectedUnits.instance;
        selectedUnits.allSelectables.Add(this);
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if(!isActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform.gameObject == gameObject)
                {
                    Select();
                }
            }
        }
        if (!isSelected)
        {
            return;
        }

        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }

    public void Select()
    {
        if(!selectedUnits.currentSelection.Contains(this))
        {
            selectedUnits.currentSelection.Add(this);
            selectionMarker.gameObject.SetActive(true);
            isSelected = true;
        }

    }

    public void SwitchOnOff()
    {
        isActive = !isActive;
    }
}
