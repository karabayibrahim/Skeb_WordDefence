using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LaserControl : MonoBehaviour
{
    public List<NpcController> Enemys = new List<NpcController>();
    public GameObject LaserBullet;

    private bool _targetControl = false;
    private Transform _targetObj;
    private float fireRate = 0.5f;
    private float lastShot = 0;
    [SerializeField] private Animator _anim;
    [SerializeField]private Transform StartShootPoz;
    [SerializeField] private Transform StartShootPoz2;
    private Tween BulletTween;
    private Tween BulletTween2;
    private bool _spawnControl = false;
    void Start()
    {
        Finish.FinishAction += FinishStatus;
        GameManager.Instance.ShotSystem.LaserShooters.Add(this);
        _anim = GetComponentInChildren<Animator>();
        StartCoroutine(SpawnTimer());
    }

    private void OnDisable()
    {
        Finish.FinishAction -= FinishStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemys.Count <= 0||GameManager.Instance.GameState!=GameState.START)
        {
            _anim.CrossFade("Idle",0.01f);
        }
        if (_spawnControl&&Enemys.Count>0)
        {
            ShotMethod();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NpcController>() != null)
        {
            Enemys.Add(other.gameObject.GetComponent<NpcController>());
            TargetMethod();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<NpcController>() != null)
        {
            Enemys.Remove(other.gameObject.GetComponent<NpcController>());
            BulletTween.Kill(true);
            BulletTween2.Kill(true);
        }
    }

    public void TargetMethod()
    {
        if (gameObject != null)
        {
            _targetControl = true;
            NpcRemoveList();
            var index = UnityEngine.Random.Range(0, Enemys.Count);
            var targetObj = Enemys[index];
            _targetObj = targetObj.transform;
            //gameObject.transform.DOLookAt(new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z), 0.1f);
            transform.DOLookAt(new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z), 0.5f).OnComplete(() => _targetControl = false);
        }
    }

    public void ShotMethod()
    {
        if (Time.time > fireRate + lastShot && !_targetControl)
        {
            if (gameObject!=null)
            {
                _anim.CrossFade("Shot", 0.01f);
                var newParticle = Instantiate(GameManager.Instance.Particles[1], StartShootPoz.position, Quaternion.identity);
                var newParticle2 = Instantiate(GameManager.Instance.Particles[1], StartShootPoz2.position, Quaternion.identity);
                Destroy(newParticle, 0.1f);
                Destroy(newParticle2, 0.1f);
                //Debug.Log("LaserShot");
                lastShot = Time.time;
                var newBullet = Instantiate(LaserBullet, StartShootPoz.position, Quaternion.identity);
                var newBullet2 = Instantiate(LaserBullet, StartShootPoz2.position, Quaternion.identity);
                if (_targetObj!=null)
                {
                    newBullet.transform.LookAt(_targetObj.transform.position);
                    newBullet2.transform.LookAt(_targetObj.transform.position);
                    BulletTween = newBullet.transform.DOMove(_targetObj.transform.position, 0.4f);
                    BulletTween2 = newBullet2.transform.DOMove(_targetObj.transform.position, 0.4f);
                }
                
            }
            
        }

    }

    public void NpcRemoveList()
    {
        for (var i = Enemys.Count - 1; i > -1; i--)
        {
            if (Enemys[i] == null)
                Enemys.RemoveAt(i);
        }
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(2f);
        _spawnControl = true;
    }

    private void FinishStatus()
    {
        _targetControl = true;
        _anim.CrossFade("Idle", 0.01f);
        this.enabled = false;
    }
}
