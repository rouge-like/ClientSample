using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;

public class MonsterController : ObjController
{
    protected override void Init()
    {
        base.Init();

        State = State.Idle;
        Dir = Dir.Down;
    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }
}
