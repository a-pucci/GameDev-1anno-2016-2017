using UnityEngine;
using System.Collections;

public class GranadeDamage : MonoBehaviour
{
	public int explosionDamage = 40;
	public float explosionRadius = 10f;
	public GameObject player;

	EnemyHealth enemyHealth;
	ParticleSystem explosion;


	// Use this for initialization
	void Start ()
	{
		explosion = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject != player)
		{
			Collider[] hitColliders = Physics.OverlapSphere (this.transform.position, explosionRadius);
			int i = 0;
			while (i < hitColliders.Length)
			{
				if (hitColliders [i].gameObject.tag == "Enemy")
				{
					EnemyHealth enemyHealth = hitColliders [i].GetComponent <EnemyHealth> ();
					if(enemyHealth != null)
					{
						Vector3 nullVec = new Vector3 (0, 0, 0);
						enemyHealth.TakeDamage (explosionDamage, nullVec);
					}
				}
				i++;
			}

		}
		gameObject.AddComponent<Rigidbody> ();
		Rigidbody granadeRB = gameObject.GetComponent <Rigidbody> ();
		granadeRB.isKinematic = true;
		Renderer rend = GetComponent<Renderer>();
		rend.enabled = false;
		explosion.Play();
		Destroy(gameObject, explosion.duration);
	}
		
}

