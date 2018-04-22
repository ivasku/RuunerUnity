# RunnerUnity
Infinite Runner (PC &amp; Mobile)


Work In progress , documentation will be completed.



Game has the following features:
------------
- **a**: Player supports left-right and jumping movement.
- **b**: Obstacles (static obstacles on the road, dynamic (spikes) and holes in the road where you can fall).
- **c**: It has MainMenu scene with UI and HighScore information. When you die you have in-Game menu to restart the game, go back to 
MainMenu scene and score information.
- **d**: Powerups - TODO

Game contains following implementations:
------------
- **a**: Procedural level generation. There are about 10 types of roads that are random generated during runtime.
- **b**: Pooling/Recycling of level objects. According to the player position, the road tiles are generated/deleted. Also there is
a Pool manager object that instantiates the object at the start of the game and during the game you never instantiate/delete
the gameObject but you reuse the objects from the object pool and when you don`t need it anymore you just deactivate it.
The usage is shown in the TileManager class.
- **c**: UX - Player has running animation and some obstacles like spikes. There are also transitions in the UI menu like fading.
- **d**: It supports Windows, Linux and Android platforms. Not tested on IOS but is should work there too.

Packages utilized from the Unity Asset Store:
