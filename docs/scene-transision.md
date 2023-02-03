# Purpose of having a scene handler
Rather than using unity's scene changing directly, I created a class `GlobalSceneHandler` which handles all scene changing stuff

# Functionality
## Loading a scene
- The enum `Scene` will automatically choose which scene to load
- You can load scenes as additional scenes, or overwrite loaded scenes

## Pausing a scene
- If you have a loaded scene, you can "pause" everything in it
- There's a resume function too

## Temporary unload a scene
- You can unload a scene so it's invisible