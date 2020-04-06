using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] stars;


    void Start() {
        instance = this;

        foreach(GameObject star in stars) {
            star.SetActive(false);
        }

    }
    
    public void UpdateStars(int startIndex, bool status) {
        stars[startIndex].SetActive(status);
    }

}
