using UnityEngine;

public class Crawler : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float sight_range, attack_range;
    [SerializeField] private int damage;
    [SerializeField] private Animator anim;
    [SerializeField] private float time_to_attack, time_to_patrol;
    private float attack_tick, patrol_tick;
    public GameObject player;
    private bool moving, attacking;
    [Header("Movement Settings")]
    [SerializeField] private float move_speed;
    private Vector2 patrol_target;
    private Vector2 prev_pos;
    void Awake(){
        anim = gameObject.GetComponent<Animator>();
        prev_pos = transform.position;
    }
    void FixedUpdate(){
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist <= attack_range){
            attacking = true;
        }
        else if(dist <= sight_range){
            attacking = false;
            moving = true;
        }
        else{
            moving = false;
            patrol_tick += Time.deltaTime;
            if (patrol_tick >=  time_to_patrol){
                patrol_tick = 0f;
                time_to_patrol = Random.Range(0.5f, 5f);
                patrol_target = Random.insideUnitCircle * 7f;
            }
            transform.position = Vector2.MoveTowards(transform.position, patrol_target, move_speed * Time.deltaTime);
        }

        anim.SetBool("move", true);

        if (moving){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, move_speed * Time.deltaTime);
        }

        if (attacking){
            attack_tick += Time.deltaTime;
            if (attack_tick >= time_to_attack){
                attack_tick = 0f;
                Attack();
            }
        }

        if(prev_pos.x > transform.position.x){
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        else{
            transform.localScale = new Vector3(1f,1f,1f);
        }

        prev_pos = transform.position;

    }

    void Attack()
    {
        //player.GetComponent<PlayerStats>().health -= damage; not implemented yet
        anim.SetTrigger("bite");
    }

    void SetNewDestination(){

    }
}
