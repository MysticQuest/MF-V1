using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RaycastAbility")]
public class RaycastAbility : Ability
{
    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    //private RayCastShootTriggerable rcShoot;

    public override void Initialize(GameObject obj)
    {
        /*  rcShoot = obj.GetComponent<RayCastShootTriggerable>();
            rcShoot.Initialize();

            rcShoot.gunDamage = gunDamage;
            rcShoot.weaponRange = weaponRange;
            rcShoot.hitForce = hitForce;
         */
    }

    public override void TriggerAbility()
    {
        // rcShoot.Fire();
    }

}
