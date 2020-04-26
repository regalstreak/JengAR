using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JengaManager : Singleton<JengaManager> {
    //Jenga Stuff
    public GameObject table;
    public GameObject jengaPiece;
    // private float pieceOffsetZ = 0.25f;
    private float pieceOffsetZ = 0.05f;
    // private float pieceOffsetY = 0.15f;
    private float pieceOffsetY = 0.03f;
    public int layers = 6;
    private int currentLayer;
    private float spawnDelay = 0.1f;
    public string jengaPieceTag = "JengaPiece";

    [HideInInspector]
    public bool pieceSelected;
    public bool isPaused;
    public bool canMove;

    public int numOfPlayers;
    private bool gameInProgress;

    // AR
    private bool localPlacedTable = false;

    //Pause Stuff
    public Canvas PauseCanvas;
    //public PauseManager myPause;
    public Button startButton, ExitButton;

    protected JengaManager() { }

    private void Start() {
        PauseCanvas.enabled = false;

        startButton.onClick.AddListener(ResumeGame);
        ExitButton.onClick.AddListener(CloseGame);
        //settingsButton.onClick.AddListener(OpenSettings);
        table.GetComponent<TableTouching>().piecesTouching = 0;

        currentLayer = 0;
        pieceSelected = false;
        isPaused = false;
        gameInProgress = false;
        Time.timeScale = 1.0f;



        localPlacedTable = JengaPlacement.Instance.placedTable;
        Debug.Log(localPlacedTable);

        if (localPlacedTable) {
            Invoke("SpawnJengaPieces", 1f);
        }
    }

    private void Update() {

        if (!localPlacedTable && JengaPlacement.Instance.placedTable) {
            localPlacedTable = true;
            Invoke("SpawnJengaPieces", 1f);
        }

        if (Input.GetKeyUp("p") || Input.GetKeyUp(KeyCode.Escape)) {
            TogglePause();
        }

        if (!isPaused) {
            if (Input.GetKeyUp("r")) {
                ResetPieces();
            }

            // if (Input.GetKeyUp("c")) {
            //     Camera.main.GetComponent<FlyCamera>().enabled = !Camera.main.GetComponent<FlyCamera>().enabled;
            // }

            if (table.GetComponent<TableTouching>().piecesTouching >= 5) {
                GameOver();
            }
        }
    }

    public void SpawnJengaPieces() {
        if (currentLayer < layers) {
            if (currentLayer % 2 == 0) {
                SpawnHorizontalLayer(currentLayer);
            } else {
                SpawnVerticalLayer(currentLayer);
            }
            currentLayer++;
            Invoke("SpawnJengaPieces", spawnDelay);
        }

        gameInProgress = true;
    }
    private void SpawnHorizontalLayer(int layer) {
        Debug.Log("Spawning Horizontal");
        Vector3 center = new Vector3(JengaPlacement.Instance.tablePosition.x, JengaPlacement.Instance.tablePosition.y + pieceOffsetY * layer, JengaPlacement.Instance.tablePosition.z);
        Quaternion rotation = new Quaternion();
        Debug.Log(JengaPlacement.Instance.tablePosition.ToString() + ' ' + JengaPlacement.Instance.tableRotation.ToString());
        // Quaternion rotation = JengaPlacement.Instance.tableRotation;
        Instantiate(jengaPiece, center, rotation);
        Instantiate(jengaPiece, new Vector3(center.x, center.y, center.z + pieceOffsetZ), rotation);
        Instantiate(jengaPiece, new Vector3(center.x, center.y, center.z - pieceOffsetZ), rotation);
    }
    private void SpawnVerticalLayer(int layer) {
        Debug.Log("Spawning Vertical");
        Vector3 center = new Vector3(JengaPlacement.Instance.tablePosition.x, JengaPlacement.Instance.tablePosition.y + pieceOffsetY * layer, JengaPlacement.Instance.tablePosition.z);

        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        // Quaternion rotation = JengaPlacement.Instance.tableRotation;
        // rotation *= Quaternion.Euler(0, 90f, 0);

        // Vector3 rot = JengaPlacement.Instance.tableRotation.eulerAngles;
        // rot = new Vector3(rot.x, rot.y + 90, rot.z);
        // Quaternion rotation = Quaternion.Euler(rot);

        Debug.Log(JengaPlacement.Instance.tablePosition.ToString() + ' ' + rotation.ToString());
        Instantiate(jengaPiece, center, rotation);
        Instantiate(jengaPiece, new Vector3(center.x + pieceOffsetZ, center.y, center.z), rotation);
        Instantiate(jengaPiece, new Vector3(center.x - pieceOffsetZ, center.y, center.z), rotation);
    }

    public void ResetPieces() {
        ClearPieces();
        table.GetComponent<TableTouching>().piecesTouching = 0;
        currentLayer = 0;
        SpawnJengaPieces();
    }
    private void ClearPieces() {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag(jengaPieceTag);
        foreach (GameObject piece in pieces) {
            Destroy(piece);
        }
    }

    private void GameOver() {
        PauseCanvas.enabled = true;
        canMove = false;
        gameInProgress = false;
        isPaused = true;
        // Camera.main.GetComponent<FlyCamera>().enabled = false;
    }

    private void TogglePause() {
        isPaused = !isPaused;
        if (isPaused) {
            PauseGame();
        }
        if (!isPaused) {
            ResumeGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        PauseCanvas.enabled = true;
        canMove = false;
        // Camera.main.GetComponent<FlyCamera>().enabled = false;
    }
    public void ResumeGame() {
        Time.timeScale = 1.0f;
        PauseCanvas.enabled = false;
        canMove = true;
        isPaused = false;
        // Camera.main.GetComponent<FlyCamera>().enabled = true;

        if (!gameInProgress) {
            ResetPieces();
            gameInProgress = true;
        }
    }

    public void CloseGame() {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

}
