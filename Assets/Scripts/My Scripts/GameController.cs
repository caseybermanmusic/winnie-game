using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public GameObject hazard2;
    public GameObject hazard3;
    public GameObject hazard4;
    public GameObject splat;
    public GameObject window;
    public float spawnWait;
    public float waveWait;
    public float startWait;
    public int currentLevel;
    public int windowsToClean;
    public int splatsDown;
    public Vector2 spawnValues;
    public int moneyMade;
    public int HowManyStages;

    private bool gameOver;
    private bool restart;
    private Vector3 spawnPosition;
    private Vector3 spawnPosition3;
    private int[] splatCoordinates;

    private float [,] windowCoordinates = new float[9,2] {
        {-1.64f, 0f}, {-0.08f, 0f}, {1.56f, 0f}, //bottom row
        {-1.64f, 1.92f}, {-0.08f, 1.92f}, {1.56f, 1.92f}, //middle
        {-1.64f, 3.84f}, {-0.08f, 3.84f}, {1.56f, 3.84f}}; //top

    private int randomPick;
    public int hazardCount;
    // public int waveNumber;

    public PlayerController playerController;
    public splat splatScript;
    public CharMoveUp charMoveUp;
    public GameObject scaffold;

    private int randomInt;

    public bool moving;

    public int windowValue;
    public Text ScoreText;
    public Text RestartText;

    void Start() {
        WindowSelecting();
		StartCoroutine (SpawnWaves());
        scaffold = GameObject.FindWithTag("Scaffold");
        gameOver = false;
        restart = false;
        moving = false;
        moneyMade = 0;
        updateMoney();
        RestartText.text = "";
    }

    public void updateMoney() {

    ScoreText.text = "you've made: $" + moneyMade * windowValue;
    }

    void randomWindowPick() {
        randomPick = Random.Range(0, 9);
        int pos = System.Array.IndexOf(splatCoordinates, randomPick);
        if (pos > -1) {
            randomWindowPick();
        }
    }

    void WindowSelecting() {
        if (currentLevel > 1) {
            for (int j = 0; j < splatCoordinates.Length; j++) {
                splatCoordinates[j] = 0;
            }
        }
        splatCoordinates = new int[windowsToClean];
        for (int i = 0; i < windowsToClean; i++) {
            randomWindowPick();
            splatCoordinates[i] = randomPick;
            Vector3 splatPosition = new Vector3(
                windowCoordinates[randomPick, 0],
                windowCoordinates[randomPick, 1] - .6f,
                0
            );
            Instantiate(splat, splatPosition, Quaternion.identity);
        }
        for(int k = 0; k < windowCoordinates.GetUpperBound(0) + 1; k ++) {

            Vector3 windowPosition = new Vector3(
                windowCoordinates[k, 0],
                windowCoordinates[k, 1] - 0.4f,
                0
            );
            Instantiate(window, windowPosition, Quaternion.identity);
        }
    }
    void MoveWindows() {
        for (int i = 0; i < 9; i++) {
            // make this ^^ number generic later
            windowCoordinates[i, 1] += 8.96f;
        }
    }

    public void nextSegment() {
        currentLevel += 1;
        Debug.Log(currentLevel);
        if (currentLevel < 12) {
            MoveWindows();
            WindowSelecting();
            splatsDown = 0;
            charMoveUp.nextPlatform += 8.96f;
        } else {
            gameOver = true;
            restart = true;
            RestartText.text = "this is as far as the game goes! Press 'r' to restart";
        }
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while(gameOver == false) {
            if (playerController.Health <= 0) {
                gameOver = true;
                break;
            };
            for(int i = 0; i < hazardCount; i++) {
                int hazardPicker;
                if (currentLevel > 4) {
                    hazardPicker = Random.Range(0,3);
                } else {
                    hazardPicker = Random.Range(0,2);
                }
                if (hazardPicker != 2) {
                    randomPick = Random.Range(0, 9);
                    spawnPosition = new Vector3(
                        windowCoordinates[randomPick, 0],
                        windowCoordinates[randomPick, 1],
                        0
                    );
                    if (hazardPicker == 0) {
                        Instantiate(hazard2, spawnPosition, Quaternion.identity);
                    } 
                    if (hazardPicker == 1) {
                        Instantiate(hazard3, spawnPosition, Quaternion.identity);
                    }
                }
                else {
                    randomInt = Random.Range(0, 2);
                    if (randomInt == 1) {
                        spawnPosition3 = new Vector3 (1.7f - Random.Range(-.6f, .6f), playerController.rb.position.y - 1, 0);
                    } else {
                        spawnPosition3 = new Vector3 (-1.7f - Random.Range(-.6f, .6f), playerController.rb.position.y - 1, 0);
                    }
                    Instantiate(hazard4, spawnPosition3, Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        if (gameOver == true) {
            restart = true;
            if (playerController.Health <= 0) {
                RestartText.text = "you died! Press 'r' to restart";
            }
        }
    }

    void Update () {
        if(charMoveUp.moving == true) {
            moving = true;
        } else {
            moving = false;
        }
		if(restart) {
			if(Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
}