using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : ObjController
{
    protected override void CUpdate()
    {
        // Rotation
        transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
    }
}
