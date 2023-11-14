using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class TrigonController : ObjController
{
    public Transform Center;
    public Transform Stick;
    public float R;
    public float Degree;
    float _randY;

    protected override void Init()
    {
        R = 3.0f;
        _randY = Random.Range(0, 0.7f);
    }

    void Update()
    {
        Degree += Time.deltaTime * Speed;

        var rad = Mathf.Deg2Rad * Degree;
        var x = R * Mathf.Cos(rad);
        var y = R * Mathf.Sin(rad);

        transform.position = Center.position + new Vector3(x, 0, y);

        Stick.rotation = Quaternion.Euler(0,-Degree, 0);
        Stick.position = Center.position + new Vector3(0, _randY, 0);

        if (Degree > 360)
            Degree -= 360;

        if (Degree < 0)
            Degree += 360;
    }
    public void Hit()
    {
        Debug.Log($"Hit");
        Speed *= -1;
        Degree += Time.deltaTime * Speed;
    }
    public override void OnDamaged()
    {
    }
    private void OnDestroy()
    {
        if(Stick != null)
            Destroy(Stick.gameObject);
    }
}
