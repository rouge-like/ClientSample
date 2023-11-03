using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    public int Id { get; set; }
    StatInfo _stat = new StatInfo();
    public StatInfo Stat
    {
        get { return _stat; }
        set { 
            if (_stat.Equals(value))
                return;

            _stat.MergeFrom(value);
        }
    }
    public float Speed { get { return Stat.Speed; } set { Stat.Speed = value; } }
    public virtual int Hp { get { return Stat.Hp; } set { Stat.Hp = value; } }

    PosInfo _posInfo = new PosInfo(); 
    protected bool _updated = false;
    public PosInfo PosInfo
    {
        get { return _posInfo; }
        set
        {
            if (_posInfo.Equals(value))
                return;
            Pos = new Vector3Int(value.PosX, 0, value.PosY);
            State = value.State;
            Dir = value.Dir;
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
            UpdateAnim();
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
            UpdateAnim();
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
            UpdateAnim();
            _updated = true;
        }
    }

    protected Animator _animator;
    protected virtual void UpdateAnim()
    {
        if (_animator == null)
            return;

        if(State == State.Idle)
        {
            switch (Dir)
            {
                case Dir.Up:
                    break;
                case Dir.Down:
                    break;
                case Dir.Right:
                    break;
                case Dir.Left:
                    break;
                case Dir.Upright:
                    break;
                case Dir.Upleft:
                    break;
                case Dir.Downright:
                    break;
                case Dir.Downleft:
                    break;
            }
            _animator.CrossFade("IDLE", 0.1f);
        }
        else if(State == State.Moving)
        {
            switch (Dir)
            {
                case Dir.Up:
                    break;
                case Dir.Down:
                    break;
                case Dir.Right:
                    break;
                case Dir.Left:
                    break;
                case Dir.Upright:
                    break;
                case Dir.Upleft:
                    break;
                case Dir.Downright:
                    break;
                case Dir.Downleft:
                    break;
            }
            _animator.CrossFade("RUN", 0.1f);
        }
        else if(State == State.Dead)
        {
            _animator.CrossFade("ONDEAD", 0.1f);
        }
    }

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();

        if(_animator == null)
            _animator = GetComponentInChildren<Animator>();
    } 
    void Start()
    {
        Init();
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
            case State.Dead:
                UpdateDead();
                break;
        }
    }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateDir()
    {         

    }

    protected virtual void UpdateMoving()
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }
    protected virtual void UpdateDead()
    {

    }
}
