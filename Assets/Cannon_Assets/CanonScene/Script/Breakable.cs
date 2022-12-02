using UnityEngine;
public class Breakable : MonoBehaviour {
    public GameObject _replacement;
    public float _breakForce = 2;
    public float _collisionMultiplier = 100;
    bool _broken;

 
    void OnCollisionEnter(Collision collision) {
        if(_broken) return;
        if (collision.relativeVelocity.magnitude >= _breakForce) {
            _broken = true;
            var replacement = Instantiate(_replacement, transform.position, transform.rotation);
            replacement.tag = "Clone";
            var rbs = replacement.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rbs) {
                rb.AddExplosionForce(collision.relativeVelocity.magnitude * _collisionMultiplier,collision.contacts[0].point,2);
            }
 
            Destroy(gameObject);
        }
    }
}