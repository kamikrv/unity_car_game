using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Transform[] stars;

    public void ChangeStar(int starIndex)
    {
        if (starIndex <= 5)
        {
            stars[starIndex-1].GetComponent<Image>().color = Color.white;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
