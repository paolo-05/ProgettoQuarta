# Forest Runner: Endless Adventures

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/demo2.png">

### Project overview

It's game, made using Unity Engine. 
It consists of a player running endlessly. 
In the game world there are spawned the land tiles, they contain coins, obstacles, enemies and more. 
By collecting coins, you can upgrade your gun which is used for shooting at the enemies, you can upgrade the bullet speed, the bullet distance range, the gun fire rate and also the bullet damagae.

### Game controls

For moving the player in the world space you can simply use WASD or the arrow keys and for shooting you can press the left mouse button. It's also possible to crouch with C and jump with the Space key.

### Basic game features

<img src="https://github.com/paolo-05/ProgettoQuarta/blob/master/Demo/demo1.png">

- All enemies have animations.
- The coins are always spinning on the y axis.
- There are two types of obstacle: one that must be jumped over and one where the player must crouch.
- All the upgrades, the coins, the best score are stored in `PlayerPrefs` a Unity database.
- There are two types of sound: Music and SFX, more of that later.
- The UI is simple and user friendly, also full of color.

### Sounds

All the sounds are stored in a script called [AudioManager.cs](https://github.com/paolo-05/ProgettoQuarta/blob/master/Assets/Scripts/AudioManager.cs), which is attached to a Game Object, when the game starts,
all the audio clips are attached to that Game Object.

In the settings, there are 3 sliders, one for the master volume, one for the music and one for the SFX sounds, the user, by interracting with them, can adjust the volume. 
At game re-opening all the setting will be saved. This was made possible by using again the `PlayerPrefs` database.
