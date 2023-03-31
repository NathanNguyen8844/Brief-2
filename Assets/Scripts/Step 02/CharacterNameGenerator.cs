using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Create Names
/// - Set Individual Name
/// - Set Team Character Names
/// </summary>
public class CharacterNameGenerator : MonoBehaviour
{
 
    [Header("Possible first names")]
    private List<string> firstNames = new List<string>(); // a list of all possible first names for us to use.
    [Header("Possible last names")]
    private List<string> lastNames = new List<string>(); // a list of all possible last names for us to use.
    [Header("Possible nicknames")]
    private List<string> nickNames = new List<string>(); // a list of all possible nick names for us to use.

    public CharacterName character;
    
    private void Awake()
    {
        // call the create names function
        CreateNames();
        SetIndividualCharacter(character);
    }

    /// <summary>
    /// Creates a list of names for all our characters to potentiall use.
    /// Step 02: Called when we press play.
    /// Step 03: Called when we press play.
    /// </summary>
    public void CreateNames()
    {
        // adding first names
        firstNames.Add("John");
        firstNames.Add("Jane");
        firstNames.Add("Dwayne");
        firstNames.Add("Jack");
        firstNames.Add("Bob");

        // adding last names
        lastNames.Add("Nguyen");
        lastNames.Add("Smith");
        lastNames.Add("Doe");
        lastNames.Add("Adams");
        lastNames.Add("Allen");

        //adding nicknames
        nickNames.Add("Rock");
        nickNames.Add("Hulk");
        nickNames.Add("Smasher");
        nickNames.Add("Brawler");
        nickNames.Add("Legend");

    }

    /// <summary>
    /// set a characters name to a random name
    /// Step 02: Called when we press the test step 02 button and sets each dancer to have a name.
    /// </summary>
    /// <param name="character"></param>
    public void SetIndividualCharacter(CharacterName character)
    {
        // So here rather than each character being called Blanky Blank Blank, we probably want it to be a random first,last and nickname
        character.firstName = firstNames[Random.Range(0,firstNames.Count)];
        character.lastName = lastNames[Random.Range(0,lastNames.Count)];
        character.nickName = nickNames[Random.Range(0,nickNames.Count)];

        /*Debug.Log(character.firstName);
        Debug.Log(character.lastName);
        Debug.Log(character.nickName);*/
    }

    /// <summary>
    /// Same as an individual...but this time it's more than one dancer.
    /// Step 03: Called from the Spawn function.
    /// </summary>
    /// <param name="namesNeeded"></param>
    /// <returns></returns>
    public void SetTeamCharacterNames(List<CharacterName> teamCharacters)
    {
        // so here we have a list of character names coming in.
        // we should probably loop over that list of charcter names, and then for each chacter set thei first, last and nickname a random one from our lists
        // if you want to get fancy you could use another function within this script to help out here.
        

    }
}