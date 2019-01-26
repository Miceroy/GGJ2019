// Create new enums to use objects in GameController for Day / Night cycle logic.

public enum GameBaseIDEnum
{
    None = 0, // Not in use / do not add into GameController.

    DEBUG_EnableHologramTablet = 998,
    DEBUG_DissolveTestObject = 999,
}

public enum GameBaseExtraFunctionality
{
    None = 0,

    Hologram = 1, // Enables other objects to appear as holographic if they are place top of this.
}