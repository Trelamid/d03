using System;
using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.SceneManagement;
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
	
	public Text textEnergy;
	public Text textHP;
	
	public TextMeshProUGUI gameSpeedText;

	public GameObject pauseUI;

	public bool win;
	public GameObject gameoverUI;
	public GameObject gameoverUIFon;
	public GameObject gameoverUIWindow;
	public GameObject gameoverRangeUI;
	public GameObject gameoverScoreUI;
	public GameObject gameoverButtonWinUI;
	public GameObject gameoverButtonFailUI;

	private Camera _camera;

	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (gm == null)
			gm = this;
	}
	
	void Start() {
		Time.timeScale = 1;
		playerHp = playerMaxHp;
		playerEnergy = playerStartEnergy;

		_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		// textEnergy = GameObject.Find("EnergyPlayer").GetComponent<TextMeshPro>();
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var obj = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
			if(obj)
				Debug.Log(obj.gameObject);
			if (obj && obj.gameObject.tag == "tower")
			{
				obj.gameObject.GetComponentInParent<towerScript>().OnClick();
			}
		}
		
		if (win)
		{
			myGameOver();
		}
		
		if (textEnergy)
			textEnergy.text = playerEnergy.ToString();
		
		if(textHP)
			textHP.text = playerHp.ToString();
		
		if(playerHp <= 0)
			gameOver();
		
		if(Input.GetKeyDown(KeyCode.Escape))
			pause();
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
		if (speed == 0)
			gameSpeedText.text = "Pause";
		else
			gameSpeedText.text = "Speed " + speed.ToString() + "x";
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
		myGameOver();
	}

	public void myGameOver()
	{
		gameoverUI.SetActive(true);
		gameoverScoreUI.GetComponent<TextMeshProUGUI>().text = score.ToString();

		if (win)
		{
			gameoverButtonFailUI.SetActive(false);
			if (SceneManager.GetActiveScene().buildIndex == 5)
			{
				gameoverButtonWinUI.SetActive(false);
				gameoverUIWindow.GetComponent<TextMeshProUGUI>().text = "You win!";
			}
			else
			{
				gameoverButtonWinUI.SetActive(true);
				gameoverUIFon.GetComponent<Image>().enabled = false;
				gameoverUIWindow.GetComponent<TextMeshProUGUI>().text = "Good!";
			}
		}
		else
		{
			gameoverUIWindow.GetComponent<TextMeshProUGUI>().text = "You lose!";
			gameoverUIFon.GetComponent<Image>().enabled = false;
			gameoverButtonWinUI.SetActive(false);
			gameoverButtonFailUI.SetActive(true);
		}
		
		if (playerEnergy > 500 && playerHp == playerMaxHp)
		{
			gameoverRangeUI.GetComponent<TextMeshProUGUI>().text = "A";
			
		}
		else if(playerEnergy > 400 && playerHp > 15)
		{
			gameoverRangeUI.GetComponent<TextMeshProUGUI>().text = "B";

		}
		else if(playerEnergy > 300 && playerHp > 10)
		{
			gameoverRangeUI.GetComponent<TextMeshProUGUI>().text = "C";

		}
		else if(playerEnergy > 100 && playerHp > 5)
		{
			gameoverRangeUI.GetComponent<TextMeshProUGUI>().text = "D";

		}
		else
		{
			gameoverRangeUI.GetComponent<TextMeshProUGUI>().text = "E";

		}
	}
}
