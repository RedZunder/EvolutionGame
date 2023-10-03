using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSlot : MonoBehaviour, IDropHandler
{
    public const string SLOT = "GRID";      //name of slots' parent
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropp = eventData.pointerDrag;
        DragNDrop dragdrop = dropp.GetComponent<DragNDrop>();

        if (transform.childCount == 0)          //if the slot is not occupied
            dragdrop.parentDrag = transform;
        else if (dragdrop.ppap.parent.name != DragNDrop.DADDY)   //if it's not directly from spawner
            dragdrop.parentDrag = dragdrop.ppap;   //put it back where it was       


    }


}
