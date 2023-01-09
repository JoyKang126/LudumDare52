using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public delegate void DragEndedDelegate(DraggableItem draggableObject);
    public DragEndedDelegate dragEndedCallback;

    private bool isDragged = false;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    public Vector3 lastPosition;
    public Vector3 startPosition;
    public List<Collider2D> collideWith;
    public List<Tile> snappedTo;
    public List<Tile> snappedToLast;
    public AudioManager audioMan;
    [SerializeField] private int size;

    public bool validSpot; 
    public bool notInBarn;

    void Awake()
    {
        startPosition = transform.position;
    }

    private void OnMouseDown() 
    {
        isDragged = true;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        audioMan.Play("choose");
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(1,1,1);
        spriteDragStartPosition = transform.localPosition;
        lastPosition = transform.position;    
        foreach(Tile tile in snappedTo)
        {
            snappedToLast.Add(tile);
        }
        if (snappedTo.Count > 0)
        {
                DeleteSnaps();
        }    
    }
    private void OnMouseDrag()
    {
        if(isDragged) {
            transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);

            //Rotate
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Rotate(-1);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Rotate(1);
            }
        }
    }
    private void OnMouseUp()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        isDragged = false;
        if (collideWith.Count == size)
        {
            notInBarn = false;
            validSpot = true;
        }
        else if (collideWith.Count == 0)
        {
            notInBarn = true;
            validSpot = false;
        }
        else
        {
            notInBarn = false;
            validSpot = false;    
        }
        dragEndedCallback(this);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!collideWith.Contains(other))
        {
            collideWith.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        collideWith.Remove(other);
    }

    public bool CheckSnaps()
    {
        foreach (Collider2D tile in collideWith)
        {
            if (tile.gameObject.GetComponent<Tile>().isOccupied == true)
            {
                return false; //tile already occupied
            }
        }
        return true; //tiles are all free, valid spot
    }

    public bool CheckTags()
    {
        foreach (Collider2D tile in collideWith)
        {
            if (tile.gameObject.GetComponent<Tile>().willTheyFight(gameObject.tag))
            {
                return false; //can't be next to this tile
            }
        }
        return true; //no offending tiles, valid spot
    }

    public void AddSnaps()
    {
        foreach (Collider2D tile in collideWith)
        {
            tile.gameObject.GetComponent<Tile>().isOccupied = true;
            tile.gameObject.GetComponent<Tile>().tiletag = gameObject.tag;
            snappedTo.Add(tile.gameObject.GetComponent<Tile>());
        }
    }

    private void DeleteSnaps()
    {
        foreach (Collider2D tile in collideWith)
        {
            tile.gameObject.GetComponent<Tile>().isOccupied = false;
            tile.gameObject.GetComponent<Tile>().tiletag = "";
            snappedTo.Remove(tile.gameObject.GetComponent<Tile>());
        }
    }

    private void Rotate(int direction)
    {
        if (direction == 1)
        {
            transform.Rotate(0, 0, -90);
        }
        else
        {
            transform.Rotate(0, 0, 90);
        }
    }
    
}
