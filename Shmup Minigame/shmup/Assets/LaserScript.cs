using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript: MonoBehaviour
{
    
   public static void ShootLaser(GameObject prefab, GameObject shooter, float f)
   {
        GameObject laser;
        laser = Instantiate(prefab, shooter.transform.position, shooter.transform.rotation);
        laser.GetComponent<Rigidbody2D>().AddForce(laser.transform.up * f);
        Destroy(laser, 1f);
        
   }

}
