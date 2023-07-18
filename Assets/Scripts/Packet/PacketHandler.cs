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

		//Debug.Log($"");
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

		foreach(int id in despawnPacket.PlayerIds)
        {
			Managers.Obj.Remove(id);
        }
	}
	public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
		S_Move movePacket = packet as S_Move;
		ServerSession serverSession = session as ServerSession;

		GameObject go = Managers.Obj.Find(movePacket.ObjectId);

		ObjController oc = go.GetComponent<ObjController>();

		oc.PosInfo = movePacket.PosInfo;

		Debug.Log($"{movePacket.ObjectId} : {oc.PosInfo} , {movePacket.PosInfo}");
    }
	public static void S_SkillHandler(PacketSession session, IMessage packet)
	{
		S_Skill enterGamePacket = packet as S_Skill;
		ServerSession serverSession = session as ServerSession;
	}
}
