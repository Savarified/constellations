using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    private float walk_speed = 2f, sprint_speed = 3.5f;
    private Vector3 input_delta;
    private float diag_mult = 0.707f;

    [Header("Animation Settings")]
    //states
    public float cycle_speed;
    public float movement_speed;
    public float idle_speed;
    private float tick;
    public int frame;
    public bool walking, sprinting;
    //all walk cycles
    public List<Sprite> n_sprites;
    public List<Sprite> ne_sprites;
    public List<Sprite> e_sprites;
    public List<Sprite> se_sprites;
    public List<Sprite> s_sprites;

    public List<Sprite> sprite_set;

    private SpriteRenderer sr;

    void Awake(){
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        input_delta.x = Input.GetAxis("Horizontal");
        input_delta.y = Input.GetAxis("Vertical");

        if (input_delta.magnitude > 0f){
            walking = true;
        }
        else{walking = false;}

        sprinting = Input.GetKey(KeyCode.LeftShift);
        if (sprinting){walking = false;}

        if (input_delta.x > 0f){
            sr.flipX = false;
        }
        if (input_delta.x < 0f){
            sr.flipX = true;
        }
        tick += Time.deltaTime;
        SelectSprite();
        Move();
    }

    void Move()
    {
        if(sprinting){
            speed = sprint_speed;
        }
        else{speed = walk_speed;}

        if ( (Mathf.Abs(input_delta.x) + Mathf.Abs(input_delta.y) ) > 1.1f){
            speed *= diag_mult;
        }
        transform.position += input_delta * speed * Time.deltaTime;
    }

    void SelectSprite(){
        sprite_set = GetSpriteDirection();
        if(tick > (cycle_speed/speed)){
            tick = 0;
            frame ++;
        }

        if (sprite_set != null){
            if(frame > (sprite_set.Count-1)){
                frame = 0;
            }
            sr.sprite = sprite_set[frame];
        }
        else{
            frame = 0;
        }
    }

    List<Sprite> GetSpriteDirection(){
        cycle_speed = movement_speed;
        Vector2 dir = new Vector2(input_delta.x, input_delta.y);
        float t = 0f; // Threshold is the minimum value to be exceeded to be considered motion

        if (dir.y > t){ //N
            if ( (dir.x > t)||(dir.x < -t) ){ //NE (NW)
                return ne_sprites;
            }
            else{
                return n_sprites;
            }
        }

        if(dir.y < -t){ //S
            if ( (dir.x > t)||(dir.x < -t)){ //SE (SW)
                return se_sprites;
            }
            return s_sprites;
        }

        if (Mathf.Abs(dir.y) <= t){ // if no y movement
            if (Mathf.Abs(dir.x) > t){
                return e_sprites;
            }
        }

        if(sprite_set.Count > 2){
            List<Sprite> idle = new List<Sprite>();
            idle.Add(sprite_set[0]);
            idle.Add(sprite_set[2]);
            return idle;
        }
        else{
            cycle_speed = idle_speed;
            return sprite_set;
        }
    }
}
