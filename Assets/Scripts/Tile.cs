using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public Tile top = null;
    public Tile left = null;
    public Tile right = null;
    public Tile down = null;
    public string tiletag;

    public bool willTheyFight(string potag)
    {
        Debug.Log(down.gameObject.tag);
        if (potag == "banana")
        {
            if (top.tiletag == "apple" || left.tiletag == "apple" || right.tiletag == "apple" || down.tiletag == "apple")
            {
                Debug.Log("no banana!");
                return true;
            }
        }
        else if (potag == "apple")
        {
            if (top.tiletag =="banana" || left.tiletag == "banana" || right.tiletag == "banana" || down.tiletag == "banana")
            {
                Debug.Log("no apple!");
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    isOccupied = false;
    //}
}

