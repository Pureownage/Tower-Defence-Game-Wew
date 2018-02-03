using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour
{

    Transform BarrelLook;
    Vector3 HoldTarget;
    Vector3 TargetPos;
    public int LockOnRange = 10;

    float Range = 10f;
    public GameObject BulletGameObject;

    float FireRate = 0.5f;
    float FireRateCooldown = 0f;
    
    // Use this for initialization
    void Start()
    {
        // Here we just do some basic bookkeeping. Setting up the targets and pointing at them
        /* TO DO: Do we still need this? We do it in the update anyway
         * We still need the barrellook transform stuff so we set the transform (Or should we do it during decleration?) 
         * But I don't think we need anything else really. Everything is done in the update which is called after start.
         * Need to do some testing to find out.
         */
        BarrelLook = transform.Find("Barrel");
        EnemyMovement[] Enemys = GameObject.FindObjectsOfType<EnemyMovement>(); 
        EnemyMovement Target = LocateTarget(Enemys);
        TargetPos = Target.transform.position;
        HoldTarget = TargetPos;
        print("Found Starting Target");
        //print(TargetPos);
        BarrelLook.transform.LookAt(TargetPos);
        BarrelLook.transform.Rotate(new Vector3(1.0f, 0, 0), 90);
    }

    // Update is called once per frame
    void Update()
    {
        // Grab a list of the current enemys which are currently in the gameworld
        EnemyMovement[] Enemys = GameObject.FindObjectsOfType<EnemyMovement>();
        // Find the closest enemy and make them our current target
        EnemyMovement Target = LocateTarget(Enemys);
        print("Finding Target");
        // If there is no valid target to shoot, reset the barrel to look forward and end this update.
        if(Target == null)
        {
            BarrelLook.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
            return;
        }
        // Otherwise, we wanna look at the target and shoot them.
        TargetPos = Target.transform.position;
        
        
        // Here we want to look at our current targets position
        BarrelLook.transform.LookAt(TargetPos);
        BarrelLook.transform.Rotate(new Vector3(1.0f, 0, 0), 90);
        // And now we fire. Frist we countdown our firerate cooldown, so we don't shoot too quickly.
        FireRateCooldown = FireRateCooldown - Time.deltaTime;
        // Next, we need to find the direction to look at, we use this to work out the range.
        Vector3 Direction = Target.transform.position - this.transform.position;
        // If the target out of range, reset
        if (Direction.magnitude >= Range)
        {
            BarrelLook.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
            return;
        }
        // Otherwise, fire at the target! We check to see if we are out of our firerate cooldown and if we are still in range.
        // TO DO: Do I still need to check if I am in range here? I just did it before. Need to check to make sure it doesn't do anything funky.
        if (FireRateCooldown <= 0 && Direction.magnitude<= Range)
        {
            FireRateCooldown = FireRate;
            ShootAt(Target);
           // print("Firing!");
        }
    }

    void ShootAt(EnemyMovement E)
    {
        // Here we shoot at the locked target. We create a new bullet and give it itself, this position and this rotation so it knows where it is in relation to the turret.
        GameObject BulletFire = (GameObject)Instantiate(BulletGameObject, this.transform.position, this.transform.rotation);

        Bullet B = BulletFire.GetComponent<Bullet>();
        B.Target = E.transform;

    }

    EnemyMovement LocateTarget(EnemyMovement[] Enemys)
    {
        EnemyMovement CurrentClosest = null;
        float Min = Mathf.Infinity;
        foreach (EnemyMovement X in Enemys)
        {
            // Work out what is the closest enemy, and set them as the target
            float Distance = Vector3.Distance(X.transform.position, this.transform.position);
            if (Distance < Min)
            {
                CurrentClosest = X;
                Min = Distance;
            }
        }
        return CurrentClosest;
    }



}