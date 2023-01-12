using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<DraggableItem>().hittingBound = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<DraggableItem>().hittingBound = false;
    }
}
