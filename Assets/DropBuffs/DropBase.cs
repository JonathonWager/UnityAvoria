using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DropBuffs{
    public abstract class DropBase : ScriptableObject {
        public string name;
        public string description;
        public Sprite icon;
        public GameObject drop;

        public int tier;
        public float dropTime;

        public abstract void Drop(GameObject enemy);
    }
}