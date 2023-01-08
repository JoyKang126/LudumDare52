using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (closestSnapPoint != null && draggable.validSpot && draggable.CheckSnaps() && draggable.CheckTags() && closestDistance <= snapRange)
        {
            Vector3 offset = draggable.transform.position - draggable.transform.GetChild(0).position;
            draggable.transform.position = closestSnapPoint.transform.position + offset;

            draggable.AddSnaps();
            draggable.snappedToLast.Clear();
            CheckEndState();
        }
        else if (!draggable.notInBarn)
        {
            //teleport, isn't triggering on triggerexit
            draggable.transform.position = draggable.lastPosition;
            foreach(Tile tile in draggable.snappedToLast)
            {
                draggable.snappedTo.Add(tile);
                tile.gameObject.GetComponent<Tile>().isOccupied = true;
                tile.gameObject.GetComponent<Tile>().tiletag = draggable.transform.gameObject.tag;
            }
            draggable.snappedToLast.Clear();
        }
        else
        {
            draggable.transform.localScale = new Vector3(0.5f,0.5f,1);
            draggable.transform.position = draggable.startPosition;
            draggable.snappedToLast.Clear();
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
            SceneManager.LoadScene("Scenes/SampleScene");
            Debug.Log("win!");
        }
    }
}
