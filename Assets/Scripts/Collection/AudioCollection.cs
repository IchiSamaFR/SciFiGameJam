using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollection : MonoBehaviour
{
    [System.Serializable]
    public struct prefabObj
    {
        public string id;
        public AudioClip audioClip;
    }

    public static AudioCollection instance;

    public List<prefabObj> prefabList = new List<prefabObj>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Instance déjà crée, il ne peut y avoir 2 CollectionManager.");
            return;
        }
        instance = this;
    }

    public AudioClip GetAudio(string id)
    {
        foreach (prefabObj item in prefabList)
        {
            if (item.id == id)
            {
                return item.audioClip;
            }
        }
        return null;
    }
}
