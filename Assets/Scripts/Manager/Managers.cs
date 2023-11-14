using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    NetworkManager _network = new NetworkManager();
    MapManager _map = new MapManager();
    ObjManager _obj = new ObjManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();

    public static NetworkManager Network { get { return Instance._network; } }
    public static MapManager Map { get { return Instance._map; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static ObjManager Obj { get { return Instance._obj; } }
    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._pool.Init();
            s_instance._network.Init();
            s_instance._map.Init();
            s_instance._obj.Init();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _network.Update();
    }
}
