using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Classe responsável por gerenciar a dinâmica do Jogo
public class GameManager : MonoBehaviour
{

	public string startMessage = "Jogue!";  // Mensagem a ser exibida no começo da fase
    public string endMessage = "Time's Up!";// Mensagem a ser exibida no fim da fase
    public float messageTime = 0.5f;        // Tempo que a mensagem inicial e final será exibida
    public float maxTime = 5.0f;            // Tempo de duração da fase do microjogo

    public Image timeBar;                   // Link para o objeto da barra de tempo
    public GameObject startUpText;          // Link para o objeto do texto inicial
    public GameObject timesUpText;          // Link para o objeto do texto final

    public bool pass;	// Deve ser definida no microgame para "false" se perdeu a partida

    float timeLeft;     // variável interna para controlar quanto tempo resta da fase
    int buildIndex;     // variável interna para controle de qual cena é no jogo

    public int level() {	// Permite o microgame recuperar o nível atual
		return GameState.difficulty;
	}

    void Start()
    {

    	buildIndex = SceneManager.GetActiveScene().buildIndex;
    	pass = true;    // por padrão se considera que jogador conseguiu finalizar a fase

        // Caso seja a cena principal (aquela que chama os microgames)
    	if (buildIndex==0) {

            timeBar.enabled = false;// Esconde a barra de tempo
            GameState.round += 1;   // Controla em que partida se está

            // Conforme a partida deixar o jogo mais rápido ou mais difícil
    		if(GameState.round==3) {
    			GameState.speed = 1.5f;
    		} else if(GameState.round==4) {
    			GameState.difficulty = 2;
    		} else if(GameState.round==5) {
    			GameState.speed = 2.0f;
    		} else if(GameState.round==6) {
    			GameState.difficulty = 3;
    		}

    		Time.timeScale = GameState.speed;   // Define a velocidade do jogo
	    	
    		if(GameState.lives>0){ // Caso as vidas não tenham acabado
    			StartCoroutine("Timer");	
    		} else { // Caso as vidas tenham acabado
    			startUpText.SetActive(false);
				timesUpText.SetActive(true);
	        	timesUpText.GetComponent<Text>().text = endMessage;
	        	Time.timeScale = 0; // Para o Jogo
    		}
    	}
        else // Caso seja um microgames
        {
    		Time.timeScale = GameState.speed;   // Define a velocidade do jogo
            // Exibe a mensagem para o jogo por messageTime segundos
            startUpText.GetComponent<Text>().text = startMessage;
	        startUpText.SetActive(true);

	        // Prepara a mensagem de fim de jogo, mas deixa ela escondida
	        timesUpText.SetActive(false);
	        timesUpText.GetComponent<Text>().text = endMessage;

	        // Define o valor do timer
	        timeLeft = maxTime;
	    }
    }

    void Update()
    {
        if (buildIndex>0) {  // Caso seja um microgames (não a cena principal)
            timeLeft -= Time.deltaTime; // reduz o tempo da partida

	        // Deixa a mensagem inicial ser exibida
	        if(timeLeft<maxTime-messageTime) {
	            startUpText.SetActive(false);
	        }

	        if(timeLeft>0) { // Checa se ainda há tempo
	        	timeBar.fillAmount = timeLeft / maxTime;
            } else { // Senão exibe a mensagem de fim
	        	timesUpText.SetActive(true);
	        	if( timeLeft < -messageTime ) {
	        		if(!pass) {
	        			GameState.lives -= 1;
	        		}
	        		SceneManager.LoadScene(0);
	        	}
	        }

	    }
    }

    IEnumerator Timer() {

        // Mostra número da partida e vidas restantes
    	timesUpText.SetActive(true);
        timesUpText.GetComponent<Text>().text = string.Format("Partida {0} - {1} Vidas", GameState.round, GameState.lives);

        // conta para o jogo começar
        startUpText.SetActive(true);
        for(int i=3; i>0; i--)
        {
            startUpText.GetComponent<Text>().text = string.Format("\n\n{0}",i);
            yield return new WaitForSeconds(0.7f);
        }
        // Lança aleatoriamente um Microjogo
        int totalScenes = SceneManager.sceneCountInBuildSettings;
    	SceneManager.LoadScene(Random.Range(1, totalScenes));
    } 

}

