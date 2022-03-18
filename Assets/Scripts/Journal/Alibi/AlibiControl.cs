using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this class stores and organize all entries of alibi*/
public class AlibiControl : MonoBehaviour
{
    [SerializeField] private List<entry> alibies = new List<entry>();
    [SerializeField] private GameObject alibiEntry;

    //getters & setters
    public List<entry> Alibies {get {return alibies;} set {alibies = value;}}

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /*draw alibi entries
      basically put name, image, and description on alibi entry prefab*/
    public void showEntries() {
      //for now instantiate new alibi entry every time, change to object pool when we know how many entries should be one page
      for (int i = 0; i < alibies.Count; i++) {
        GameObject newEntry = Instantiate(alibiEntry, Vector2.zero, Quaternion.identity);
        newEntry.transform.SetParent(gameObject.transform);
        
        //set information of new entry
        AlibiEntry newAlibi = newEntry.GetComponent<AlibiEntry>();
        newAlibi.CurrentEntry = alibies[i];
        newAlibi.drawSelf();
      }
    }
}
