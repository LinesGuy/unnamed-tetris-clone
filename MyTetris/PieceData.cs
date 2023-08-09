using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace MyTetris {
    public static class PieceData {
        // [PieceID, Orientation]
        public static string[,] Tiles = {
            { "0000111100000000", "0010001000100010", "0000000011110000", "0100010001000100" }, // I
            { "1000111000000000", "0110010001000000", "0000111000100000", "0100010011000000" }, // J
            { "0010111000000000", "0100010001100000", "0000111010000000", "1100010001000000" }, // L
            { "0110011000000000", "0110011000000000", "0110011000000000", "0110011000000000" }, // O
            { "0110110000000000", "0100011000100000", "0000011011000000", "1000110001000000" }, // S
            { "0100111000000000", "0100011001000000", "0000111001000000", "0100110001000000" }, // T
            { "1100011000000000", "0010011001000000", "0000110001100000", "0100110010000000" }  // Z
        };
        public static Color[] Colours = {
            new Color(0, 255, 255),
            new Color(0, 0, 255),
            new Color(255, 128, 0),
            new Color(255, 255, 0),
            new Color(0, 255, 0),
            new Color(128, 0, 255),
            new Color(255, 0, 0)
        };
        // X CW (Y) -> Starting orientation X, ClockWise, (Ending orientation Y). Note CCW = CounterClockWise
        private static Point[][] _wallkick_data_jltsz = {
            new Point[]{ new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0,-2), new Point(-1,-2) }, // 0 CCW (3)
            new Point[]{ new Point(0, 0), new Point( 1, 0), new Point( 1, 1), new Point(0,-2), new Point( 1,-2) }, // 0 CW  (1)
            new Point[]{ new Point(0, 0), new Point( 1, 0), new Point( 1,-1), new Point(0, 2), new Point( 1, 2) }, // 1 CCW (0)
            new Point[]{ new Point(0, 0), new Point( 1, 0), new Point( 1,-1), new Point(0, 2), new Point( 1, 2) }, // 1 CW  (2)
            new Point[]{ new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0,-2), new Point(-1,-2) }, // 2 CCW (1)
            new Point[]{ new Point(0, 0), new Point( 1, 0), new Point( 1, 1), new Point(0,-2), new Point( 1,-2) }, // 2 CW  (3)
            new Point[]{ new Point(0, 0), new Point(-1, 0), new Point(-1,-1), new Point(0, 2), new Point(-1, 2) }, // 3 CCW (2)
            new Point[]{ new Point(0, 0), new Point(-1, 0), new Point(-1,-1), new Point(0, 2), new Point(-1, 2) }  // 3 CW  (0)
        };
        private static Point[][] _wallkick_data_i = {
            new Point[]{ new Point(0, 0), new Point(-2, 0), new Point( 1, 0), new Point(-2,-1), new Point( 1, 2) }, // 0 CCW (3)
            new Point[]{ new Point(0, 0), new Point(-1, 0), new Point( 2, 0), new Point(-1, 2), new Point( 2,-1) }, // 0 CW  (1)
            new Point[]{ new Point(0, 0), new Point( 2, 0), new Point(-1, 0), new Point( 2, 1), new Point(-1,-2) }, // 1 CCW (0)
            new Point[]{ new Point(0, 0), new Point(-1, 0), new Point( 2, 0), new Point(-1, 2), new Point( 2,-1) }, // 1 CW  (2)
            new Point[]{ new Point(0, 0), new Point( 1, 0), new Point(-2, 0), new Point( 1,-2), new Point(-2, 1) }, // 2 CCW (1)
            new Point[]{ new Point(0, 0), new Point( 2, 0), new Point(-1, 0), new Point( 2, 1), new Point(-1,-2) }, // 2 CW  (3)
            new Point[]{ new Point(0, 0), new Point(-2, 0), new Point(-1, 0), new Point(-2,-1), new Point( 1, 2) }, // 3 CCW (2)
            new Point[]{ new Point(0, 0), new Point( 1, 0), new Point(-2, 0), new Point( 1,-2), new Point(-2, 1) }  // 3 CW  (0)
        };
        /// <summary>
        /// Gets the array of kicks to try for the given piece id, starting orientation (0-3) and direction of rotation (-1 or 1).
        /// </summary>
        /// <param name="pieceId"></param>
        /// <param name="startOrientation"></param>
        /// <param name="rotationDirection"></param>
        /// <returns>Array of points representing offsets.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static Point[] WallkickData(int pieceId, int startOrientation, int rotationDirection) {
            if (rotationDirection == -1) rotationDirection = 0; // stupid hack don't ask
            switch (pieceId) {
                case 0: // I piece
                    return _wallkick_data_i[startOrientation * 2 + rotationDirection];
                case 3: // O Piece
                    throw new NotImplementedException("O piece kick table not yet implemented");
                case 1: // Everything else
                case 2:
                case 4:
                case 5:
                case 6:
                    return _wallkick_data_jltsz[startOrientation * 2 + rotationDirection];
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
