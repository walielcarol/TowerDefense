using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOptionOnClick : MonoBehaviour {

    public GameObject infoHolder;
    public GameObject[] covered;

    public void ShowOption()
    {
        infoHolder.SetActive(true);
        foreach(GameObject go in covered)
        {
            go.SetActive(false);
        }
    }
}
