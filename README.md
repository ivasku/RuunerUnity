# RunnerUnity
Infinite Runner (PC &amp; Mobile)

**Windows build located in the Build directory.**
**Android build (APK) located in the androidBuild directory.**

Developed with Unity3d 2017.4.1f1 version.

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
the gameObject but you reuse the objects from the object pool and when you don't need it anymore you just deactivate it.
The usage is shown in the `TileManager` class.
- **c**: UX - Player has running animation and some obstacles like spikes. There are also transitions in the UI menu like fading.
- **d**: It supports Windows, Linux and Android platforms. Not tested on IOS but is should work there too.
Controlls on mobile devices are divided in the regions of the screen. When you tap on the left part of the screen you move left,
on the upper part of the screen you jump.

Packages utilized from the Unity Asset Store:
------------
- **a**: Low Poly Street Pack (https://assetstore.unity.com/packages/3d/environments/urban/low-poly-street-pack-67475)
- **b**: Free Animated Space Man (https://assetstore.unity.com/packages/3d/characters/humanoids/free-animated-space-man-61548)
- **c**: Simple UI (https://assetstore.unity.com/packages/2d/gui/icons/simple-ui-103969)
- **d**: Skybox (https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-4183)
- **e**: 100+ Magic Particle Effects (https://assetstore.unity.com/packages/vfx/particles/spells/100-magic-particle-effects-23515)

All assets are free from the store.

Assets utilized from other sources:
------------
- **a**: Road image (https://pixabay.com/en/sand-sky-apocalypse-cloud-horizon-2876796/)
- **b**: Assets created by `Stefan Subin`: spikes 3d object,

All assets are free for comercial use, no attribution required
