using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player1;
    public Hero player1Script;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player1Script = player1.GetComponent<Hero>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        player1Script.jumpPressed = true;
        Debug.Log(name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        player1Script.jumpPressed = false;
        Debug.Log(name + "No longer being clicked");
    }

    public void Jump(){
        Debug.Log("Jumped");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
