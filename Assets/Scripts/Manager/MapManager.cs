using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager
{
    int[,] _map;
    int _lenX;
    int _lenY;
    Dictionary<int, Vector3Int> _objs;
    public int[,] Map { get { return _map; } } 

    public void Init()
    {        
        TextAsset txt = Resources.Load("map") as TextAsset;
        StringReader reader = new StringReader(txt.text);
        _lenX = int.Parse(reader.ReadLine());
        _lenY = int.Parse(reader.ReadLine());
        _map = new int[_lenX, _lenY];
        for (int y = 0; y < _lenY; y++)
        {
            string line = reader.ReadLine();
            for (int x = 0; x < _lenX; x++)
            {
                _map[x, y] = (int)Char.GetNumericValue(line[x]);
                if (_map[x, y] == 1)
                {
                    GameObject go = Resources.Load<GameObject>("tree");
                    go = UnityEngine.Object.Instantiate(go);
                    go.transform.position = new Vector3Int(x,0,y);
                }
            }
        }

        _objs = new Dictionary<int, Vector3Int>();
    }
    public void Add(Vector3Int pos, int id)
    {
        _objs.Add(id, pos);
    }

    public void Remove(int id)
    {
        _objs.Remove(id);
    }
    public bool setPos(Vector3Int pos, int id)
    {
        int x = pos.x;
        int y = pos.z;

        Vector3Int preVec = _objs[id];
        if (x < 0 || y < 0 || x > _lenX - 1 || y > _lenY - 1)
            return false;

        if (_map[x, y] == 0)
        {
            int preX = preVec.x;
            int preY = preVec.z;
            _objs[id] = pos;
            _map[preX, preY] = 0;
            _map[x, y] = id;
            return true;
        }
        else
            return false;
    }
    public void returnPos(int id)
    {
        Debug.Log($"{_objs[id]}");
    }
}
