using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerShip : MonoBehaviour
{
    // References
    [Header("Ship Movement")]
    private Vector2 input_delta;
    public float ship_mass = 5f;
    public float ship_drag = 1.5f;
    public float max_movement_speed = 5f, movement_speed_multiplier = 1.5f, rot_speed;
    private Vector2 ship_movement;
    
    // Stats
    [Header("Ship Stats")]
    public int max_health = 100;
    [HideInInspector] public int current_health;
    // Flags
    public bool can_move = true;

    //Landing
    [Header("Ship Landing")]
    private Planet landing_target = null;
    private bool planet_in_range = false;
    public float detection_radius = 1f;
    private CircleCollider2D col;


    [Header("Ship Sprite (unused)")] // Copied structure from the player script for future usage
    public List<Sprite> n_sprites;
    public List<Sprite> ne_sprites;
    public List<Sprite> e_sprites;
    public List<Sprite> se_sprites;
    public List<Sprite> s_sprites;

    public List<Sprite> sprite_set;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<CircleCollider2D>();
        
        col.radius = detection_radius;
    }

    void Start()
    {
        current_health = max_health;
    }

    void Update()
    {
        input_delta.x = Input.GetAxis("Horizontal");
        input_delta.y = Input.GetAxis("Vertical");

        // Check if a planet is close enough to land on
        if (landing_target != null)
        {
            planet_in_range = ((landing_target.transform.position - transform.position).sqrMagnitude < landing_target.transform.localScale.sqrMagnitude + detection_radius)
            ? planet_in_range = true : planet_in_range = false;
        }

        if (planet_in_range)
            PromptLanding();

        if (can_move)
            Move();
            Rotate();
    }

    // Movement: Placeholder, your take on the character movement was great so I left lots of room for improvement
    void Move() // Also enable the slowdown if the opposite input is detected? Or keep it for the stereotypical "annoying controls sci-fi drifting damn my ship feels heavy" feeling?
    {
        if ( input_delta.sqrMagnitude > 0.01f)
        {
            Vector2 direction = input_delta.y * transform.right;

            // Calculate acceleration
            float acceleration = (max_movement_speed / ship_mass) * movement_speed_multiplier;

            // Increment movement speed by acceleration
            ship_movement += acceleration * Time.deltaTime * direction;

            // Clamp movement speed to max_movement_speed on both axis
            ship_movement.x = Mathf.Clamp(ship_movement.x, -max_movement_speed, max_movement_speed);
            ship_movement.y = Mathf.Clamp(ship_movement.y, -max_movement_speed, max_movement_speed);
        }
        else
        {
            // Gradually slow the ship down when there's no input
            ship_movement = Vector2.Lerp(ship_movement, Vector2.zero, ship_drag * Time.deltaTime);
        }

        transform.position += (Vector3)ship_movement * Time.deltaTime;
    }

    void Rotate()
    {
        transform.Rotate(0, 0, input_delta.x * rot_speed*Time.deltaTime, Space.Self);
    }

    // Landing
    void OnTriggerEnter2D(Collider2D collision) // Detect impact, place planets so that the "landing radius" of them won't intersect
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            print("Landing target set!");
            landing_target = collision.gameObject.GetComponent<Planet>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            print("Landing target out of range!");
            landing_target = null;
        }
    }

    void PromptLanding()
    {
        if (landing_target == null)
            return;

        // Show UI button hovering around the planet's position?
        print($"Press 'F' to land on {landing_target.planet_name ?? "undefined_planet"}");
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!string.IsNullOrEmpty(landing_target.planet_scene))
            {
                SceneManager.LoadScene(landing_target.planet_scene);
            }
            else
            {
                Debug.LogError($"Scene name (planet_scene) for planet \"{landing_target.planet_name}\" is not set!");
            }
        }
    }

    // Debug
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detection_radius);
    }
}
