using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float BulletSpeed = 1f;
    public float Damage = 1f;
    public Transform Target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Direction we need to travel into to hit the target this frame
        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 Direction = Target.position - this.transform.localPosition;
        // How far we are going to travel to hit the target. This is done so we can pause time if needs be.
        float DistanceThisFrame = BulletSpeed * Time.deltaTime;
        // Have we hit the target? Lets find out if the magnitude is less than the distance we are going to travel this frame, we have hit it
        if (Direction.magnitude <= DistanceThisFrame)
        {
            HitTarget();
        }
        else
        // If not, we need to move forward.
        {
            transform.Translate(Direction.normalized * DistanceThisFrame, Space.World);
            Quaternion TargetRotation = Quaternion.LookRotation(Direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, TargetRotation, Time.deltaTime * 5);

        }
	}
    // Here we hit the target, we call the enemy's controller and call their hit function, so they take damage. Damage is set here so if its upgraded we can do it here.
    void HitTarget()
    {
        Target.GetComponent<EnemyMovement>().BeHit(Damage);
        Destroy(gameObject);
    }

}
