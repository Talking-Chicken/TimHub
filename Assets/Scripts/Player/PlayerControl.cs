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
    public Animator myAnimator;
    private Vector3 moveDir;

    //mouse detection
    [SerializeField] private GameObject hoveringObj;

    //dialogue
    [BoxGroup("Dialgoue")] public InteractiveObj interactingObj;
    [BoxGroup("Dialgoue")] public DialogueRunner runner;

    //journal
    [SerializeField, BoxGroup("Journal")] private GameObject journalContainer;
    [SerializeField, BoxGroup("Journal")] private JournalControl journal;

    //getters & setters
    public SpriteRenderer MyRenderer {get {return myRenderer;} private set {myRenderer = value;}}
    public Vector2 Destination{get {return _destination;} set {_destination = value;}}
    public JournalControl Journal {get {return journal;}}
    public GameObject HoveringObj {get => hoveringObj; set => hoveringObj = value;}

    //post processing
    [BoxGroup("post-processing")] public GameObject blurCamera; //active it when want to blur the camera

    //states
    private PlayerStateBase currentState;
    public PlayerStateBase previousState{set; get;}
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

    public IEnumerator waitToChangeState(PlayerStateBase newState) {
        yield return new WaitForSeconds(0.2f);
        changeState(newState);
    }

    //changing state function, for unity event (button)
    public void changeToExploreState() {changeState(stateExplore);}
    public void changeToDialogueState() {changeState(stateDialogue);}
    public void changeToJournalState() {changeState(stateJournal);}
    public void changeToPreviousState() {
        if (previousState != null)
            changeState(previousState);
        else
            changeToExploreState();
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        changeState(stateExplore);
        myRenderer = GetComponent<SpriteRenderer>();
        _destination = transform.position;
    }

    void Update()
    {
        HoveringObj = mouseHoveringObj();
        currentState.updateState(this);
        Animate();
        
        //Debug.Log(Vector2.Distance(transform.position, Destination));
        
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
        /*if (movingPos.x - transform.position.x > 0)
            myRenderer.flipX = true;
        else
            myRenderer.flipX = false;*/

        Vector2 direction = new Vector2(movingPos.x - transform.position.x, movingPos.y - transform.position.y);
        float moveX = 0f;
        float moveY = 0f;

        moveX = direction.x;
        moveY = direction.y;
        moveDir = new Vector3(moveX, moveY).normalized;

        transform.position = Vector2.Lerp(transform.position, movingPos, 0.5f);;
    }

    //open and close things
    public void openJournal() {
        journalContainer.SetActive(true);
        journal.changeState(journal.stateAlibis);
    }
    public void closeJournal() {
        journalContainer.SetActive(false);
        journal.changeState(journal.stateIdle);
    }

    /* return Gameobject that player mouse is hovering*/
    public GameObject mouseHoveringObj() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            return hit.collider.gameObject;
        }
        return null;
    }

    /* teleport player to destination, also set _destination to new position*/
    public void teleport(Vector2 destination) {
        transform.position = new Vector3(destination.x, destination.y, transform.position.z);
        Camera.main.transform.position = new Vector3(destination.x, destination.y, Camera.main.transform.position.z);
        Destination = destination;
    }

    void Animate() {
        myAnimator.SetFloat("moveX", moveDir.x);
        myAnimator.SetFloat("moveY", moveDir.y);
        myAnimator.SetFloat("moveMagnitude",  Vector2.Distance(transform.position, Destination));
    }
}
