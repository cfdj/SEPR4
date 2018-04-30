using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

/// <summary>
/// [EXTENSIONS] - Added bonusHealth and bonusAttack stats to allow characters to grow stronger
/// [CHANGES] - Changed BobbyBard character type
/// 		  - Add bonus health to character types so that they can become stronger
/// [Extensions Untwo] - added three new characters 
/// [Extensions Untwo] - added gorrila 
/// </summary>
public static class CombatCharacterFactory {
	public enum CombatCharacterPresets{// always store an element from this list instead of a CombatCharacter if a variable needs to be serialized
		BobbyBard,
		CharlieCleric,
		MabelMage,
		SusanShapeShifter,
		WalterWizard,
		PamelaPaladin,
		Goose,
        ViceChancellor,
        Mike,
        Robot,
        Shrub,
		Gorrila
        
	}   

	public static int bonusHealth = 0;
	public static int bonusAttack = 0;

	/// <summary>
	/// [CHANGE] - Remove feature setting Bobby Bard to half health and beta testers found it confusing and wasn't necessary to
	/// distinguish his character type
	/// </summary>
	public static CombatCharacter MakeCharacter(CombatCharacterPresets characterType){
		CombatCharacter newCharacter = null;
		int characterMaxHealth = GetCharacterMaxhealth (characterType);
		int characterMaxEnergy = GetCharacterMaxEnergy (characterType);
		CombatAbility basicAttack = getCharacterBasicAttack (characterType);
		newCharacter = new CombatCharacter(characterMaxHealth, characterMaxHealth, characterMaxEnergy, characterMaxEnergy, basicAttack);
		List<CombatAbility> abilities = GetCharacterAbilities (characterType);
		foreach (CombatAbility ability in abilities) {
			newCharacter.AddAbility (ability);
		}
		newCharacter.combatSprites = getCharacterSprites (characterType);
		return newCharacter;
	}

    /// <summary>
    /// [Extensions Untwo] - added three new characters 
    /// [Extensions Untwo] - added gorrila 
    /// </summary>
    public static string GetCharacterName(CombatCharacterPresets characterType){
		switch (characterType) {
		case CombatCharacterPresets.BobbyBard:
			return "Bobby The Bard";
		case CombatCharacterPresets.CharlieCleric:
			return "Charlie The Cleric";
		case CombatCharacterPresets.MabelMage:
			return "Mabel The Mage";
		case CombatCharacterPresets.PamelaPaladin:
			return "Pamela The Paladin";
		case CombatCharacterPresets.WalterWizard:
			return "Walter The Wizard";
		case CombatCharacterPresets.SusanShapeShifter:
			return "Susan The ShapeShifter";
		case CombatCharacterPresets.Goose:
			return "Goose";
        case CombatCharacterPresets.ViceChancellor:
            return "Vice Chancellor";
        case CombatCharacterPresets.Mike:
            return "Mike";
        case CombatCharacterPresets.Robot:
            return "Robot";
        case CombatCharacterPresets.Shrub:
            return "Shrub";
		case CombatCharacterPresets.Gorrila:
			return "Gorrila";
		}
		return "Character name not defined";
	}

    /// <summary>
    /// [CHANGE] - For all non-enemy types, add bonusHealth to their base stat
    /// </summary>
    /// <returns>The character's maximum health</returns>
    /// [Extensions Untwo] - added three new characters with standard healths
    /// [Extensions Untwo] - added gorrila with health
    /// [changes Untwo] - reduced health of final boss by 100 to create more likely but still chalanging dificulty
    /// <param name="characterType">The character type</param>
    public static int GetCharacterMaxhealth(CombatCharacterPresets characterType){
		switch (characterType) {
		case CombatCharacterPresets.BobbyBard:
			return 70 + bonusHealth;
		case CombatCharacterPresets.CharlieCleric:
			return 100 + bonusHealth;
		case CombatCharacterPresets.MabelMage:
			return 130 + bonusHealth;
		case CombatCharacterPresets.PamelaPaladin:
			return 180 + bonusHealth;
		case CombatCharacterPresets.WalterWizard:
			return 100 + bonusHealth;
		case CombatCharacterPresets.SusanShapeShifter:
			return 100 + bonusHealth;
		case CombatCharacterPresets.Goose:
			return 50;
        case CombatCharacterPresets.ViceChancellor:
            return 400;
        case CombatCharacterPresets.Mike:
            return 300;
        case CombatCharacterPresets.Robot:
            return 130;
        case CombatCharacterPresets.Shrub:
            return 60;
		case CombatCharacterPresets.Gorrila:
			return 200;
        }
		return 1;
	}

    /// <summary>
    /// [Extensions Untwo] - added gorrila 
    /// </summary>
    public static int GetCharacterMaxEnergy(CombatCharacterPresets characterType){
		switch (characterType) {
		case CombatCharacterPresets.BobbyBard:
			return 200;
		case CombatCharacterPresets.CharlieCleric:
			return 150;
		case CombatCharacterPresets.MabelMage:
			return 130;
		case CombatCharacterPresets.PamelaPaladin:
			return 60;
		case CombatCharacterPresets.WalterWizard:
			return 150;
		case CombatCharacterPresets.SusanShapeShifter:
			return 120;
		case CombatCharacterPresets.Gorrila:
			return 200;
		}
		return 1;
	}

    /// <summary>
    /// [Extensions Untwo] - added three new characters with standard attack
    /// [Extensions Untwo] - added gorrila attack
    /// </summary>
	public static List<CombatAbility> GetCharacterAbilities (CombatCharacterPresets characterType){
		List<CombatAbility> abilities = new List<CombatAbility> ();
		switch (characterType) {
		case CombatCharacterPresets.BobbyBard:
			abilities.Add (new SimpleHeal (30, 50, 40, "Basic Heal"));
			abilities.Add( new GiveEffect (new CombatEffect( CombatEffect.effectType.damageDealtModifier, 3, damageType : "melee", modifier : 1.5f), 30, true, "Melee Buff"));
			break;
		case CombatCharacterPresets.CharlieCleric:
			abilities.Add (new SimpleHeal (30, 50, 40, "Basic Heal"));
			abilities.Add (new SimpleHeal (80, 100, 80, "Big Heal"));
			break;
		case CombatCharacterPresets.MabelMage:
			abilities.Add (new SimpleAttack (60, 80, "melee", 40, "Heavy Strike"));
			abilities.Add (new SimpleAttack (65, 75, "magic", 40, "Ethereal Projectile"));
			break;
		case CombatCharacterPresets.PamelaPaladin:
			abilities.Add (new SimpleAttack (60, 80, "melee", 40, "Heavy Strike"));
			abilities.Add (new SimpleAttack (80, 100, "melee", 80, "Epic Strike"));
			break;
		case CombatCharacterPresets.WalterWizard:
			abilities.Add (new SimpleAttack (60, 70, "fire", 30, "Fireball"));
			abilities.Add (new SimpleAttack (65, 100, "electric", 70, "Lightning Strike"));
			break;
		case CombatCharacterPresets.SusanShapeShifter:
			abilities.Add (new SelfEffect (new CombatEffect (CombatEffect.effectType.damageTakenModifier, 3, damageType : "melee", modifier : 0.2f), 40, "Skin of Steel"));
			abilities.Add (new SelfEffect (new CombatEffect (CombatEffect.effectType.damageDealtModifier, 3, damageType : "melee", modifier : 4f), 40, "Knives For Hands"));
			break;
		case CombatCharacterPresets.Goose:
			break;
        case CombatCharacterPresets.ViceChancellor:
            break;
        case CombatCharacterPresets.Mike:
            break;
        case CombatCharacterPresets.Robot:
            break;
        case CombatCharacterPresets.Shrub:
            break;
		case CombatCharacterPresets.Gorrila:
			abilities.Add (new GorrilaAttack (80, 100, "melee", 30, "Barrels"));
			abilities.Add (new GorrilaAttack (150, 170, "melee", 30, "Wild Swings"));
			break;
		}
		return abilities;
	}

    /// <summary>
    /// [EXTENSION] - Add bonusAttack to max and min stats for any non enemy tpes
    /// [Extensions Untwo] - added gorrila attack
    /// </summary>
    /// <returns>The character's basic attack</returns>
    /// <param name="characterType">The character type</param>
    public static CombatAbility getCharacterBasicAttack(CombatCharacterPresets characterType){
		switch (characterType) {
            case CombatCharacterPresets.ViceChancellor:
                return new SimpleAttack(60, 120, "melee", 0);
		case CombatCharacterPresets.Goose:
			return new SimpleAttack (20, 30, "melee", 0);
        case CombatCharacterPresets.Mike:
            return new SimpleAttack(100, 110, "melee", 0);
        case CombatCharacterPresets.Robot:
            return new SimpleAttack(30, 50, "melee", 0);
        case CombatCharacterPresets.Shrub:
            return new SimpleAttack(30, 40, "melee", 0);
		case CombatCharacterPresets.Gorrila:
			return new GorrilaAttack (100, 120, "melee", 0, "Arms");
        default:
			return new SimpleAttack (20 + bonusAttack, 30 + bonusAttack, "melee", 0);
		}
	}

	public static Dictionary<string, List<Sprite>> getCharacterSprites(CombatCharacterPresets characterType){
		Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>> ();
		List<Sprite> frames = new List<Sprite> ();
		switch (characterType) {
		case CombatCharacterPresets.BobbyBard:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 6/F4"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 6/F5"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 6/F6"));
			sprites.Add ("attack", frames);
			break;

		case CombatCharacterPresets.CharlieCleric:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 4/D4"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 4/D7"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 4/D6"));
			sprites.Add ("attack", frames);
			break;
		
		case CombatCharacterPresets.MabelMage:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 2/A4"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 2/A5"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 2/A6"));
			sprites.Add ("attack", frames);
			break;
		
		case CombatCharacterPresets.PamelaPaladin:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 5/E4"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 5/E5"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 5/E6"));
			sprites.Add ("attack", frames);
			break;
		
		case CombatCharacterPresets.SusanShapeShifter:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 3/B4"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 3/B5"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 3/B6"));
			sprites.Add ("attack", frames);
			break;
		
		case CombatCharacterPresets.WalterWizard:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 1/c21"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 1/c4"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Character 1/c6"));
			sprites.Add ("attack", frames);
			break;

		case CombatCharacterPresets.Goose:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite>("Sprites/Goose"));
			sprites.Add ("base", frames);
			break;

        case CombatCharacterPresets.ViceChancellor:
            frames = new List<Sprite>();
            frames.Add(Resources.Load<Sprite>("Sprites/ViceChancellor/ViceChancellor"));
            sprites.Add("base", frames);
            frames = new List<Sprite>();
            frames.Add(Resources.Load<Sprite>("Sprites/ViceChancellor/ViceChancellor3"));
            frames.Add(Resources.Load<Sprite>("Sprites/ViceChancellor/ViceChancellor2"));
            sprites.Add("attack", frames);
            break;

            /// [Extensions Untwo] added sprite
            case CombatCharacterPresets.Mike:
                frames = new List<Sprite>();
                frames.Add(Resources.Load<Sprite>("Sprites/Mike"));
                sprites.Add("base", frames);
                break;

            /// [Extensions Untwo] added sprite
            case CombatCharacterPresets.Robot:
                frames = new List<Sprite>();
                frames.Add(Resources.Load<Sprite>("Sprites/Robot"));
                sprites.Add("base", frames);
                break;

            /// [Extensions Untwo] added sprite
            case CombatCharacterPresets.Shrub:
                frames = new List<Sprite>();
                frames.Add(Resources.Load<Sprite>("Sprites/Shrub"));
                sprites.Add("base", frames);
                break;

            /// [Extensions Untwo] added sprite
            case CombatCharacterPresets.Gorrila:
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Gorrila/G1"));
			sprites.Add ("base", frames);
			frames = new List<Sprite> ();
			frames.Add (Resources.Load<Sprite> ("Sprites/Gorrila/G2"));
			frames.Add (Resources.Load<Sprite> ("Sprites/Gorrila/G3"));
			sprites.Add ("attack", frames);
			break;
		}
		return sprites;
	
	}
}

