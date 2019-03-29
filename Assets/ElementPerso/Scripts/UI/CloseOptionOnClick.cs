using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptionOnClick : MonoBehaviour {

    public GameObject infoHolder;
    public GameObject[] toReveal;

    public void CloseOption()
    {
        infoHolder.SetActive(false);
        foreach (GameObject go in toReveal)
        {
            go.SetActive(true);
        }
    }
}
