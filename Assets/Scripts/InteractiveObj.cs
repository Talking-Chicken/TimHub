using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InteractiveObj : MonoBehaviour, IInteractable, ITalkable
{
    [SerializeField, BoxGroup("Dialogue")] private bool _talkable;
    [SerializeField] private bool _interactable;
    [SerializeField, EnableIf("_talkable"), BoxGroup("Dialogue")] private string _talkerName;
    [SerializeField, EnableIf("_talkable"), BoxGroup("Dialogue")] private string _startNode;
    
    //getter & setters
    public string TalkerName{get {return _talkerName;} set {_talkerName = value;}}
    public string StartNode{get {return _startNode;} set {_startNode = value;}}
    public bool isTalkable{get {return _talkable;} set {_talkable = value;}}
    public bool isInteractable{get {return _interactable;} set {_interactable = value;}}

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void interact() {
        
    }

    public virtual void talk() {

    }

}
