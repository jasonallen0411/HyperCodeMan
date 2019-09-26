using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigid;    
    Transform target;
    private GameObject enemy4;
    private GameObject player;
    private float range;
    [SerializeField]
    public float speed = 3f;
    public bool hit = false;

    public Transform enemyLineStart, enemyLineEnd;
    public Transform enemyLineStartX, enemyLineEndX;

    RaycastHit2D hitPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(GameObject.FindGameObjectsWithTag("Player"));
        _rigid = GetComponent<Rigidbody2D>();
        enemy4 = GameObject.FindWithTag("Enemy3");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
Movement();
Raycasting();

        if(enemy4.transform.position.x < player.transform.position.x){
            transform.position = Vector2.MoveTowards(
            transform.position, 
            new Vector2(player.transform.position.x, player.transform.position.y), 
            speed*Time.deltaTime);
        }
        
    }
    void Raycasting()
    {
        Debug.DrawLine(enemyLineStart.position, enemyLineEnd.position, Color.green);
        Debug.DrawLine(enemyLineStartX.position, enemyLineEndX.position, Color.red);

        if(_sprite.flipX == true){
            if( Physics2D.Linecast (enemyLineStartX.position, enemyLineEndX.position, 1 << LayerMask.NameToLayer("Player") ) ){
                hitPlayer = Physics2D.Linecast (enemyLineStartX.position, enemyLineEndX.position, 1 << LayerMask.NameToLayer("Player"));
                hit = true;
            } else {
                hit = false;
            }

            if(hit == true){
                Destroy (hitPlayer.collider.gameObject, .5f);
            }
        }

        if( Physics2D.Linecast (enemyLineStart.position, enemyLineEnd.position, 1 << LayerMask.NameToLayer("Player") ) ){
            hitPlayer = Physics2D.Linecast (enemyLineStart.position, enemyLineEnd.position, 1 << LayerMask.NameToLayer("Player"));
            hit = true;
        } else {
            hit = false;
        }

        if(hit == true){
            Destroy (hitPlayer.collider.gameObject, .5f);
        }


    }

    void Movement(){

    //float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        if(enemy4.transform.position.x > player.transform.position.x){
            _sprite.flipX = false;
        }else {
            _sprite.flipX = true;
        }

        //Debug.Log(enemy4.transform.position.x);
    }
}

//else if(enemy.transform.position.x > 0 && player.transform.position.x > 0) {
        //_sprite.flipX = true;

