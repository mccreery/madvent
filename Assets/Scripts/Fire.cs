using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab = null;

    [SerializeField]
    private float cooldown = 0.1f;

    [SerializeField]
    private float force = 1000;

    [SerializeField]
    private float pitchIncrease = 20;

    [SerializeField]
    private float startDistance = -5;

    [SerializeField]
    private AudioClip fireSound = null;

    [SerializeField]
    private Vector3 spin;

    private new Camera camera;
    private AudioSource source;
    private void Start()
    {
        camera = Camera.main;
        source = GetComponent<AudioSource>();
    }

    private float timeSinceSpawn;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn >= cooldown)
            {
                Spawn(prefab, force);
                source.PlayOneShot(fireSound);
                timeSinceSpawn = 0;
            }
        }
        else
        {
            timeSinceSpawn = cooldown;
        }
    }

    private GameObject Spawn(GameObject prefab, float force)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        ray.direction = Quaternion.AngleAxis(pitchIncrease, Vector3.left) * ray.direction;

        GameObject spawned = Instantiate(prefab, ray.GetPoint(startDistance), Random.rotation);
        Rigidbody rigidbody = spawned.GetComponent<Rigidbody>();
        rigidbody.AddForce(ray.direction * force);
        rigidbody.AddTorque(spin, ForceMode.Impulse);

        return spawned;
    }
}
