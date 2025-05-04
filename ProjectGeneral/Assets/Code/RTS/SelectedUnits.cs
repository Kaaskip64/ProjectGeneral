using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public static SelectedUnits instance;

    public List<SelectUnit> currentSelection;
    private void Awake()
    {
        instance = this;
    }
}
