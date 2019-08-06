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
     [SerializeField]
     private float speed = 0.3f;

     private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
     _rigid = GetComponent<Rigidbody2D>();
     _sprite = GetComponentInChildren<SpriteRenderer>();
     _anim = GetComponentInChildren<Animator>();
     _anim.SetBool("strike", false);
        
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        
    }

    void Movement(){
    float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
    //float verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

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

    if(UnityEngine.Input.GetKeyDown(KeyCode.E)) {
        _anim.SetBool("strike", true);
    } else {
        _anim.SetBool("strike", false);
    };

    // if(_grounded == true) {
    //     _anim.SetBool("jump", true);
    // } else{
    //     _anim.SetBool("jump", false);
    // }

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

}


