# ❓ Forest Runner: Endless Adventures

> A simple game made in Unity Engine for a totally topic free school project.

- You can play the game [here](https://simmer.io/@paoloBianchessi/forest-runner-endless-adventures)
- If you are my professor and you wanto see the docs, those are [here](https://paolo-05.github.io/ProgettoQuarta/)

# 🔎 Project overview

It consists of a player running endlessly. 
In the game world there are spawned the land tiles, they contain coins, obstacles, enemies and more. 
By collecting coins, you can upgrade your gun which is used for shooting at the enemies, you can upgrade the bullet speed, the bullet distance range, the gun fire rate and also the bullet damagae.

# 🕹️ Game controls

For moving the player in the world space you can simply use WASD or the arrow keys and for shooting you can press the left mouse button. It's also possible to crouch with C and jump with the Space key.

# 🔥 Basic game features

- The player is fully animated
- All enemies have animations.
- The coins are always spinning on the y axis.
- There are two types of obstacle: one that must be jumped over and one where the player must crouch.
- All the upgrades, the coins, the best score are stored in `PlayerPrefs` a Unity database.
- There are two types of sound: Music and SFX, more of that later.
- The UI is simple and user friendly, also full of color.

# 🎶 Sounds

All the sounds are stored in a script called [AudioManager.cs](https://github.com/paolo-05/ProgettoQuarta/blob/master/Assets/Scripts/AudioManager.cs), which is attached to a Game Object, when the game starts,
all the audio clips are attached to that Game Object.

In the settings, there are 3 sliders, one for the master volume, one for the music and one for the SFX sounds, the user, by interracting with them, can adjust the volume. 
At game re-opening all the setting will be saved. This was made possible by using again the `PlayerPrefs` database.

# 📰 Following, there are some screenshots of the game.

## Starting screen
  <img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/Screenshot%202023-05-19%20104834.png">
  
## Settings

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/Screenshot%202023-05-19%20104929.png">

## The purchase buttons, available before the game starts

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/Screenshot%202023-05-19%20104955.png">

## Actual game screenshot

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/Screenshot%202023-05-19%20105034.png">

## The game over panel

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/Screenshot%202023-05-19%20105057.png">

## The pause panel

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/Screenshot%202023-05-19%20105122.png">
