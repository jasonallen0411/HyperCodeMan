using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigid;    
    Transform target;
    private GameObject enemy;
    private GameObject player;
    private float range;
    [SerializeField]
    public float speed = 3f;
    public bool hit = false;

    public Transform enemyLineStart, enemyLineEnd;

    RaycastHit2D hitPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(GameObject.FindGameObjectsWithTag("Player"));
        _rigid = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindWithTag("Enemy");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
Movement();
Raycasting();
        
        transform.position = Vector2.MoveTowards(
            transform.position, 
            new Vector2(player.transform.position.x, player.transform.position.y), 
            speed*Time.deltaTime);
    }
    void Raycasting()
    {
        Debug.DrawLine(enemyLineStart.position, enemyLineEnd.position, Color.green);

        if( Physics2D.Linecast (enemyLineStart.position, enemyLineEnd.position, 1 << LayerMask.NameToLayer("Player") ) ){
            hitPlayer = Physics2D.Linecast (enemyLineStart.position, enemyLineEnd.position, 1 << LayerMask.NameToLayer("Player"));
            hit = true;
        } else {
            hit = false;
        }

        if(hit == true){
            Destroy (hitPlayer.collider.gameObject, .5f);
        }

        Debug.Log(enemyLineEnd.position);

    }

    void Movement(){

    //float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");

    if(enemy.transform.position.y < 0 ){
        _sprite.flipX = false;
    }else if(enemy.transform.position.y > 0) {
        _sprite.flipX = true;
    }
    }
}

