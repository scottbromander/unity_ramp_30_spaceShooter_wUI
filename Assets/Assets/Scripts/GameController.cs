using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public Transform enemy;

	[Header("Wave Properties")]
	public float timeBeforeSpawning = 1.5f;
	public float timeBetweenEnemies = 0.25f;
	public float timeBeforeWaves = 2.0f;

	private int score = 0;
	private int waveNumber = 0;

	[Header("Game Settings")]
	public int playerStartingLives = 3;

	private int playerLives = 0;

	[Header("User Interface")]
	public Text scoreText;
	public Text waveText;
	public Text livesText;
	public Text gameOverText;

	public int enemiesPerWave = 10;
	private int currentNumberOfEnemies = 0;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		gameOverText.text = "";
		StartCoroutine(SpawnEnemies());
		playerLives = playerStartingLives;
		updateLivesText (playerLives);
	}

	IEnumerator SpawnEnemies() {
		yield return new WaitForSeconds (timeBeforeSpawning);

		while (!gameOver) {
			if (currentNumberOfEnemies <= 0) {
				waveNumber++;
				waveText.text = "Wave: " + waveNumber;

				for (int i = 0; i < enemiesPerWave; i++) {
					float randDistance = Random.Range (10, 25);

					Vector2 randDirection = Random.insideUnitCircle;
					Vector3 enemyPos = this.transform.position;

					enemyPos.x = randDirection.x * randDistance;
					enemyPos.y = randDirection.y * randDistance;

					Instantiate (enemy, enemyPos, this.transform.rotation);
					currentNumberOfEnemies++;
					yield return new WaitForSeconds (timeBetweenEnemies);
				}
			}

			yield return new WaitForSeconds (timeBeforeWaves);
		}
	}

	public void KilledEnemy(){
		currentNumberOfEnemies--;
	}

	public void PlayerHit(){
		print ("Player hit!");
		playerLives--;
		updateLivesText (playerLives);

		if (playerLives <= 0) {
			EndGame ();
		}
	}

	private void updateLivesText(int lives){
		livesText.text = "Lives: " + lives; 
	}

	public void IncreaseScore(int increase){
		score += increase;
		scoreText.text = "Score: " + score;
	}

	public void EndGame() {
		GameObject player = GameObject.Find ("playerShip");
		Destroy (player);
		gameOver = true;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}

		scoreText.text = "";
		waveText.text = "";
		livesText.text = "";
		gameOverText.text = "GAME OVER\n" + "SCORE: " + score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
