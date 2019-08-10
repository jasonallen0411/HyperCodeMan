using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _sprite;
     private Rigidbody2D _rigid;
     [SerializeField]
     private float _jumpForce = 5.0f;
     [SerializeField]
     private bool _grounded = false;
    [SerializeField]

     private LayerMask _groundLayer;
     private bool resetJumpNeeded;
     private bool strikeReady = true;                                  
     [SerializeField]
     private float speed = 0.3f;

     private Animator _anim;

     private GameObject enemy;
     private GameObject player;

     public bool hammerHit = false;
     public Transform lineStart, lineEnd;
     public Transform lineStartX, lineEndX;

     RaycastHit2D hammerHitEnemy;

    // Start is called before the first frame update
    void Start()
    {
     _rigid = GetComponent<Rigidbody2D>();
     _sprite = GetComponentInChildren<SpriteRenderer>();
     _anim = GetComponentInChildren<Animator>();
     _anim.SetBool("strike", false);
     enemy = GameObject.FindWithTag("Enemy");
     player = GameObject.FindWithTag("Player");
     
     
     //-16(value of y postion I want player to die)
     //Respawn location: -6.5x 1.57y
     //end level location: 88.05515x 2.415138y
        
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Raycasting();
        
    }

    void Raycasting()
    {
        Debug.DrawLine(lineStart.position, lineEnd.position, Color.green);
        Debug.DrawLine(lineStartX.position, lineEndX.position, Color.red);

        if(_sprite.flipX == true){
            if( Physics2D.Linecast (lineStartX.position, lineEndX.position, 1 << LayerMask.NameToLayer("Enemy") ) ){
                hammerHitEnemy = Physics2D.Linecast (lineStartX.position, lineEndX.position, 1 << LayerMask.NameToLayer("Enemy"));
                hammerHit = true;
            } else {
                hammerHit = false;
            }

            if(Input.GetKeyDown(KeyCode.E) && hammerHit == true){
                Destroy (hammerHitEnemy.collider.gameObject, .5f);
            }
        }

        if( Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Enemy") ) ){
            hammerHitEnemy = Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Enemy"));
            hammerHit = true;
        } else {
            hammerHit = false;
        }

        if(Input.GetKeyDown(KeyCode.E) && hammerHit == true){
            Destroy (hammerHitEnemy.collider.gameObject, .5f);
        }

    }



    void Movement(){
    float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");

    if(horizontalInput < 0 ){
        _sprite.flipX = true;
    }else if(horizontalInput > 0) {
        _sprite.flipX = false;
    }

    if( horizontalInput > 0 || horizontalInput < 0){
        _anim.SetBool("move", true);
    } else {
        _anim.SetBool("move", false);
    }

    if(UnityEngine.Input.GetKeyDown(KeyCode.E) && strikeReady ) {
        _anim.SetBool("strike", true);
        strikeReady = false;
        StartCoroutine(ResetStrikeRoutine());
    } 

    if(player.transform.position.y <= -16f){
        //Destroy (player);
        player.transform.position = new Vector2(-6.5f,1.57f);
    }
    if(player.transform.position.x >= 88.05515f && player.transform.position.y >= 2.415138){
        //Destroy (player);
        player.transform.position = new Vector2(-6.5f,1.57f);
    }

    //Debug.Log(_sprite);
    Debug.Log(horizontalInput);
    //Debug.Log(_grounded);


    _grounded = false;
    isGrounded();
    _anim.SetBool("jump", !_grounded);

    _rigid.velocity = new Vector2(horizontalInput * speed, _rigid.velocity.y);
    if(UnityEngine.Input.GetKeyDown(KeyCode.Space) && _grounded == true){
            
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _grounded = false;
            //_anim.SetBool("jump", true);
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
    }  
      
} 

    bool isGrounded() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, _groundLayer.value);

        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);

        if(hitInfo.collider != null){
            Debug.Log("You hit " + hitInfo.collider.name);
            _grounded = true;

            if(resetJumpNeeded == false)
                return true;
        }
        return false;

    }

    IEnumerator ResetJumpNeededRoutine(){
        yield return new WaitForSeconds(.1f);
        resetJumpNeeded = false;
    }

    IEnumerator ResetStrikeRoutine(){
        yield return new WaitForSeconds(.6f);
        strikeReady = true;
        _anim.SetBool("strike", false);
    }

}


