using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;

public class PlayerController : ObjController
{
    protected Coroutine _coSkill;

    protected override void Init()
    {
        base.Init();
    }
    protected override void UpdateIdle()
    {
        Vector3Int desPos = Pos;
        switch (Dir)
        {
            case Dir.Up:
                desPos += new Vector3Int(0, 0, 1);
                break;
            case Dir.Down:
                desPos += new Vector3Int(0, 0, -1);
                break;
            case Dir.Right:
                desPos += new Vector3Int(1, 0, 0);
                break;
            case Dir.Left:
                desPos += new Vector3Int(-1, 0, 0);
                break;
            case Dir.Upright:
                desPos += new Vector3Int(1, 0, 1);
                break;
            case Dir.Upleft:
                desPos += new Vector3Int(-1, 0, 1);
                break;
            case Dir.Downright:
                desPos += new Vector3Int(1, 0, -1);
                break;
            case Dir.Downleft:
                desPos += new Vector3Int(-1, 0, -1);
                break;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desPos - transform.position), 20 * Time.deltaTime);
    }
    protected override void UpdateAnim()
    {
        base.UpdateAnim();
        if (State == State.Skill)
            _animator.CrossFade("ATTACK", 0.1f);

    }

    public void UseSkill(int skillId)
    {
        if(skillId == 1)
        {

        }
        else if(skillId == 2)
        {
            _coSkill = StartCoroutine("CoSkill2");
        }
    }
    protected virtual void SendPacket()
    {

    }
    IEnumerator CoSkill2()
    {
        State = State.Skill;
        yield return new WaitForSeconds(0.5f);
        State = State.Idle;
        _coSkill = null;
        SendPacket();
    }
}
