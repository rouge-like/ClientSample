using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    public int Id { get; set; }

    PosInfo _posInfo = new PosInfo(); 
    protected bool _updated = false;
    public PosInfo PosInfo
    {
        get { return _posInfo; }
        set
        {
            if (_posInfo.Equals(value))
                return;
            _posInfo = value;
        }
    }
    public Vector3Int Pos {
        get
        {
            return new Vector3Int(PosInfo.PosX, 0, PosInfo.PosY);
        }
        set
        {
            PosInfo.PosX = value.x;
            PosInfo.PosY = value.z;

            _updated = true;
        }
    }
    public Dir Dir
    {
        get { return PosInfo.Dir; }
        set
        {
            if (PosInfo.Dir == value)
                return;
            PosInfo.Dir = value;

            _updated = true;
        }
    }
    public State State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;
            PosInfo.State = value;

            _updated = true;
        }
    }


    public float _speed;

    void Start()
    {
        
    }
    void Update()
    {
        CUpdate();
    }
    protected virtual void CUpdate()
    {
        switch (State)
        {
            case State.Idle:
                transform.position = Pos;
                UpdateIdle();
                break;
            case State.Moving:
                UpdateMoving();
                break;
            //
        }
    }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateDir()
    {         

    }

    void UpdateMoving()
    {
        Vector3 dir = Pos - transform.position;
        float dist = dir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = Pos;
            UpdateDir();
        }
        else
        {
            transform.position += _speed * dir.normalized * Time.deltaTime;
        }
    }
    void UpdateAttack()
    {

    }
}
