using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy6 : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigid;    
    Transform target;
    private GameObject enemy6;
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
        enemy6 = GameObject.FindWithTag("Enemy5");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
Movement();
Raycasting();

        if(player.transform.position.x >= 56.22){
            transform.position = Vector2.MoveTowards(
            transform.position, 
            new Vector2(player.transform.position.x, player.transform.position.y), 
            speed*Time.deltaTime);
        }
        
        // transform.position = Vector2.MoveTowards(
        //     transform.position, 
        //     new Vector2(player.transform.position.x, player.transform.position.y), 
        //     speed*Time.deltaTime);
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
                Invoke("reloadLevel", 3f);
                // SceneManager.LoadScene("Level1");
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
            Invoke("reloadLevel", 3f);
                // SceneManager.LoadScene("Level1");
        }


    }

    void Movement(){

    //float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        if(enemy6.transform.position.x > player.transform.position.x){
            _sprite.flipX = false;
        }else {
            _sprite.flipX = true;
        }

        //Debug.Log(enemy6.transform.position.x);
    }

    void reloadLevel()
     {
     SceneManager.LoadScene("Level1");
     }
}

//else if(enemy.transform.position.x > 0 && player.transform.position.x > 0) {
        //_sprite.flipX = true;

