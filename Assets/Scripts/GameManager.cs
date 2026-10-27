using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }
    public int lives { get; private set; }

    public TextMeshProUGUI scoreText;
    public GameObject[] lifeIcons;
    public GameObject gameOverText;
    public GameObject gameOverSub;

    private void Start(){
        NewGame();
    }

    private void Update(){
        if (this.lives <= 0 && Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame){
            NewGame();
        }
    }

    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();
        this.gameOverText.SetActive(false);
        this.gameOverSub.SetActive(false);
    }

    private void NewRound(){
        foreach(Transform pellet in this.pellets){
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState(){
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }

    private void GameOver(){
        for (int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
        this.gameOverText.SetActive(true);
        this.gameOverSub.SetActive(true);
    }



    private void SetScore(int score){
        this.score = score;
        this.scoreText.text = score.ToString();
    }

    private void SetLives(int lives){
        this.lives = lives;
        
        for (int i = 0; i < this.lifeIcons.Length; i++)
        {
            this.lifeIcons[i].SetActive(i < lives);
        }
    }

    public void GhostEaten(Ghost ghost){
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten(){
        this.pacman.Die();
        SetLives(this.lives - 1);
 
        if (this.lives > 0){
            Invoke(nameof(ResetState), 3.0f);
        } else {
            Invoke(nameof(GameOver), 2.0f);
        }
    }

    public void PelletEaten(Pellet pellet){
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets()){
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet){

        for (int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets(){
        foreach(Transform pellet in this.pellets){
            if (pellet.gameObject.activeSelf){
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier(){
        this.ghostMultiplier = 1;
    }
}
