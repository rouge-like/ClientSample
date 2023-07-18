using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjController
{
    bool _moveKeyPressed = false;
    protected override void CUpdate()
    {
        UpdateInput();
        base.CUpdate();
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, 10, transform.position.z - 10);
    }

    protected override void UpdateIdle()
    {
        if(_moveKeyPressed)
        {
            State = State.Moving;
            return;
        }
    }
    protected override void UpdateDir()
    {
        if (_moveKeyPressed == false)
        {
            State = State.Idle;
            SendPacket();
            return;
        }
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

        if (Managers.Map.setPos(desPos, Id))
        {
            Pos = desPos;
            SendPacket();
        }
    }
    void SendPacket()
    {
        if (_updated)
        {
            C_Move move = new C_Move();
            move.PosInfo = new PosInfo()
            {
                Dir = Dir,
                State = State,
                PosX = Pos.x,
                PosY = Pos.z
            };

            Managers.Network.Send(move);
            _updated = false;
        }
    }
    void UpdateInput()
    {
        _moveKeyPressed = true;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Dir = Dir.Up;
            if (Input.GetKey(KeyCode.RightArrow)){
                Dir = Dir.Upright;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)){
                Dir = Dir.Upleft;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Dir = Dir.Down;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Dir = Dir.Downright;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Dir = Dir.Downleft;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Dir = Dir.Right;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Dir = Dir.Upright;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Dir = Dir.Downright;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Dir = Dir.Left;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Dir = Dir.Upleft;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Dir = Dir.Downleft;
            }
        }
        else
        {
            _moveKeyPressed = false;
        }
    }
}
