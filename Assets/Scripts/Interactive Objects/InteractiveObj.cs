using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Yarn.Unity;

public class InteractiveObj : MonoBehaviour, IInteractable, ITalkable
{
    [SerializeField, BoxGroup("Dialogue")] private bool _talkable;
    [SerializeField, BoxGroup("interaction")] private bool _interactable;
    [SerializeField, BoxGroup("interaction"), Header("do interact before dialogue"), ShowIf("_talkable")]
    private bool _interactFirst;
    [SerializeField, EnableIf("_talkable"), BoxGroup("Dialogue")] private string _startNode;
    [SerializeField, BoxGroup("collider")] private Collider2D collider;

    private entry entryInfo;
    [SerializeField, BoxGroup("Character Info")] private List<entry> entryList;

    //hovering over objects variables (including cursor)
    private SpriteRenderer objSprite;

    //pathfinding destination
    [SerializeField, BoxGroup("Path Finding")] private bool hasDifferentDestination;
    [SerializeField, EnableIf("hasDifferentDestination"), BoxGroup("Path Finding")] private Transform destination;
    [SerializeField, EnableIf("hasDifferentDestination"), BoxGroup("Path Finding")] private List<Transform> destinations;
    
    private PlayerControl player;

    //getter & setters
    public string StartNode { get { return _startNode; } set { _startNode = value; } }
    public bool IsTalkable { get { return _talkable; } set { _talkable = value; } }
    public bool IsInteractable { get { return _interactable; } set { _interactable = value; } }
    public bool IsInteractFirst { get { return _interactFirst; } private set { _interactFirst = value; } }
    public List<entry> EntryList { get => entryList; set => entryList = value; }
    public bool HasDifferentDestination {get => hasDifferentDestination;}
    public Transform Destination {get => destination;}
    public List<Transform> Destinations {get => destinations;}

    void Start()
    {
        //check everything has set up
        if (collider == null)
            Debug.LogWarning(name + " has not set up a collider yet");

        if (!_interactable)
            _interactFirst = false;

        objSprite = collider.gameObject.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerControl>();
    }

    public virtual void interact() {
        
    }

    /*
     * start dialogue by using this object's start node
     */
    public virtual void talk()
    {
        DialogueRunner runner = player.runner;
        runner.StartDialogue(StartNode);
        player.changeState(player.stateDialogue);
    }

    void OnMouseOver()
    {
        if (player.CanInvestigate)
            objSprite.color = new Color(1, (float)0.7, (float)0.9, 1);
    }

    public void OnMouseExit()
    {
        if (player.CanInvestigate)
            objSprite.color = Color.white;
    }
}
