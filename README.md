# North-Coast-Attack
## Overview
**North Coast Attack** is a first-person shooter set during the events of World War II.
This project was created as a way to test the implementation of enemy controlls and navigation using the `state machine` approach, as well as the implementation of several programming and optimization patterns such as `SingleTone` and `ObjectPool`.

The playable character finds himself on the beach of the first location ("level") and must proceed deeper into the area to encouter various enemies, as well as lootable items. The path consists mainly of 3 separate locations ("level") to complete the game.
The player`s mission is to reach the final location and face the final "boss" of the game. 

## Controls
* The player is moved by pressing the keys on the keyboard - forward, backward, right, left (`WASD`, respectively).
* The shot is made by pressing the "Shoot" key (`LMB`) on the mouse. 
* Aiming is performed by pressing the "Aim" key (`RMB`) on the mouse.

## Concept and Outline
* The player is roleplaying as a soldier whose commited to slay his enemies. Aim at enemies to kill them.

* In each level, the player will encounter enemies of varying strength and elite enemies (`bosses`) at the end of each level.
  To access each of the following locations (levels), the elite enemy of the current location ("boss") has to be defeated, and all enemies of the current location are slain 

* Weapons can be modified by finding weapon modifications (`weapon mods`) placed in various locations across the area, enhancing certain weapon characteristics. 
  The best weapons are obtained after killing elite enemies or hidden in the surrounding area.

* The player can restore his health by finding health restoration means (`med kits`).
* Supply packs (`ammo kits`) found in different places of the locations will replanish ammunition.

## Additional "Core Mechanics"
1. Mouse buttons are used to aim and shoot, the keyboard keys are used to move.
2. During the game, the player can switch between different types of weapons and swap modifications for the current weapon
3. The player can aim to shoot, increasing the accuracy
4. The damage is dealt if the aimsight is pointed at the enemy and the "shoot" key is pressed at the same time
5. After killing an enemy, their weapon could be dropped on the ground
6. Elite enemies always drop improved weapons
7. Items are located in a certain way in the area, making it difficult to find them.
8. During the game, the player can save the current game progress after completing a level.

## Items
* `Med kit` item
* `Ammo kit` item
* `Weapon` item
* `Weapon Mod` item

## Win/Loss Conditions 
* **WIN** - all enemies, including bosses are required to be defeated in all of the locations ("levels"). After slaying the "boss" of the final location the victory is achieved
* **LOSS** - the player character dies (the number of health points drops to `0`)

## UI/UX 
* The player's current health level is displayed. After inflicting damage to the player, the health level visually decreases, after using the "med kit" item, it visually increases. 
* The amount of ammunition is displayed. After each shot, the ammunition counter decreases, after using the "ammo kit" item, it increases. 

* Enemy health level is displayed above their head.  

* In the `Main Menu` there is an option to select the localization language (**English** by default). 

* All registered hits to the enemy and the player are accompanied by visual feedback and sound effects 

## Game Loop
-> Killing enemies on the location using distinct weaponry as well as exploring the area for loot -> 
Encountering an elite enemy, aka "boss" ->
Defeating a "boss" ->
Moving to next level -> 
