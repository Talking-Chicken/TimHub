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
    [SerializeField, BoxGroup("Entry Info")] private EntryType entryType;
    [SerializeField, BoxGroup("Character Info")] private string characterName;
    [SerializeField, BoxGroup("Dialogue")] private bool _talkable;
    [SerializeField, BoxGroup("interaction")] private bool _interactable;
    [SerializeField, BoxGroup("interaction"), Header("do interact before dialogue"),ShowIf("_talkable")] 
    private bool _interactFirst;
    [SerializeField, EnableIf("_talkable"), BoxGroup("Dialogue")] private string _startNode;
    [SerializeField, BoxGroup("collider")] private Collider2D collider;

    private entry entryInfo;
    [SerializeField, BoxGroup("Character Info")] private List<entry> entryList;
    
    
    //getter & setters
    public string StartNode{get {return _startNode;} set {_startNode = value;}}
    public bool IsTalkable{get {return _talkable;} set {_talkable = value;}}
    public bool IsInteractable{get {return _interactable;} set {_interactable = value;}}
    public bool IsInteractFirst{get {return _interactFirst;} private set{_interactFirst = value;}}
    public entry Entry {
        get {
            return entryInfo;
        } private set {
            entryInfo.entryName = this.entryName;
            entryInfo.entryDes = this.entryDes;
            entryInfo.entryImage = this.entryImage;
            entryInfo.entryType = this.entryType;
        }
    }

    public List<entry> EntryList {get => entryList; set => entryList = value;}

    void Start()
    {
        //set gameobject name to character name
        if (characterName != "")
            name = characterName;
        else
            Debug.LogWarning("didn't set a character name for " + name);

        //initialize Entry
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
