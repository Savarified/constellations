using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float walk_speed, sprint_speed;
    public Vector3 input_delta;
    private float diag_mult = 0.707f;
    public bool sprinting;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        input_delta.x = Input.GetAxis("Horizontal");
        input_delta.y = Input.GetAxis("Vertical");

        sprinting = Input.GetKey(KeyCode.LeftShift);

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
}
