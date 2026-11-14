using UnityEngine;

public enum CardinalDirectionEnum 
{
    //0000 main bits+ padding 00
    Any = 0b000000,
    North = 0b000100,
    North_West = 0b001000,
    North_East = 0b001100,

    West = 0b010000,
    East = 0b010100,

    South = 0b011000,
    South_West = 0b011100,
    South_East = 0b100000,
}
