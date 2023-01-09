using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFeature : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject levelClear;

    public void LevelClear()
    {
        levelClear.SetActive(false);
    }
}
