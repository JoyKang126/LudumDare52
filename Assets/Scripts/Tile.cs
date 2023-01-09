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
        if (potag == "banana")
        {
            if (top.tiletag == "onion" || left.tiletag == "onion" || right.tiletag == "onion" || down.tiletag == "onion")
            {
                return true;
            }
        }
        else if (potag == "onion")
        {
            if (top.tiletag =="banana" || left.tiletag == "banana" || right.tiletag == "banana" || down.tiletag == "banana")
            {
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

