using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using NaughtyAttributes;

public class PlayerControl : MonoBehaviour
{
    [SerializeField, Range(3.0f,15.0f), BoxGroup("Movement")] private float speed;
    private Vector2 _destination;
    private SpriteRenderer myRenderer;

    //getters & setters
    public SpriteRenderer MyRenderer {get {return myRenderer;} private set {myRenderer = value;}}
    public Vector2 Destination{get {return _destination;} set {_destination = value;}}

    //dialogue
    [BoxGroup("Dialgoue")] public InteractiveObj interactingObj;
    [BoxGroup("Dialgoue")] public DialogueRunner runner;

    //journal
    [SerializeField, BoxGroup("Journal")] private GameObject journalContainer;

    //post processing
    [BoxGroup("post-processing")] public GameObject blurCamera; //active it when want to blur the camera

    //states
    private PlayerStateBase currentState;
    public PlayerStateExplore stateExplore = new PlayerStateExplore();
    public PlayerStateDialogue stateDialogue = new PlayerStateDialogue();
    public PlayerStateJournal stateJournal = new PlayerStateJournal();

    public void changeState(PlayerStateBase newState) {
        if (currentState != null)
        {
            currentState.leaveState(this);
        }

        currentState = newState;

        if (currentState != null)
        {  
            currentState.enterState(this);
        }
    }

    //changing state function, for unity event (button)
    public void changeToExploreState() {changeState(stateExplore);}
    public void changeToDialogueState() {changeState(stateDialogue);}
    public void changeToJournalState() {changeState(stateJournal);}

    void Start()
    {
        changeState(stateExplore);
        myRenderer = GetComponent<SpriteRenderer>();
        _destination = transform.position;
    }

    void Update()
    {
        currentState.updateState(this);
    }

    void FixedUpdate() {
        currentState.fixedUpdate(this);
    }

    /**
    * move this object to destination
    * meanwhile, adjust sprite facing direction
    */
    public void moveTo(Vector2 destination) {
        Vector2 movingPos = Vector2.MoveTowards(transform.position, destination, Time.deltaTime*speed);
        if (movingPos.x - transform.position.x > 0)
            myRenderer.flipX = true;
        else
            myRenderer.flipX = false;
        
        transform.position = Vector2.Lerp(transform.position, movingPos, 0.5f);;
    }

    //open and close things
    public void openJournal() {
        journalContainer.SetActive(true);
    }
    public void closeJournal() {
        journalContainer.SetActive(false);
    }
}
