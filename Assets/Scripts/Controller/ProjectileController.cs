using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;

public class ProjectileController : ObjController
{
    protected override void Init()
    {
        switch (Dir)
        {
			case Dir.Up:
				transform.rotation = Quaternion.Euler(90, 0, 0);
				break;
			case Dir.Down:
				transform.rotation = Quaternion.Euler(-90, 0, 0);
				break;
			case Dir.Left:
				transform.rotation = Quaternion.Euler(0, 0, -90);
				break;
			case Dir.Right:
				transform.rotation = Quaternion.Euler(0, 0, 90);
				break;

		}
        base.Init();
    }

    protected override void UpdateMoving()
    {
        Vector3 dir = Pos - transform.position;
        float dist = dir.magnitude;
        if (dist < Speed * Time.deltaTime)
        {
            transform.position = Pos;
            UpdateDir();
        }
        else
        {
            transform.position += Speed * dir.normalized * Time.deltaTime;
        }
    }

}
