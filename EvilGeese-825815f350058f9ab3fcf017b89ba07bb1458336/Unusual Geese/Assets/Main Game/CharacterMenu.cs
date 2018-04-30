using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// handles the character selection menu
/// <summary>
/// [EXTENSIONS] - Added text to display the current position of a character in the current team
/// </summary>
public class CharacterMenu : MonoBehaviour {
	GameObject characterButtonPattern;
	GameObject abilityTextPattern;
	GameObject costTextPattern;
	GameObject characterNameText;
	GameObject healthText;
	GameObject energyText;
	GameObject positionText;

	CombatCharacterFactory.CombatCharacterPresets currentCharacter;
	GameStateManager state;

	List<GameObject> buttonObjects;
	List<GameObject> abilityTextObjects;
	List<GameObject> costTextObjects;


	// Use this for initialization
	void Start () {
		state = GameStateManager.getGameStateManager ();
		characterButtonPattern = this.transform.Find ("CharacterSelectButton").gameObject;
		abilityTextPattern = this.transform.Find ("AbilityText").gameObject;
		costTextPattern = this.transform.Find ("CostText").gameObject;
		characterNameText = this.transform.Find ("CharacterNameText").gameObject;
		healthText = this.transform.Find ("HealthText").gameObject;
		energyText = this.transform.Find ("EnergyText").gameObject;
		positionText = this.transform.Find ("PosText").gameObject;

		buttonObjects = new List<GameObject> ();
		selectCharacter(state.availibleCharacters[0]);

		float buttonOffset = 0.0f;
		foreach (CombatCharacterFactory.CombatCharacterPresets character in state.availibleCharacters) {
			GameObject button = Instantiate (characterButtonPattern, this.transform);
			button.transform.Find ("Text").gameObject.GetComponent<Text> ().text = CombatCharacterFactory.GetCharacterName (character);
			button.SetActive (true);
			buttonObjects.Add (button);
			((RectTransform)(button.transform)).anchorMin -= new Vector2 (0f, buttonOffset);
			((RectTransform)(button.transform)).anchorMax -= new Vector2 (0f, buttonOffset);
			CombatCharacterFactory.CombatCharacterPresets tempValue = character;
			button.GetComponent<Button> ().onClick.AddListener (delegate {
				selectCharacter (tempValue);
				});
			buttonOffset += 0.08f;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// [EXTENSION] - Call updateDisplayedPosition for the character
	/// </summary>
	/// <param name="character">The character</param>
	public void selectCharacter(CombatCharacterFactory.CombatCharacterPresets character){
		currentCharacter = character;
		characterNameText.GetComponent<Text> ().text = CombatCharacterFactory.GetCharacterName (character);
		healthText.GetComponent<Text> ().text = "Health: " + CombatCharacterFactory.GetCharacterMaxhealth (character).ToString ();
		energyText.GetComponent<Text> ().text = "Energy: " + CombatCharacterFactory.GetCharacterMaxEnergy (character).ToString ();
		updateDisplayedPosition (character);

		if (abilityTextObjects != null) {
			foreach (GameObject o in abilityTextObjects) {
				Destroy (o);
			}
		}
		if (costTextObjects != null) {
			foreach (GameObject o in costTextObjects) {
				Destroy (o);
			}
		}

		abilityTextObjects = new List<GameObject> ();
		costTextObjects = new List<GameObject> ();

		float abilityOffset = 0f;
		foreach (CombatAbility ability in CombatCharacterFactory.GetCharacterAbilities(character)) {
			GameObject abilityText = Instantiate (abilityTextPattern, this.transform);
			GameObject costText = Instantiate (costTextPattern, this.transform);
			abilityTextObjects.Add (abilityText);
			costTextObjects.Add (costText);

			abilityText.SetActive (true);
			costText.SetActive (true);
			abilityText.GetComponent<Text> ().text = ability.abilityName;
			costText.GetComponent<Text> ().text = ability.energyCost.ToString() + " energy";

			((RectTransform)abilityText.transform).anchorMin -= new Vector2 (0, abilityOffset);
			((RectTransform)abilityText.transform).anchorMax -= new Vector2 (0, abilityOffset);
			((RectTransform)costText.transform).anchorMin -= new Vector2 (0, abilityOffset);
			((RectTransform)costText.transform).anchorMax -= new Vector2 (0, abilityOffset);
			abilityOffset += 0.05f;
		}

	}

	/// <summary>
	/// [EXTENSION] - Display the current position of a character in the current team
	/// If not in current team, display "Not in current team"
	/// </summary>
	/// <param name="character">The character</param>
	public void updateDisplayedPosition(CombatCharacterFactory.CombatCharacterPresets character) {
		string pos;
		int index = state.currentTeam.IndexOf (character);
		if (index >= 0) {
			pos = (index + 1).ToString ();
		} else {
			pos = "Not in current team";
		}
		positionText.GetComponent<Text> ().text = "Position: " + pos;
	}

	/// <summary>
	/// [EXTENSION] - Also call updateDisplayedPosition to update display to the newly changed order
	/// </summary>
	/// <param name="pos">The position to place the team member in</param>
	public void setCurrentCharacterPositionInTeam(int pos){
		if (pos >= state.currentTeam.Count) {
			if (!state.currentTeam.Contains (currentCharacter)) {
				state.currentTeam.Add (currentCharacter);
			}
			return;
		}

		if (state.currentTeam.Contains (currentCharacter)) {
			if (state.currentTeam.IndexOf (currentCharacter) == pos) {
				return;
			}

			CombatCharacterFactory.CombatCharacterPresets swapValue = state.currentTeam [pos];
			int otherPos = state.currentTeam.IndexOf (currentCharacter);
			state.currentTeam [otherPos] = swapValue;
			state.currentTeam [pos] = currentCharacter;

		} else {
			state.currentTeam [pos] = currentCharacter;
		}

		updateDisplayedPosition (currentCharacter);
	}

	public static void open(){
		GameStateManager state = GameStateManager.getGameStateManager ();
		state.movementEnabled = false;
		Instantiate (Resources.Load ("CharRosterCanvas"));
	}

	public void close (){
		Destroy (this.transform.gameObject);
		state.movementEnabled = true;
	}
}
