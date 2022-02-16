using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class TowerG : MonoBehaviour
{
    private float _healt = 100f;
    private float _speed = 7f;
    public TowerType MyType;
    public string MyString;
    public List<NpcShotController> MyShooters = new List<NpcShotController>();
    public List<GameObject> TowerPiace = new List<GameObject>();
    public GameObject DestroyObj;
    public GameObject MainObject;
    public float Healt
    {
        get
        {
            return _healt;
        }
        set
        {
            if (Healt == value)
            {
                return;
            }
            _healt = value;
            HealtChanhed();
        }
    }


    private void HealtChanhed()
    {
        if (Healt <= 0)
        {
            MainObject.SetActive(false);
            DestroyObj.SetActive(true);
            Destroy(gameObject, 2f);
            //Destroy(gameObject);
            //transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
            //{
            //});
        }
    }
    void Start()
    {
        Debug.Log(GameManager.Instance.UIManager.AnswerInput.text);
        TowerSpawnScale();
        foreach (var item in TowerPiace)
        {
            item.GetComponentInChildren<TextMeshPro>().text = MyString[TowerPiace.IndexOf(item.gameObject)].ToString().ToUpper();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -_speed * Time.deltaTime);
    }

    public void ShooterPosSet()
    {
        if (MyShooters.Count > 0)
        {
            foreach (var item in MyShooters)
            {
                item.transform.localPosition = new Vector3(item.transform.localPosition.x, 2.2f, item.transform.localPosition.z);
            }
        }

    }

    private void TowerSpawnScale()
    {
        StartCoroutine(TowerSpawnTimer());
    }

    private IEnumerator TowerSpawnTimer()
    {
        Vector3 startScale = new Vector3(0, 0, 0);
        foreach (var item in TowerPiace)
        {
            startScale = item.gameObject.transform.localScale;
            item.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        foreach (var item in TowerPiace)
        {
            item.gameObject.transform.DOScale(startScale, 0.1f);
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }
}
