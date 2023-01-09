using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFeature : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject levelClear;
    public AudioManager audioManager;

    public void LevelClear()
    {
        FindObjectOfType<AudioManager>().Play("button");
        levelClear.SetActive(false);
    }
}
