# Purpose
It serves as an easy way to transition into a new scene

# Implementation
- I create a base class `TransitionScene` that derives from `ISceneBase`, then I add on extra properties and add functionality that handles scene loading with transition
- Because I want to use coroutines as a way to transition, I have to make this class a `MonoBehaviour`, which means I have to automatically create a new instance when I want to load a scene

## Scene load / unload
- This will create a coroutine that processes the transition