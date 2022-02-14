using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerControl : MonoBehaviour
{
    [SerializeField, Range(3.0f,15.0f)] private float speed;
    private Vector2 _destination;
    public Vector2 Destination{get {return _destination;} set {_destination = value;}}

    public InteractiveObj interactingObj;
    public DialogueRunner runner;

    //states
    private PlayerStateBase currentState;
    public PlayerStateExplore stateExplore = new PlayerStateExplore();
    public PlayerStateDialogue stateDialogue = new PlayerStateDialogue();

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

    void Start()
    {
        changeState(stateExplore);
        _destination = transform.position;
    }

    void Update()
    {
        currentState.updateState(this);
    }

    void FixedUpdate() {
        currentState.fixedUpdate(this);
    }

    public void moveTo(Vector2 destination) {
        transform.position = Vector2.Lerp(transform.position, Vector2.MoveTowards(transform.position, destination, Time.deltaTime*speed), 0.5f);
    }
}
