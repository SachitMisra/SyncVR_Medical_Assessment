using UnityEngine;
public class Explosive : MonoBehaviour {
    public float _triggerForce = 0.5f;
    public float _explosionRadius = 5;
    public float _explosionForce = 500;
    public GameObject vfx;
    void Start()
    {
        vfx.SetActive(false);
    }
 
    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude >= _triggerForce) {
            var surroundingObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
 
            foreach (var obj in surroundingObjects) {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) continue;
 
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius,1);
            }
            vfx.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}