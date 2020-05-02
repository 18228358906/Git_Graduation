﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//保存每一波敌人生成所需的属性
[System.Serializable]
public class Wave 
{
    public GameObject enemyPrefab;
    public int count;
    public float rate;
    public float PhysicDefence;
    public float MagicDefence;
}
