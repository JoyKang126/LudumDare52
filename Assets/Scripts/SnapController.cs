using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Tile> snapPoints;
    public List<DraggableItem> draggableObjects;
    public float snapRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(DraggableItem draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(DraggableItem draggable)
    {
        float closestDistance = -1;
        Tile closestSnapPoint = null;

        foreach(Tile snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.GetChild(0).position, snapPoint.transform.position);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && draggable.validSpot && !closestSnapPoint.isOccupied && closestDistance <= snapRange)
        {
            Vector3 offset = draggable.transform.position - draggable.transform.GetChild(0).position;
            draggable.transform.position = closestSnapPoint.transform.position + offset;

            draggable.AddSnaps();
            CheckEndState();
        }
        else if (!draggable.notInBarn)
        {
            draggable.transform.position = draggable.lastPosition;
        }
    }

    void CheckEndState()
    {
        bool emptySpot = false;
        foreach(Tile snapPoint in snapPoints)
        {
            if (!snapPoint.isOccupied)
            {
                emptySpot = true;
                break;
            }
        }
        if(!emptySpot)
        {
            //next level
            Debug.Log("win!");
        }
    }
}
