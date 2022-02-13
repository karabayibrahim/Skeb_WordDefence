using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManager : ScriptableObject
{
    public GameObject SpawnObject;
    [Header("Towers")]
    public GameObject WoodTower;
    public GameObject StoneTower;
    public GameObject ConcreateTower;
    public GameObject LaserTower;
    public GameObject TowerG;

    private float _pozY;


    public void SpawnObjectMethod()
    {
        float PozX = Random.Range(-6, 6);
        float PozZ = Random.Range(GameManager.Instance.Player.transform.position.z + 50, GameManager.Instance.Player.transform.position.z + 55f);
        var newNpc = Instantiate(SpawnObject, new Vector3(PozX, 0, PozZ), Quaternion.identity);
        newNpc.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void StoneSpawn(int _wordCount, float _pozZ)
    {
        var tower = TowerAssigment(_wordCount);
        float PozX;
        float PozZ = _pozZ;
        if (_wordCount % 2 != 0)
        {
            PozX = _wordCount - 1;

        }
        else
        {
            PozX = _wordCount;
        }
        var myTowerG = Instantiate(TowerG, new Vector3(PozX, _pozY, PozZ), Quaternion.identity);
        for (int i = 0; i < _wordCount / 2; i++)
        {
            if (i == 0)
            {
                var newTower = Instantiate(tower, new Vector3(PozX, _pozY, PozZ), Quaternion.identity,myTowerG.transform);
                StartScaleEffeckt(newTower);
                if (_wordCount % 2 != 0&& _wordCount <= 3f)
                {
                    newTower.GetComponent<Tower>().LeftTower.SetActive(true);
                }
                if (_wordCount <= 3f)
                {
                    newTower.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }

            else
            {
                if ((i == _wordCount / 2 - 1) && _wordCount % 2 != 0)
                {
                    Debug.Log("Tek");
                    var newTower = Instantiate(tower, new Vector3(PozX - (i * 4), _pozY, PozZ), Quaternion.identity, myTowerG.transform);
                    StartScaleEffeckt(newTower);
                    if (_wordCount <= 3f)
                    {
                        newTower.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else
                    {
                        newTower.GetComponent<Tower>().LeftTower.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("Çift");
                    var newTower = Instantiate(tower, new Vector3(PozX - (i * 4), _pozY, PozZ), Quaternion.identity, myTowerG.transform);
                    StartScaleEffeckt(newTower);
                    if (_wordCount <= 3f)
                    {
                        newTower.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }

            }
        }
    }

    private GameObject TowerAssigment(int _wordCount)
    {
        if (_wordCount <= 3)
        {
            PozYAssigment(0.9f);
            return WoodTower;
        }
        else if (_wordCount > 3 && _wordCount < 6)
        {
            PozYAssigment(1.3f);
            return StoneTower;
        }
        else if (_wordCount > 5 && _wordCount < 8)
        {
            PozYAssigment(1f);
            return ConcreateTower;
        }
        else
        {
            PozYAssigment(0.7f);
            return LaserTower;
        }
    }

    private float PozYAssigment(float pozY)
    {
        _pozY = pozY;
        return _pozY;
    }

    private void StartScaleEffeckt(GameObject _obj)
    {
        Vector3 startScale = _obj.transform.localScale;
        _obj.transform.localScale = new Vector3(0, 0, 0);
        _obj.transform.DOScale(startScale, 0.5f).SetEase(Ease.OutBack).OnComplete(()=>
        {
            _obj.GetComponent<Tower>().GuardPositionFixed();
        });
    }
}
