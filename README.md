# North-Coast-Attack
## Overview
North Coast Attack is a first-person shooter set during the events of World War II.
This project was created as a way to test the implementation of enemy controlls and navigation using the state machine approach, as well as the implementation of several programming and optimization patterns such as SingleTone or ObjectPool.

The player finds himself on the beach of the first location (level) and must move deeper into the location to kill all enemies. The path consists mainly of 3 levels (locations) to complete the game.
To win, the player has to defeat all enemies throughout the locations

## Controls
The player moves by pressing the keys on the keyboard (forward, backward, right, left, WASD, respectively).
The shot is made by pressing the "shot" key (LKM) on the mouse. 
Aiming is performed by pressing the "Aim" key (PCM) on the mouse

## Concept and Outline
The player plays as a soldier who kills enemies. 
The player's task is to aim at enemies to kill them.
In each level, the player will encounter enemies of varying strength and elite enemies ("bosses") at the end of each level.
To access each next location (level), both conditions must be met: the elite enemy of the current location (boss) is destroyed, and all enemies of the current location are destroyed 
The player can upgrade his weapon by finding weapon modifications placed in various locations. 
The player can restore his health by finding health restoration means ("first aid kits") placed in different places of the locations.
While exploring the area, the player will come across various weapons and upgrades for them, ammunition for weapons, and health restorers. 
The best weapons are obtained after killing elite enemies or in hidden places in the locations.

## Core Mechanics
The player uses the mouse buttons to aim and shoot, and the keyboard keys to move.
During the game, the player can switch between different types of weapons 
The player can aim while shooting, which increases the accuracy of the hit 
The player can deal damage to enemies if the reticle is pointed at the enemy and the "shot" key is pressed at the same time 
The player can restore part of the health if he finds and uses the "first aid kit" 
The player can replenish the amount of ammunition by finding and using the "ammunition" item 
The player can improve weapon damage by finding and using the "modification" item 
A random weapon can "fall out" of a killed enemy 
Elite enemies always drop improved weapons 
Items are located in a certain way in the location, which should make it difficult to find them 
During the game, the player can save the progress of the game only after completing the location.
The player has a default inventory with two cells for two different types of weapons, which the player can carry and equip at any time. 

## Items
"First aid kit" item
Item "Modification"
"Ammunition" item
Item "Weapon" 

## Win/Loss Conditions
The player must first defeat all enemies at each location, then the "boss" at the location, and only then can he move to the next location or complete the game (for the last location) 
To win the game, the player must defeat all enemies, including bosses, in all locations. 
The player loses the game if the character dies (the number of health points is "0")

## UI/UX 
The screen displays the player's health level. After inflicting damage to the player, the health level visually decreases, after using the "first aid kit" item, it visually increases. 
The screen displays the amount of ammunition. After each shot, the ammunition counter decreases, after using the "ammunition" item, it increases. 
The health level of enemies is displayed above the enemy's head.  
In the "Main Menu" there is an option to select the localization language. 
All hits to the enemy and the player should be accompanied by visual feedback and/or sound effects 
