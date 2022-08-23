using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private float rotationPower;
    [SerializeField, Range(0.1f, 15f)] private float shootRange; //дальность
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform shootForcePoint;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float shootTime;
    private float shootTimeLeft;
    public static bool canShoot = true;

    public UnityEvent onFire;

    private Rigidbody rb;
    private void Awake() => rb = GetComponent<Rigidbody>();
    private void Update()
    {                                                                                          
        shootTimeLeft -= Time.deltaTime;                                                       
        if(Input.GetMouseButton(0) && shootTimeLeft <= 0)                                      
        {
            shootTimeLeft = shootTime;
            if(canShoot) Fire();
            RaycastHit hit;
            Physics.Raycast(shootPoint.position, transform.forward * shootRange, out hit);
            if(hit.collider != null)
            {
                if(hit.collider.TryGetComponent<Bot>(out Bot bot) || hit.collider.TryGetComponent<FinishBlock>(out FinishBlock finishBlock))
                {
                    GameManager.SlowMoOn();
                }
            }
        }
    }
    public static void ChangeShootState(bool state) => canShoot = state;
    private void Fire()
    {
        onFire.Invoke();
        Instantiate(bullet, shootPoint.position, shootPoint.transform.rotation);
        Knockback();
    }
    private void Knockback()
    {
        var force = -transform.forward * knockbackPower;
        rb.AddExplosionForce(rotationPower, shootForcePoint.position, 0.1f, 2, ForceMode.Impulse);
        rb.AddForceAtPosition(force, shootForcePoint.position , ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Lose") && GameManager.instance.state == GameManager.GameState.PLAY) GameManager.instance.Lose();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawRay(shootPoint.position, transform.forward * shootRange);
    }
}
