using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnapController : MonoBehaviour
{
    public List<Tile> snapPoints;
    public List<DraggableItem> draggableObjects;
    public float snapRange = 0.4f;
    public GameObject levelClearWindow;

    private bool tagClash;
    // Start is called before the first frame update
    void Start()
    {
        foreach(DraggableItem draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
        levelClearWindow.SetActive(false);
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

        if (draggable.CheckTags())
        {
            tagClash = true;
        }
        else
        {
            tagClash = false;
            FindObjectOfType<AudioManager>().Play("fart");
        }

        if (closestSnapPoint != null && draggable.validSpot && draggable.CheckSnaps() && tagClash && closestDistance <= snapRange) //snap to new position
        {
            FindObjectOfType<AudioManager>().Play("snap");
            Vector3 offset = draggable.transform.position - draggable.transform.GetChild(0).position;
            draggable.transform.position = closestSnapPoint.transform.position + offset;

            draggable.AddSnaps();
            draggable.snappedToLast.Clear();
            CheckEndState();
        }
        else if (!draggable.notInBarn) //go back to last position
        {          
            draggable.transform.rotation = draggable.lastRotation;
            //teleport, isn't triggering on triggerexit
            if (draggable.lastPosition == draggable.startPosition)
            {
                draggable.transform.localScale = new Vector3(0.7f,0.7f,1);
            }
            draggable.transform.position = draggable.lastPosition;
            foreach(Tile tile in draggable.snappedToLast)
            {
                draggable.snappedTo.Add(tile);
                tile.gameObject.GetComponent<Tile>().isOccupied = true;
                tile.gameObject.GetComponent<Tile>().tiletag = draggable.transform.gameObject.tag;
            }
            draggable.snappedToLast.Clear();
        }
        else //go back to start position
        {
            draggable.transform.rotation = draggable.startRotation;
            draggable.transform.localScale = new Vector3(0.7f,0.7f,1);
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
        if(!emptySpot) //beary good and load next scene
        {
            levelClearWindow.SetActive(true);
        }
    }
}