using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

// 'Tab' keyboard input behavior to change from 1 form to another
public class ChangeInputForm : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if(previous != null)
            {
                previous.Select();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke();
        }
    }
}
