using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

	//Vous pouvez directement changer ces valeurs de base dans l'inspecteur si vous voulez personnaliser votre jeu
	[HideInInspector]public int playerHp = 20;
	public int playerMaxHp = 20;
	[HideInInspector]public int playerEnergy = 300;
	public int playerStartEnergy = 300;

	public int delayBetweenWaves = 10;					//Temps entre les vagues
	public int nextWaveEnnemyHpUp = 20; 				//Augmentation de la vie des bots a chaque vague (en %)
	public int nextWaveEnnemyValueUp = 30; 		//Augmentation de l'energie donnee par les bots a chaque vague (en %)
	public int averageWavesLenght = 15;					//Taille moyenne d'une vague d'ennemis
	public int totalWavesNumber = 20;						// Nombre des vagues au total
	[HideInInspector]public bool lastWave = false;
	[HideInInspector]public int currentWave = 1;
	private float tmpTimeScale = 1;
	[HideInInspector]public int score = 0;

	public static gameManager gm;
	private bool paused;
	
	public TextMeshProUGUI textEnergy;
	public TextMeshProUGUI textHP;

	public GameObject pauseUI;
	
	
	

	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (gm == null)
			gm = this;
	}

	private void Update()
	{
		textEnergy.text = playerEnergy.ToString();
		textHP.text = playerHp.ToString();
		if(playerHp <= 0)
			gameOver();
		
		if(Input.GetKeyDown(KeyCode.Escape))
			pause();
	}

	void Start() {
		Time.timeScale = 1;
		playerHp = playerMaxHp;
		playerEnergy = playerStartEnergy;
	}

	//Pour mettre le jeu en pause
	public void pause() {
		// Debug.Log("pause");
		
		if (!paused) {
			tmpTimeScale = Time.timeScale;
			Time.timeScale = 0;
			paused = true;
			pauseUI.SetActive(true);
		}
		else
		{
			pauseUI.SetActive(false);
			paused = false;
			Time.timeScale = tmpTimeScale;
		}
	}

	//Pour changer la vitesse de base du jeu
	public void changeSpeed(float speed) {
		Time.timeScale = speed;
	}

	//Le joueur perd de la vie
	public void damagePlayer(int damage) {
		playerHp -= damage;
		if (playerHp <= 0)
			gameOver();
		else
			Debug.Log ("Il reste au joueur " + playerHp + "hp");
	}

	//On pause le jeu en cas de game over
	public void gameOver() {
		Time.timeScale = 0;
		Debug.Log ("Game Over");
	}
}
