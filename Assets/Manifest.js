/*

----------------------------------------------Truffle and Caffeine----------------------------------------------
                                                PROJECT MANIFEST
                                                ----------------

  Project Name:Vees
  Version:Debug
  Bundle indentifier(Package name):com.magicswipe.vees
  Platforms:Android

  Instructions for Devs
  ---------------------
  1)Creating new levels
    Create a new level,Add levelName and levelNumber in gameManager
  2)EnemySpawner,PlayerSpawner,TouchManager and GameManager are critical scripts,Editing only done by the core team
  3)Every level should have critical scripts
  4)Never change level build number,Add new levels to end indices

  Architecture documentation
  --------------------------
  Script:Game Manager

    Most Critical script
    Core game manager

    Functions
    1)Score reseting,incrementing,fetching
    2)Life reseting,decrementing,fetching & Set life to an integer
    3)Set life to 5(for all levels),If life is zero gameOver gets triggered()
    4)Set score
    5)Manage gameOver Level,like setting score
    6)Check for game over
    7)Manage Life Display
    8)Get level name


    Script: Touch Manager

    Script that deals with TouchInput
    Critical Script
   
     Functions
     1)Calculate swipeVelocity
     2)Check for a minimum swipe distance
     3)Set drag properties to each level


     Script : PlayerSpawner

    //Critical script
    //Responsible for generating player vees
    //Spawn vees for each level
    //TODO:Functional refactoring

    Script : EnemySpawner

    //Critical Script
    //Spawns enemy according to each level properties
    //TODO:FUNCTIONAL REFACTORING

    Script : GoalDetector
     //Increment score if player hits'em or decrease score if enemy hits'em

     Script : PlayerFormation
     Destroy enemies colliding with player except for levels FAST_ESCAPE

     Script : ScoreBoard
     //Set scoreboard for gameOver level

     Script: MusicPlayer
     //Music player,Splash screen sound independent

     Script : LevelManager
     //Deals with splash screen and loading of other levels

     Script : FadeIn
     //UI CODE ONLY
    //PURPOSE:FADING EFFECT

    Script : ScreenManager
    Determines screen dimensions

    Script : Destroyer
    //Destroy anything that comes its way
    //Just a code to organize gamespace

----------------------------------------------Truffle and Caffeine----------------------------------------------
*/