using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Yarn.Unity;

public class InteractiveObj : MonoBehaviour, IInteractable, ITalkable
{
    [SerializeField, BoxGroup("Entry Info")] private string entryName;
    [SerializeField, BoxGroup("Entry Info"), ResizableTextArea] private string entryDes;
    [SerializeField, BoxGroup("Entry Info")] private Sprite entryImage;
    [SerializeField, BoxGroup("Dialogue")] private bool _talkable;
    [SerializeField, BoxGroup("interaction")] private bool _interactable;
    [SerializeField, BoxGroup("interaction"), Header("do interact before dialogue"), EnableIf("_interactable")] private bool _interactFirst;
    [SerializeField, EnableIf("_talkable"), BoxGroup("Dialogue")] private string _startNode;
    [SerializeField, BoxGroup("collider")] private Collider2D collider;

    private entry entryInfo;
    
    
    //getter & setters
    public string StartNode{get {return _startNode;} set {_startNode = value;}}
    public bool IsTalkable{get {return _talkable;} set {_talkable = value;}}
    public bool IsInteractable{get {return _interactable;} set {_interactable = value;}}
    public bool IsInteractFirst{get {return _interactFirst;} private set{_interactFirst = value;}}
    public entry Entry {
        get {
            return entryInfo;
        } private set {
            Debug.Log("setting");
            entryInfo.entryName = this.entryName;
            entryInfo.entryDes = this.entryDes;
            entryInfo.entryImage = this.entryImage;
            Debug.Log(Entry.entryName);
        }
    }

    void Start()
    {
        Entry = entryInfo;

        //check everything has set up
        if (collider == null)
            Debug.LogWarning(name + " has not set up a collider yet");
        
        if (!_interactable)
            _interactFirst = false;
    }

    void Update()
    {
        
    }

    public virtual void interact() {
        
    }

    /*
     * start dialogue by using this object's start node
     */
    public virtual void talk() {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        DialogueRunner runner = player.runner;
        runner.StartDialogue(StartNode);
        player.changeState(player.stateDialogue);
    }
}
