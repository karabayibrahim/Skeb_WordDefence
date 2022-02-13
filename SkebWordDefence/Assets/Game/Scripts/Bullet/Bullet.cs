using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NpcController>()!=null)
        {
            
            //Destroy();
            //other.gameObject.SetActive(false);
            GameManager.Instance.ShotSystem.RemoveNpc(other.gameObject.GetComponent<NpcController>());
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
