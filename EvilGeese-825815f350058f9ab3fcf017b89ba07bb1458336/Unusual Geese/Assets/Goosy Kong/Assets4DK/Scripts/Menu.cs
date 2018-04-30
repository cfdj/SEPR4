using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class Menu : MonoBehaviour {
	public Canvas quitMenu;
	public Button exitText;



	//initialise quit Menu to be disabled
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas>();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
		
	}
	//enable quieMeny when exit button is pressed
	public void ExitPress()
	{
		quitMenu.enabled = true;
		exitText.enabled = false;
	}
	//close quit menu if no is pressed on quit menu
	public void NoPress()
	{
		quitMenu.enabled = false;
		exitText.enabled = true;
	}
	//leave game upon pressing yes of quit menu
	public void LeaveLevel(){
		SceneManager.LoadScene ("EndGame");
	}


}
