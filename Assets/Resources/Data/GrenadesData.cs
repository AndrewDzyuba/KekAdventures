using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "GrenadesData", menuName = "ScriptableObjects/GrenadesData", order = 1)]
public class GrenadesData : ScriptableObject
{
    public List<GrenadeData> GrenadesDatas { get { return _grenadesDatas; } }
    [SerializeField] private List<GrenadeData> _grenadesDatas = new List<GrenadeData>();

    public GrenadeData GetGrenadeData(GrenadeType type)
    {
        return GrenadesDatas.Find(x => x.type == type);
    }

    [Serializable]
    public class GrenadeData
    {
        public GrenadeType type;
        public Material material;
        public Sprite icon;
    }
}