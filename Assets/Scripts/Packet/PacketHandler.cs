using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PacketHandler
{
	public static void S_EnterGameHandler(PacketSession session, IMessage packet)
	{
		S_EnterGame enterGamePacket = packet as S_EnterGame;
		ServerSession serverSession = session as ServerSession;

		Managers.Obj.Add(enterGamePacket.Player, true);
	}
	public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	{
		S_LeaveGame leaveGamePacket = packet as S_LeaveGame;
		ServerSession serverSession = session as ServerSession;

	}
	public static void S_SpawnHandler(PacketSession session, IMessage packet)
	{
		S_Spawn spawnPacket = packet as S_Spawn;
		ServerSession serverSession = session as ServerSession;

		foreach(ObjectInfo info in spawnPacket.Objects)
        {
			Managers.Obj.Add(info);
        }
	}
	public static void S_DespawnHandler(PacketSession session, IMessage packet)
	{
		S_Despawn despawnPacket = packet as S_Despawn;
		ServerSession serverSession = session as ServerSession;

		foreach(int id in despawnPacket.ObjectIds)
        {
			Managers.Obj.Remove(id);
			//Debug.Log($"Destroy : {id}");
        }
	}
	public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
		S_Move movePacket = packet as S_Move;
		ServerSession serverSession = session as ServerSession;

		GameObject go = Managers.Obj.Find(movePacket.ObjectId);
		if (go == null)
			return;

		ObjController oc = go.GetComponent<ObjController>();

		oc.PosInfo = movePacket.PosInfo;

		//Debug.Log($"{movePacket.ObjectId} : {oc.PosInfo} , {movePacket.PosInfo}");
    }
	public static void S_SkillHandler(PacketSession session, IMessage packet)
	{
		S_Skill skillPacket = packet as S_Skill;
		ServerSession serverSession = session as ServerSession;

		GameObject go = Managers.Obj.Find(skillPacket.ObjectId);
		if (go == null)
			return;

		PlayerController pc = go.GetComponent<PlayerController>();
		if(pc != null)
        {
			if (pc.State == State.Dead)
				Debug.Log("Bug Call");
			pc.UseSkill(skillPacket.Info.SkillId);
        }

		//Debug.Log($"Skill : {skillPacket.Info.SkillId} , {skillPacket.ObjectId}");
	}

	public static void S_ChangeHpHandler(PacketSession session, IMessage packet)
	{
		S_ChangeHp changePacket = packet as S_ChangeHp;
		ServerSession serverSession = session as ServerSession;

		GameObject go = Managers.Obj.Find(changePacket.ObjectId);
		if (go == null)
			return;

		ObjController oc = go.GetComponent<ObjController>();
		if (oc != null)
		{
			oc.Stat.Hp = changePacket.Hp;

			Debug.Log($"{go.name}'s Hp : {changePacket.Hp}");
		}

	}
	public static void S_PingHandler(PacketSession session, IMessage packet)
	{
		C_Pong pongPakcet = new C_Pong();
		Debug.Log("Ping Check");
		Managers.Network.Send(pongPakcet);
	}
	public static void S_DieHandler(PacketSession session, IMessage packet)
	{
		S_Die die = (S_Die)packet;

		GameObject player = Managers.Obj.Find(die.ObjectId);

		PlayerController pc = player.GetComponent<PlayerController>();
		pc.State = State.Dead;
    }
	public static void S_MoveFloatHandler(PacketSession session, IMessage packet)
	{
		S_MoveFloat s_move = (S_MoveFloat)packet;
		ServerSession serverSession = session as ServerSession;

		GameObject go = Managers.Obj.Find(s_move.ObjectId);
		if (go == null)
			return;
		TrigonController tc = go.GetComponent<TrigonController>();
		tc.Degree = s_move.Degree;
		if(tc.Speed < 0 && s_move.Dir)
		{
			tc.Speed = tc.Speed * -1;
		}
		else if(tc.Speed > 0 && !s_move.Dir)
		{
            tc.Speed = tc.Speed * -1;
        }

	}

	public static void S_HitTrigonHandler(PacketSession session, IMessage packet)
	{
		S_HitTrigon hitTrigon = (S_HitTrigon)packet;

		GameObject go1 = Managers.Obj.Find(hitTrigon.Trigon1Id);

		if (go1 != null)
		{
            TrigonController tc1 = go1.GetComponent<TrigonController>();
			tc1.Hit();
        }
        GameObject go2 = Managers.Obj.Find(hitTrigon.Trigon2Id);
		if (go2 != null)
		{
            TrigonController tc2 = go2.GetComponent<TrigonController>();
			tc2.Hit();
        }
    }
	public static void S_EquipHandler(PacketSession session, IMessage packet)
	{
		S_Equip equip = (S_Equip)packet;

    }
}
