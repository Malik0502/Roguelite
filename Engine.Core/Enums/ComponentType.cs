namespace Engine.Core.Enums;

[Flags]
public enum ComponentType : long
{
    // 64 different components are possible with bitmask 0-63
    Health = 0,
    Transform = 1L << 0
}