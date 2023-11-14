using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    StatInfo _stat;
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        _stat = transform.parent.GetComponent<ObjController>().Stat;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.Hp / (float)_stat.MaxHp;
        //Debug.Log(ratio);
        SetHpRatio(ratio);
    }
    public void SetHpRatio(float r)
    {
        GetComponentInChildren<Slider>().value = r;
    }
}
