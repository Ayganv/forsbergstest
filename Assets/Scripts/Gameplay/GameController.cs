using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public EnemyProducer enemyProducer;
    public GameObject playerPrefab;
    public Text winText;
    public Text donateText;
    public Text countText;
    public int enemyDeaths;

    void Start () {
        enemyDeaths = 0;
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.onPlayerDeath += onPlayerDeath;
        countText.text = GetKillCountText();
    }

        void Update() {
    }

    public void onPlayerDeath(Player player) {
        enemyProducer.SpawnEnemies(false);
        Destroy(player.gameObject);

        Invoke("restartGame", 5);
    }

    void restartGame() {
        enemyDeaths = 0;
        winText.text = "";
        donateText.text = "";
        countText.text = "";
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            Destroy(enemy);
        }
        var playerObject = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        var cameraRig = Camera.main.GetComponent<CameraRig>();
        cameraRig.target = playerObject;
        enemyProducer.SpawnEnemies(true);
        playerObject.GetComponent<Player>().onPlayerDeath += onPlayerDeath;
    }

    void StopEnemies() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            Destroy(enemy);
        }
        var enemyProducer = GameObject.Find("EnemyProducer");
        Destroy(enemyProducer);
    }

    public void OnEnemyDeath() {
        enemyDeaths++;
        countText.text = GetKillCountText();
        if (enemyDeaths >= 5) {
            winText.text = "VIRG WINS!";
            donateText.text = "Ps. Don't forget to check out GDQ on Twitch this weekend Aug 13th to 23rd!";
            StopEnemies();
        }
    }

    String GetKillCountText() {
        return "Kill count: " + enemyDeaths;
    }

}
