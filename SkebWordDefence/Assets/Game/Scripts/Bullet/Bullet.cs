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
        if (other.gameObject.GetComponent<NpcController>() != null)
        {

            //Destroy();
            //other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<NpcController>().NpcState = NpcState.DEAD;
            GameManager.Instance.ShotSystem.RemoveNpc(other.gameObject.GetComponent<NpcController>());
            GameManager.Instance.NpcList.Remove(other.gameObject);
            Destroy(other.gameObject, 4f);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
