using System.Linq;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal;

    public Vector3 Project(Vector3 forward, float _speed)
    {
        return forward * _speed;
    }

    // private void OnCollisionEnter(Collision collision) {
    //     _normal = collision.contacts[0].normal;
    // }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.white;
    //     Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward, 1f));
    // }
}
