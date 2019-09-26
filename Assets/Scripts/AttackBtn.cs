using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player1;
    public Hero player1Script;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player1Script = player1.GetComponent<Hero>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //player1Script.attackPressed = true;
        //Debug.Log("NewAttack");
        
    }
    
    public void attack(){
        Debug.Log("Attacked");
    }

    
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        player1Script.attackPressed = true;
        Debug.Log("NewAttack");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        player1Script.attackPressed = false;
        Debug.Log("NewAttack");
    }
    // void OnMouseDown()
    // {
    //     player1Script.attackPressed = true;
    //     Debug.Log("NewAttack");
    // }
}
