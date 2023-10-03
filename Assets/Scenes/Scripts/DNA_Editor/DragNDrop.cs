using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static string DADDY = "ASSETS";      //parent name of items before moving to grid

    Image im;
    [HideInInspector] public Transform parentDrag, ppap;

    
    private void Start()
    {
        parentDrag = null;
        im = gameObject.GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ppap = transform.parent;            //for when the slot already has an item
        if (transform.parent.parent.transform.name == DADDY)
            SpawnShit();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        im.raycastTarget = false;           //ignore self

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        parentDrag = null;
    }
   
    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentDrag != null)
        {
            transform.SetParent(parentDrag);
            im.raycastTarget = true;
        }
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (!im.raycastTarget)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                transform.localRotation = Quaternion.Euler(0, 0, transform.localEulerAngles.z + 180);
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                transform.localRotation = Quaternion.Euler(0, 0, transform.localEulerAngles.z - 180);
            }
            else if(Input.GetKeyUp(KeyCode.Escape))
            {
                Destroy(gameObject);
            }
        }

    }
    public void SpawnShit()
    {
        GameObject t = Instantiate(gameObject, transform.parent);
        t.transform.SetAsFirstSibling();
        t.name = transform.name;

    }

}
