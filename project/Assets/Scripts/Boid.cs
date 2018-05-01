using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
	
	AudioSource sound;
	public AudioClip pew;
	public AudioClip boomsound;

	public Vector3 force = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 30.0f;
	public float disiredDist = 20f;
	public bool shooting;
	public bool targeted = false;
	public float fireRate = 0.8f;
	float lastShot = 0.0f;

	public float damage = 0f;
	public bool takingDamage = false;
	public bool destroyed = false;
	public GameObject debris;
	public GameObject bullets;

	List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

	void Start () {
		shooting = false;
		sound = gameObject.GetComponent<AudioSource> ();

		SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour> ();
		foreach (SteeringBehaviour b in behaviours) {
			this.behaviours.Add (b);
		}
	}
	public Vector3 ArriveForce(Vector3 target, float slowingDistance = 15.0f, float deceleration = 1.0f)
	{
		Vector3 toTarget = target - transform.position;

		float distance = toTarget.magnitude;
		if (distance < disiredDist) {
			return Vector3.zero;
		}
		float ramped = maxSpeed * (distance / (slowingDistance * deceleration));

		float clamped = Mathf.Min(ramped, maxSpeed);
		Vector3 desired = clamped * (toTarget / distance);

		return desired - velocity;
	}

	Vector3 Calculate()
	{
		force = Vector3.zero;

		foreach (SteeringBehaviour b in behaviours)
		{
			if (b.isActiveAndEnabled)
			{
				force += b.Calculate() * b.weight;
			}
		}


		return force;
	}

	void Update () {
		force = Calculate();
		Vector3 newAcceleration = force / mass;

		float smoothRate = Mathf.Clamp(9.0f * Time.deltaTime, 0.15f, 0.4f) / 2.0f;
		acceleration = Vector3.Lerp(acceleration, newAcceleration, smoothRate);

		velocity += acceleration * Time.deltaTime;

		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

		Vector3 globalUp = new Vector3(0, 0.2f, 0);
		Vector3 accelUp = acceleration * 0.05f;
		Vector3 bankUp = accelUp + globalUp;
		smoothRate = Time.deltaTime * 3f;
		Vector3 tempUp = transform.up;
		tempUp = Vector3.Lerp(tempUp, bankUp, smoothRate);

		if (velocity.magnitude  > 0.0001f)
		{
			transform.LookAt(transform.position + velocity, tempUp);
			velocity *= 0.99f;
		}
		transform.position += velocity * Time.deltaTime;


		if (takingDamage) {
			damage += 10 * Time.deltaTime;
			gameObject.GetComponent<Attack> ().enabled = false;
			gameObject.GetComponent<Flee> ().enabled = true;
			shooting = false;
		}

		if (shooting && !destroyed && Borg.hostilePresent) {
			if (Time.time > fireRate + lastShot) {
				GameObject pewpew = Instantiate (bullets, transform.position, transform.rotation) as GameObject;
				Physics.IgnoreCollision (this.gameObject.GetComponentInChildren<CapsuleCollider> (), pewpew.gameObject.GetComponent<SphereCollider> ());
				sound.pitch = Random.Range(0.8f, 1.2f);
				sound.PlayOneShot (pew);
				lastShot = Time.time;
			}
		}

		if (damage > 100 && destroyed == false) {
			sound.PlayOneShot (boomsound);
			GameObject boom = GameObject.Instantiate(debris, transform.position + new Vector3(-10, 0, 80), transform.GetChild(0).gameObject.transform.rotation) as GameObject;
			Destroy (transform.GetChild(0).gameObject);
			destroyed = true;
		}

		if (destroyed) {
			maxSpeed = 0f;
			gameObject.GetComponent<Attack> ().enabled = false;
			gameObject.GetComponent<Flee> ().enabled = false;
			gameObject.GetComponent<Wander> ().enabled = false;
			gameObject.GetComponent<Regroup> ().enabled = false;
		}
	}

	public void Engage() {
		StartCoroutine (Engaging());
	}

	IEnumerator Engaging() {
		shooting = true;
		yield return new WaitForSeconds (2f);
		targeted = true;
	}
		
}
