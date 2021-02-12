using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : EntityStats
{
    [System.Serializable]
    public struct RessourcesDroped
    {
        public string id;
        public int amount;
    }


    [Header("AI Stats")]
    [SerializeField]
    private float rangeAttack = 7;
    [SerializeField]
    private float rangeDetection = 12;
    [SerializeField]
    private SphereCollider rangeCollider;
    [SerializeField]
    private int dmg = 1;
    [SerializeField]
    private int fireRate = 1;



    [Header("Drop on death")]
    [SerializeField]
    private bool drop = true;
    [SerializeField]
    private List<RessourcesDroped> ressourcesToDrop = new List<RessourcesDroped>();

    #region Getter

    public float RangeAttack { get => rangeAttack; set => rangeAttack = value; }
    public int Dmg { get => dmg; set => dmg = value; }
    public int FireRate { get => fireRate; set => fireRate = value; }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = rangeDetection;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRegen();
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();

        CreateDrop();

        GameObject obj = Instantiate(PrefabCollection.instance.GetPrefab("fxAudio"));
        obj.transform.position = transform.position;
        obj.GetComponent<FXAudio>().Set(AudioCollection.instance.GetAudio("explosion"));

        Destroy(gameObject);
    }

    void CreateDrop()
    {
        Vector3 pos = transform.position;

        for (int i = 0; i < ressourcesToDrop.Count; i++)
        {
            RessourcesDroped item = ressourcesToDrop[i];
            int amount = item.amount;

            while (amount > 0)
            {
                Item _item = ItemCollection.instance.GetItem(item.id);
                if(_item == null)
                {
                    return;
                }

                GameObject _obj = Instantiate(_item.DropPrefab);
                _obj.GetComponent<Ressource>().Item = _item;
                amount = _obj.GetComponent<Ressource>().Item.AddAmount(amount);
                _obj.transform.position = new Vector3(Random.Range(pos.x - 0.5f, pos.x + 0.5f),
                                                      0,
                                                      Random.Range(pos.z - 0.5f, pos.z + 0.5f));
            }
        }
    }

}
