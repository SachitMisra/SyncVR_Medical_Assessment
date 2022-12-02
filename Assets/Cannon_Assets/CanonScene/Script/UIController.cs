using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject play, creds, quit, menu, next, levels, Canon;
    public List<Transform> levelList = new List<Transform>();
    public int selector = 0;

    public void PlayAction()
    {
        Canon.GetComponent<CanonController>().enabled = true;
        play.SetActive(false);
        creds.SetActive(false);
        quit.SetActive(false);
        menu.SetActive(true);
        next.SetActive(true);
    }
    public void CredsAction()
    {
        creds.transform.GetChild(0).gameObject.SetActive(true);
        creds.transform.GetChild(1).gameObject.SetActive(true);
        menu.SetActive(true);
    }

    public void QuitAction()
    {
        Application.Quit();
    }
    public void MenuAction()
    {
        Canon.GetComponent<CanonController>().enabled = false;
        play.SetActive(true);
        creds.SetActive(true);
        quit.SetActive(true);
        menu.SetActive(false);
        next.SetActive(false);
        creds.transform.GetChild(0).gameObject.SetActive(false);
        creds.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void NextAction()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag ("Clone");
     
     for(var i = 0 ; i < gameObjects.Length ; i ++)
     {
         Destroy(gameObjects[i]);
     }
        selector++;
            selector%=levelList.Count;
            if(levels.transform.childCount > 0)
            Destroy(levels.transform.GetChild(0).gameObject);      
            var level = Instantiate(levelList[selector].gameObject,levels.transform.position,Quaternion.identity);
            level.transform.SetParent(levels.transform);
    }
}
