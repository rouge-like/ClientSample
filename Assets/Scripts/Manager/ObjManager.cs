using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager
{
    public MyPlayerController MyPlayer { get; set; }
    Dictionary<int, GameObject> _objs = new Dictionary<int, GameObject>();

    public void Init()
    {

    }
    public static GameObjectType GetObjectTypeById(int id)
    {
        int type = (id >> 24) & 0x7F;
        return (GameObjectType)type;
    }

    public void Add(ObjectInfo info, bool myPlayer = false)
    {
        if (MyPlayer != null && MyPlayer.Id == info.ObjectId)
            return;
        if (_objs.ContainsKey(info.ObjectId))
            return;

        GameObjectType type = GetObjectTypeById(info.ObjectId);
        if (type == GameObjectType.Player)
        {
            if (myPlayer)
            {
                GameObject go = Resources.Load<GameObject>("MyPlayer");
                GameObject particle = Resources.Load<GameObject>("ParticleFire");
                go = Object.Instantiate(go);
                particle = Object.Instantiate(particle);
                go.name = info.Name;
                particle.name = info.Name + "_fire";

                _objs.Add(info.ObjectId, go);

                MyPlayer = go.GetComponent<MyPlayerController>();
                MyPlayer.Id = info.ObjectId;
                MyPlayer.PosInfo = info.PosInfo;
                MyPlayer.Stat = info.StatInfo;

                ParticleController pc = particle.GetComponent<ParticleController>();
                pc._parentGo = go;
            }
            else
            {
                GameObject go = Resources.Load<GameObject>("Player");
                go = Object.Instantiate(go);
                go.name = info.Name;

                _objs.Add(info.ObjectId, go);

                PlayerController pc = go.GetComponent<PlayerController>();
                pc.Id = info.ObjectId;
                pc.PosInfo = info.PosInfo;
                pc.Stat = info.StatInfo;
                go.transform.position = pc.Pos;
            }
        }
        else if(type == GameObjectType.Projectile)
        {
            GameObject go = Resources.Load<GameObject>("Cylinder");
            go = Object.Instantiate(go);
            go.name = info.Name;

            _objs.Add(info.ObjectId, go);

            ProjectileController pc = go.GetComponent<ProjectileController>();
            pc.Id = info.ObjectId;
            pc.PosInfo = info.PosInfo;
            pc.Stat = info.StatInfo;
            go.transform.position = pc.Pos;
        }

        Managers.Map.Add(new Vector3Int(info.PosInfo.PosX, 0, info.PosInfo.PosY), info.ObjectId);
    }
    public void Add(GameObject go, int id)
    {
        _objs.Add(id, go);
    }
    
    public void Remove(int id)
    {
        if (MyPlayer != null && MyPlayer.Id == id)
            return;
        if (_objs.ContainsKey(id) == false)
            return;

        GameObject go = null;
        _objs.TryGetValue(id, out go);
        Object.Destroy(go);

        _objs.Remove(id);

        Managers.Map.Remove(id);
    }

    public GameObject Find(int id)
    {
        GameObject go = null;
        _objs.TryGetValue(id, out go);

        return go;
    }
}
