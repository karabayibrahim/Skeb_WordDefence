using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManager : ScriptableObject
{
    public GameObject SpawnObject;
    [Header("Towers")]
    //public GameObject WoodTower;
    //public GameObject StoneTower;
    //public GameObject ConcreateTower;
    //public GameObject LaserTower;
    //public GameObject TowerG;
    public List<TowerG> Towers = new List<TowerG>();
    private bool _firstSpawn = false;


    public void SpawnObjectMethod()
    {
        float PozX = Random.Range(-6, 6);
        float PozZ = Random.Range(GameManager.Instance.Player.transform.position.z + 50, GameManager.Instance.Player.transform.position.z + 55f);
        var newNpc = Instantiate(SpawnObject, new Vector3(PozX, 0, PozZ), Quaternion.identity);
        GameManager.Instance.NpcList.Add(newNpc);
        newNpc.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void TowerSpawn(int _wordCount,float PosZ,string playerInput)
    {
        if (GameManager.Instance.FirstSpawn)
        {
            GameManager.Instance.Player.transform.position = new Vector3(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y, GameManager.Instance.Player.transform.position.z - 5f);
        }
        switch (_wordCount)
        {
            case 2:
                var newTower = Instantiate(Towers[0], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower.gameObject);
                break;
            case 3:
                var newTower2 = Instantiate(Towers[1], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower2.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower2.gameObject);
                break;
            case 4:
                var newTower3 = Instantiate(Towers[2], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower3.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower3.gameObject);
                break;
            case 5:
                var newTower4 = Instantiate(Towers[3], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower4.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower4.gameObject);
                break;
            case 6:
                var newTower5 = Instantiate(Towers[4], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower5.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower5.gameObject);
                break;
            case 7:
                var newTower6 = Instantiate(Towers[5], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower6.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower6.gameObject);
                break;
            case 8:
                var newTower7 = Instantiate(Towers[6], new Vector3(0, 0, PosZ), Quaternion.identity);
                newTower7.GetComponent<TowerG>().MyString = playerInput;
                StartScaleEffeckt(newTower7.gameObject);
                break;
            default:
                if (_wordCount>8)
                {
                    var newTower8 = Instantiate(Towers[6], new Vector3(0, 0, PosZ), Quaternion.identity);
                    newTower8.GetComponent<TowerG>().MyString = playerInput;
                    StartScaleEffeckt(newTower8.gameObject);
                }
                else
                {
                    GameManager.Instance.Player.transform.position = new Vector3(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y, GameManager.Instance.Player.transform.position.z + 5f);
                    Debug.Log("No");
                }
                break;
        }
    }
    //public void StoneSpawn(int _wordCount, float _pozZ)
    //{
    //    var tower = TowerAssigment(_wordCount);
    //    float PozX;
    //    float PozZ = _pozZ;
    //    if (_wordCount % 2 != 0)
    //    {
    //        PozX = _wordCount - 1;

    //    }
    //    else
    //    {
    //        PozX = _wordCount;
    //    }
    //    var myTowerG = Instantiate(TowerG, new Vector3(PozX, _pozY, PozZ), Quaternion.identity);
    //    for (int i = 0; i < _wordCount / 2; i++)
    //    {
    //        if (i == 0)
    //        {
    //            var newTower = Instantiate(tower, new Vector3(PozX, _pozY, PozZ), Quaternion.identity,myTowerG.transform);
    //            StartScaleEffeckt(newTower);
    //            if (_wordCount % 2 != 0&& _wordCount <= 3f)
    //            {
    //                newTower.GetComponent<Tower>().LeftTower.SetActive(true);
    //            }
    //            if (_wordCount <= 3f)
    //            {
    //                newTower.transform.rotation = Quaternion.Euler(0, 90, 0);
    //            }
    //        }

    //        else
    //        {
    //            if ((i == _wordCount / 2 - 1) && _wordCount % 2 != 0)
    //            {
    //                Debug.Log("Tek");
    //                var newTower = Instantiate(tower, new Vector3(PozX - (i * 4), _pozY, PozZ), Quaternion.identity, myTowerG.transform);
    //                StartScaleEffeckt(newTower);
    //                if (_wordCount <= 3f)
    //                {
    //                    newTower.transform.rotation = Quaternion.Euler(0, 90, 0);
    //                }
    //                else
    //                {
    //                    newTower.GetComponent<Tower>().LeftTower.SetActive(true);
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("Çift");
    //                var newTower = Instantiate(tower, new Vector3(PozX - (i * 4), _pozY, PozZ), Quaternion.identity, myTowerG.transform);
    //                StartScaleEffeckt(newTower);
    //                if (_wordCount <= 3f)
    //                {
    //                    newTower.transform.rotation = Quaternion.Euler(0, 90, 0);
    //                }
    //            }

    //        }
    //    }
    //}

    //private GameObject TowerAssigment(int _wordCount)
    //{
    //    if (_wordCount <= 3)
    //    {
    //        PozYAssigment(0.9f);
    //        return WoodTower;
    //    }
    //    else if (_wordCount > 3 && _wordCount < 6)
    //    {
    //        PozYAssigment(1.3f);
    //        return StoneTower;
    //    }
    //    else if (_wordCount > 5 && _wordCount < 8)
    //    {
    //        PozYAssigment(1f);
    //        return ConcreateTower;
    //    }
    //    else
    //    {
    //        PozYAssigment(0.7f);
    //        return LaserTower;
    //    }
    //}

    //private float PozYAssigment(float pozY)
    //{
    //    _pozY = pozY;
    //    return _pozY;
    //}

    private void StartScaleEffeckt(GameObject _obj)
    {
        //Vector3 startScale = _obj.transform.localScale;
        //_obj.transform.localScale = new Vector3(0, 0, 0);
        //_obj.transform.DOScale(startScale, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        // {
        //     _obj.GetComponent<TowerG>().ShooterPosSet();
        // });
    }
}
