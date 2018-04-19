using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionController : MonoBehaviour {

    List<GameObject> selectedUnits;
    GameObject selectedUnit;
    
	void Start () {
		
	}

    private void Awake()
    {
        selectedUnits = new List<GameObject>();
    }

    void Update () {
        CheckMouse();
        DebugPrintList(selectedUnits);
	}

    void SelectUnit(GameObject unit)
    {
        if(selectedUnits == null)
        {
            Debug.Log("List is null!");
            return;
        }
        if (unit == null)
        {
            Debug.Log("Unit is NULL");
            ClearSelection();
            return;
        }
        if(selectedUnits != null)
        {
            if(!selectedUnits.Contains(unit))
            {
                selectedUnits.Add(unit);
                return;
            }
        }
        selectedUnits.Add(unit);
    }

    void ClearSelection()
    {
        selectedUnits.Clear();
    }

    void ClearSelection(GameObject unit)
    {
        selectedUnits.Remove(unit);
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button pressed");
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                GameObject hitObject = hitInfo.transform.root.gameObject;
                Debug.Log(hitObject.name);
                if(hitObject.GetComponent<Soldier_Base>() != null)
                {
                    Debug.Log(Input.GetButton("Add to selection"));
                    if (!Input.GetButton("Add to selection"))
                    {
                        Debug.Log("Selected a soldier");
                        ClearSelection();
                        SelectUnit(hitObject);
                    }else if (Input.GetButton("Add to selection") && selectedUnits.Contains(hitObject))
                    {
                        Debug.Log("Removed from selection");
                        ClearSelection(hitObject);
                    }
                    else if(Input.GetButton("Add to selection"))
                    {
                        Debug.Log("Added to selection");
                        SelectUnit(hitObject);
                    }
                    else
                    {
                        Debug.Log("Nothing happened");
                    }
                }
                else
                {
                    Debug.Log("No soldier detected");
                    ClearSelection();
                }
            }
        }
    }

    void DebugPrintList(List<GameObject> list)
    {
        if(Input.GetButtonDown("Keyboard d"))
        {
            foreach (GameObject soldier in list)
                {
                    Debug.Log(soldier.name);
                }
        }
    }
}
