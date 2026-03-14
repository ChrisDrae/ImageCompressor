namespace imageCompressor;

public static class AppState
{
#pragma warning disable CA2211 // Non-constant fields should not be visible
    public static volatile bool Paused = false;
#pragma warning restore CA2211 // Non-constant fields should not be visible
}
