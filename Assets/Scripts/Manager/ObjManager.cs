using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager
{
    public PlayerController MyPlayer { get; set; }
    Dictionary<int, GameObject> _objs = new Dictionary<int, GameObject>();

    public void Init()
    {

    }

    public void Add(ObjectInfo info, bool myPlayer = false)
    {
        if (myPlayer)
        {
            GameObject go = Resources.Load<GameObject>("MyCube");
            go = Object.Instantiate(go);
            go.name = info.Name;

            _objs.Add(info.ObjectId, go);

            MyPlayer = go.GetComponent<PlayerController>();
            MyPlayer.Id = info.ObjectId;
            MyPlayer.PosInfo = info.PosInfo;
        }
        else
        {
            GameObject go = Resources.Load<GameObject>("Cube");
            go = Object.Instantiate(go);
            go.name = info.Name;

            _objs.Add(info.ObjectId, go);

            ObjController oc = go.GetComponent<ObjController>();
            oc.Id = info.ObjectId;
            oc.PosInfo = info.PosInfo;
            go.transform.position = oc.Pos;
        }
        Managers.Map.Add(new Vector3Int(info.PosInfo.PosX, 0, info.PosInfo.PosY), info.ObjectId);
    }
    public void Add(GameObject go, int id)
    {
        _objs.Add(id, go);
    }
    
    public void Remove(int id)
    {
        GameObject go = null;
        _objs.TryGetValue(id, out go);
        Object.Destroy(go);

        _objs.Remove(id);
    }

    public GameObject Find(int id)
    {
        GameObject go = null;
        _objs.TryGetValue(id, out go);

        return go;
    }
}
