namespace Engine.Core.Enums;

[Flags]
public enum ComponentType : long
{
    // 64 different components are possible with bitmask 0-63
    None = 0,
    Transform = 1L << 0,
    Sprite = 1L << 1,
    Health = 1L << 2,
}