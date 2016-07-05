using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {
	public float playerSpeed = 4.0f;
	public float playerDrag = 0.90f;
	private float currentSpeed = 0.0f;
	private Vector3 lastMovement = new Vector3 ();

	public Transform laser;
	public float laserDistance = 0.2f;
	public float timeBetweenFires = 0.3f;
	private float timeTilNextFire = 0.0f;

	public AudioClip shootSound;
	private AudioSource audioSource;

	public List<KeyCode> shootButton;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}


	// Update is called once per frame
	void Update () {
		CheckFire ();
		Rotation ();
		Movement ();
	}

	void CheckFire(){
		foreach (KeyCode element in shootButton) {
			if (Input.GetKey (element) && timeTilNextFire < 0) {
				timeTilNextFire = timeBetweenFires;
				ShootLaser ();
				break;
			}
		}

		timeTilNextFire -= Time.deltaTime;
	}

	void ShootLaser(){
		audioSource.PlayOneShot (shootSound);
		Vector3 laserPos = this.transform.position;
		float rotationAngle = transform.localEulerAngles.z - 90;
		laserPos.x += (Mathf.Cos ((rotationAngle) * Mathf.Deg2Rad) * - laserDistance);
		laserPos.y += (Mathf.Sin ((rotationAngle) * Mathf.Deg2Rad) * - laserDistance);

		Instantiate (laser, laserPos, this.transform.rotation);
	}

	void Rotation() {
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint (worldPos);

		float dx = this.transform.position.x - worldPos.x;
		float dy = this.transform.position.y - worldPos.y;

		float angle = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg;

		Quaternion rot = Quaternion.Euler(new Vector3(0,0, angle + 90));

		this.transform.rotation = rot;
	}

	void Movement(){
		Vector3 movement = new Vector3();

		movement.x += Input.GetAxis("Horizontal");
		movement.y += Input.GetAxis("Vertical");

		movement.Normalize();

		if(movement.magnitude > 0){
			currentSpeed = playerSpeed;
			this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
			lastMovement = movement;
		} else {
			this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
			currentSpeed *= playerDrag;
		}
	}
}
