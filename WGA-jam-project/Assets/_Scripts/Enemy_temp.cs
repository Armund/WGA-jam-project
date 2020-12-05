using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_temp : MonoBehaviour
{
	public Transform target;
	public GameObject bulletPrefab;

	public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Shoot());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator Shoot() {
		//Vector3 targetPosition = target.transform.position;
        transform.LookAt(target);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		//bullet.transform.LookAt(target);
		Vector3 direction = bullet.transform.forward;
        bullet.GetComponent<BulletScript>().SetInitTarget(target.position);
        //direction.z = 0;
        //Debug.Log(direction);
        //bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed);

        yield return new WaitForSeconds(2);
		StartCoroutine(Shoot());
	}
}
