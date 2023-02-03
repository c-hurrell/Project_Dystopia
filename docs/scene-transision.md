# Purpose of having a scene handler
Rather than using unity's scene changing directly, I created a class `SceneHandler` which handles all scene changing stuff

# Functionality
## Background scenes
- Functions that takes the `Background` enum will automatically load that background as an additional scene

## Battle scene
- You can transition to all of the game's battle scenes, and give this function the transition effect coroutine in order to choose your own transition effect
- Because how after the battle ends, we must resume the normal gameplay from where it got cut off, I need to have a way that doesn't unload the main scene, but keeps it "inactive in the background"
  - This I can solve with a base "world" game object class called `WorldObject`, which has extra functionality to help the main scene objects to pause while in battle scene