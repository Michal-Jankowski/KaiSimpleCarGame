using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStarSystem : MonoBehaviour
{
    // Start is called before the first frame update
    int currentLevel;

    public int level1, level2, level3, level4, level5;

    public bool isChasable;

    PoliceSpawner policeSpawner;

    PoliceChase[] policeman;

    void Start()
    {
        policeman = FindObjectsOfType<PoliceChase>();
        policeSpawner = FindObjectOfType<PoliceSpawner>();
        currentLevel = 0;
        StartCoroutine(DropWantedLevel());

    }

    // Update is called once per frame
    void CheckWantedLevel(int level)
    {
        if(level < level1) {
            // no wanted level
            isChasable = false;
        }
        else if (level >= level1 && level < level2) {
           // low wanted level
            isChasable = true;
            foreach(PoliceChase pol in policeman) {
                pol.isChasing = true;
                UIManager.instance.UpdateStars(0, true);
                policeSpawner.spawnTimer = level5;
            }
        }
        else if( level >= level2 && level < level3) {
            // still low wanted level
            isChasable = true;
            foreach(PoliceChase pol in policeman) {
                pol.isChasing = true;
                UIManager.instance.UpdateStars(1, true);
                policeSpawner.spawnTimer = level4;
            }
        }
        else if (level >= level3 && level < level4) {
            // medium wanted level
            isChasable = true;
            foreach (PoliceChase pol in policeman) {
                pol.isChasing = true;
                UIManager.instance.UpdateStars(2, true);
                policeSpawner.spawnTimer = level3;
            }
        }
        else if (level >= level4 && level < level5) {
            // high wanted level
            isChasable = true;
            foreach (PoliceChase pol in policeman) {
                pol.isChasing = true;
                UIManager.instance.UpdateStars(3, true);
                policeSpawner.spawnTimer = level2;
            }
        }
        else{
            // impossible wanted leve
            isChasable = true;
            foreach (PoliceChase pol in policeman) {
                pol.isChasing = true;
                UIManager.instance.UpdateStars(4, true);
                policeSpawner.spawnTimer = level1;
            }
        }

    }

    IEnumerator DropWantedLevel() {

        yield return new WaitForSeconds(currentLevel > 5 ? currentLevel : 5);
        if (!isChasable) {

            currentLevel -=1;
            if(currentLevel <= 0) {
                currentLevel = 0;
            }
            if(currentLevel < level1) {
                UIManager.instance.UpdateStars(0, false);
            }
            else if(currentLevel < level2) {
                UIManager.instance.UpdateStars(1, false);
            }
            else if (currentLevel < level3) {
                UIManager.instance.UpdateStars(2, false);
            }
            else if (currentLevel < level4) {
                UIManager.instance.UpdateStars(3, false);
            }
            else {
                UIManager.instance.UpdateStars(4, false);
            }
        }
        Debug.Log(currentLevel);
        StartCoroutine(DropWantedLevel());
    }

    public int GetCurrentLevelCheck() {
        return currentLevel;
    }

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Pedestrian")) {

            //other.gameObject.GetComponent<Animator>().SetTrigger("Die");
            Destroy(other.gameObject);
            currentLevel += 1;
            if(currentLevel >= level5) 
                currentLevel = level5;

            CheckWantedLevel(currentLevel);
            other.gameObject.tag = "Untagged";
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PoliceTrigger")) {

            if(currentLevel > level1) {
                isChasable = true;
            }
        }

        if (other.gameObject.CompareTag("Pedestrian")) {

            //other.gameObject.GetComponent<Animator>().SetTrigger("Die");
            Destroy(other.gameObject);
            currentLevel += 1;
            if (currentLevel >= level5)
                currentLevel = level5;

            CheckWantedLevel(currentLevel);
            other.gameObject.tag = "Untagged";
        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.CompareTag("PoliceTrigger")) {
            StartCoroutine(MakeUnchasable());
        }

    }

    IEnumerator MakeUnchasable() {

        yield return new WaitForSeconds(2);
        isChasable = false;
    }

}
