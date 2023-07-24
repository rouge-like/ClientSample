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
