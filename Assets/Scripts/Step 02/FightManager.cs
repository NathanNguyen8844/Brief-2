﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - DecideWinner
/// </summary>
public class FightManager : MonoBehaviour
{
    public Color drawCol = Color.gray; // A colour you might want to set the battle log message to if it's a draw.
    private float fightAnimTime = 2; //An amount to wait between initiating the fight, and the fight begining, so we can see some of that sick dancing.
    private Coroutine fightRoutine; // stores a reference to the current battle routine running.
    private BattleSystem battleSystem; // stores a reference to the battle system in our scene.
    private Color teamAColour; // stores the current team colour, if there is no team then it will defeault to red for A, and Blue for B
    private Color teamBColour;
    private string checkWinner;
    string battleMessage;

    /// <summary>
    /// This function takes in two characters and we need to decide who wins the fight.
    /// Step 02: Called when the test step 02 button is pressed and passes in the two dancers from the test implementation function.
    /// Step 03: Called from the BattleSystem once it selects two random characters.
    /// </summary>
    /// <param name="teamACharacter"></param>
    /// <param name="teamBCharacter"></param>
    private void DecideWinner(Character teamACharacter, Character teamBCharacter)
    {
        // so we are storing our two power levels of each of our characters.
        int playerOnePowerLevel = teamACharacter.myPowerSystem.ReturnMyDancePowerLevel();
        int playerTwoPowerLevel = teamBCharacter.myPowerSystem.ReturnMyDancePowerLevel();

        //Debug.Log(playerOnePowerLevel);
        //Debug.Log(playerTwoPowerLevel);

        // We should probably determine here who has won, and who has lost by comparing their power levels.
        
        if (playerOnePowerLevel > playerTwoPowerLevel) {
            checkWinner = "PlayerOne";
        } else if (playerTwoPowerLevel > playerOnePowerLevel) {
            checkWinner = "PlayerTwo";
        }

        // we should also do some damage or heal the appropriate characters.
        if (checkWinner == "PlayerOne") {
            teamACharacter.myStatsSystem.ChangeHealth(0.2f);
        } else if (checkWinner == "PlayerTwo") {
            teamBCharacter.myStatsSystem.ChangeHealth(0.2f);
        } else {
            teamACharacter.myStatsSystem.ChangeHealth(0.1f);
            teamBCharacter.myStatsSystem.ChangeHealth(0.1f);
        }

        // we could also give them some XP if we want to.
        // so we have the character class, which means any variables,references and functions we can access.
        // By default it will automatically be a draw.

        if (checkWinner == "PlayerOne") {
            battleMessage = teamACharacter.charName.GetFullCharacterName() + " " + teamBCharacter.charName.GetFullCharacterName() + "," + teamBCharacter.charName.GetFullCharacterName() + " wins!";
        } else if (checkWinner == "PlayerTwo") {
            battleMessage = teamACharacter.charName.GetFullCharacterName() + " " + teamBCharacter.charName.GetFullCharacterName() + "," + teamACharacter.charName.GetFullCharacterName() + " wins!";
        } else {
            battleMessage = teamACharacter.charName.GetFullCharacterName() + " " + teamBCharacter.charName.GetFullCharacterName() + " fight is a draw";
        }
        
        // Logs out the message to our console         
        BattleLog.Log(battleMessage, drawCol);
        BattleLog.Log("team A draw", teamAColour);
        BattleLog.Log("team B draw", teamBColour);

        // here we are just telling the system who has won, and who has lost; for any other result other than a draw we should probably pass in false.
        FightCompleted(teamBCharacter, teamACharacter, true);
    }


    #region No Modifications Required.

    ///// <summary>
    ///// Sets up a dancer to be selected and the animation to start dancing.
    ///// </summary>
    ///// <param name="dancer"></param>
    private void SetUpAttack(Character dancer)
    {
        dancer.isSelected = true;
        dancer.GetComponent<AnimationController>().Dance();
        if (dancer.myTeam != null)
        {
            BattleLog.Log(dancer.charName.GetFullCharacterName() + " Selected", dancer.myTeam.teamColor);
        }
    }

    /// <summary>
    /// When a character is chosen, let them know they are selected, and make them dance.
    /// </summary>
    /// <param name="character"></param>
    /// <param name="teamColor"></param>
    private void DisplaySelected(Character character, Color teamColor)
    {
        character.isSelected = true;
        character.animController.Dance();
        if (character != null)
        {
            BattleLog.Log(character.charName.GetFullCharacterName() + " Selected", teamColor);
        }
    }

    /// <summary>
    /// just checks to see if they've been assigned a team, if yes then use their color;
    /// otherwise just set A to be red, and B to be blue.
    /// </summary>
    /// <param name="teamA"></param>
    /// <param name="teamB"></param>
    private void GetTeamColours(Character teamA, Character teamB)
    {
        if(teamA.myTeam)
        {
            teamAColour = teamA.myTeam.teamColor;
        }
        else
        {
            teamAColour = Color.red;
        }

        if(teamB.myTeam)
        {
            teamBColour = teamB.myTeam.teamColor;
        }
        else
        {
            teamBColour = Color.blue;
        }
    }

    /// <summary>
    /// This is called when we want to start a fight between two charcters it then intiialises the corouinte.
    /// </summary>
    /// <param name="teamACharacter"></param>
    /// <param name="teamBCharacter"></param>
    public void Fight(Character teamACharacter, Character teamBCharacter)
    {
        if (fightRoutine == null)
        {
            GetTeamColours(teamACharacter, teamBCharacter);
            // only start a new fight if the previous is completed.
            fightRoutine = StartCoroutine(StartFight(teamACharacter, teamBCharacter));
        }
    }

    /// <summary>
    /// This handles the actual round and them fighting etc, setting up animations etc.
    /// </summary>
    /// <param name="teamACharacter"></param>
    /// <param name="teamBCharacter"></param>
    /// <returns></returns>
    private IEnumerator StartFight(Character teamACharacter, Character teamBCharacter)
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            // just chill and wait for the input from the player
            yield return null;
        }

        DisplaySelected(teamACharacter, teamAColour);
        DisplaySelected(teamBCharacter, teamBColour);

        SetUpAttack(teamACharacter);
        SetUpAttack(teamBCharacter);

        yield return new WaitForSeconds(fightAnimTime);

        DecideWinner(teamACharacter, teamBCharacter);


        Debug.Log("Next Round Starting");

        if (!battleSystem)
        {
            // if we don't have a battle system ref, find one.
            battleSystem = FindObjectOfType<BattleSystem>();
        }

        if (battleSystem)
        {
            battleSystem.FightOver();
        }

        yield return null;
    }

    /// <summary>
    /// Passes in a winning character, and a defeated character, as well as the outcome 
    /// </summary>
    /// <param name="winner"></param>
    /// <param name="defeated"></param>
    /// <param name="outcome"></param>
    private void FightCompleted(Character winner, Character defeated, bool isDraw)
    {
        winner.isSelected = false;
        defeated.isSelected = false;
        fightRoutine = null;
        
        winner.animController.BattleResult(winner, defeated, isDraw? 0:1);
        defeated.animController.BattleResult(winner, defeated, isDraw ? 0 : 1);
    }

    public void TestImplementation()
    {
        Character[] allCharacter = FindObjectsOfType<Character>();
        // here assume we just find 2.
        Fight(allCharacter[0], allCharacter[1]);
    }

     #endregion  
}
