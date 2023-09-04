using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyTetris.GamePlay
{
    public class CurrentPiece
    {
        private TetrisGame _game;
        public int Id;
        public int Orientation;
        public int SubTiles; // Used in gravity calculations, 65536 sub-tiles = 1 full tile
        public bool AutoPieceLock = true; // Whether or not pieces will automatically lock after touching the ground for a while
        public int GroundedFrames; // Number of frames the piece has been touching the ground without moving
        public int AppearanceDelay; // The number of frames to wait until spawning the next piece. If this is 0 then the piece is active and can be moved, if this is above 0 then the player will not be able to move any piece.
        public int AutoRepeatDelay; // The number of frames until piece is auto shifted. Resets to DAS upon user pressing left or right. Resets to ARR upon reaching zero. Decrements while left or right is held.
        public int AutoRepeatDirection; // -1 for left, 1 for right
        public Point Position;
        public CurrentPiece(TetrisGame game)
        {
            _game = game;
            SpawnNewPiece();
        }
        public void LockPiece()
        {
            // Locks and places the current piece onto the playfield, adds ARE timer to AppearanceDelay,
            // and calls the playfield's check line clear function (adding line clear delay if so)
            // Note that this assumes the current piece position is valid, this may overwrite other tiles otherwise.
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (PieceData.Tiles[Id, Orientation][x + y * 4] == '0')
                        continue;
                    _game.PlayField.Tiles[Position.X + x, Position.Y + y] = Id;
                }
            }
            if (_game.PlayField.ClearLines())
            {
                AppearanceDelay = _game.LevelManager.LineARE + _game.LevelManager.LineClear;
            }
            else
            {
                AppearanceDelay = _game.LevelManager.ARE;
            }
            if (_game.Hold.JustHeld == true)
            {
                _game.Hold.JustHeld = false;
            }
        }
        /// <summary>
        /// Get the next piece from NextPieces, place it at the top-middle of the playfield with default orientation, and reset some variables.
        /// </summary>
        /// <returns>Whether or not the piece fits as soon as it spawns.</returns>
        /// <param name="fromHold">Whether or not to spawn the piece from hold. If so, ignore NextPieces and IHS.</param>
        public bool SpawnNewPiece(bool fromHold = false)
        {
            if (fromHold)
            {
                Id = _game.Hold.HeldPiece;
            }
            else
            {
                Id = _game.NextPieces.Next();
                // Initial Hold System (IHS)
                if (InputManager.Keyboard.IsKeyDown(Keys.Space))
                {
                    _game.Hold.SwapHold();
                }
            }
            if (Id == 0) {
                Position = new Point(3, 0); // I piece spawns one level higher than the rest of the pieces so it touches the ceiling
            } else {
                Position = new Point(3, 1);
            }
            Orientation = 0;
            GroundedFrames = 0;
            SubTiles = 0;
            AutoRepeatDelay = 0;
            // Initial Rotation System (IRS)
            if (InputManager.Keyboard.IsKeyDown(Keys.Z))
            {
                Orientation = 3;
            }
            if (InputManager.Keyboard.IsKeyDown(Keys.X))
            {
                Orientation = 1;
            }
            // Play SFX of next piece
            Assets.Mino[_game.NextPieces.Pieces.Peek()].Play();
            return CanFit(Position.X, Position.Y, Orientation);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="abs_x"></param>
        /// <param name="abs_y"></param>
        /// <param name="orientation"></param>
        /// <returns>Whether or not the piece fits on the playfield at the given position with the given orientation.</returns>
        public bool CanFit(int abs_x, int abs_y, int orientation)
        {
            for (int y = 0; y < 4; y++)
            {

                for (int x = 0; x < 4; x++)
                {
                    if (PieceData.Tiles[Id, orientation][x + y * 4] == '0')
                        continue;
                    // If this tile is outside of the playfield, it's invalid
                    if (abs_x + x < 0 || abs_x + x >= _game.PlayField.Width || abs_y + y < 0 || abs_y + y >= _game.PlayField.Height)
                        return false;
                    // If this tile collides with an existing tile on the playfield, it's invalid
                    if (_game.PlayField.Tiles[abs_x + x, abs_y + y] != -1)
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Tries to move the piece by the relative amount.
        /// </summary>
        /// <param name="rel_x"></param>
        /// <param name="rel_y"></param>
        /// <returns>Whether or not the move was successful.</returns>
        public bool TryMove(int rel_x, int rel_y)
        {
            if (CanFit(Position.X + rel_x, Position.Y + rel_y, Orientation))
            {
                Position.X += rel_x;
                Position.Y += rel_y;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Tries to rotate the piece by the given direction. If the basic rotation fails, a kick table is used.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Whether or not the rotation was successful, including if the kick table was used.</returns>
        public bool TryRotate(int direction)
        {
            // For rotations, we use a kick table
            // The first kick in each row of kicks is always a "basic rotation", which is no kick at all
            Point[] kicks = PieceData.WallkickData(Id, Orientation, direction);

            int new_orientation = Orientation + direction;
            if (new_orientation < 0) new_orientation += 4;
            if (new_orientation >= 4) new_orientation -= 4;

            foreach (Point kick in kicks)
            {
                // Note that we add kick X but subtract kick Y
                // This is because we use positive Y = down but SRS kick tables use positive Y = up
                if (CanFit(Position.X + kick.X, Position.Y - kick.Y, new_orientation))
                {
                    Position.X += kick.X;
                    Position.Y -= kick.Y;
                    Orientation = new_orientation;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Adds the current gravity level according to the game's speed level and moves the piece down the appropriate number of tiles (which may be none)
        /// </summary>
        private void ApplyGravity()
        {
            SubTiles += _game.LevelManager.Gravity;
            while (SubTiles >= 65536)
            {
                SubTiles -= 65536;
                TryMove(0, 1);
            }
        }
        public void Update(GameTime gameTime)
        {
            // Handle user inputs for DAS/ARR
            if (InputManager.WasKeyJustDown(Keys.Left))
            {
                AutoRepeatDirection = -1;
                AutoRepeatDelay = _game.LevelManager.DAS;
            }
            if (InputManager.WasKeyJustDown(Keys.Right))
            {
                AutoRepeatDirection = 1;
                AutoRepeatDelay = _game.LevelManager.DAS;
            }

            if (InputManager.Keyboard.IsKeyDown(Keys.Left) || InputManager.Keyboard.IsKeyDown(Keys.Right))
            {
                AutoRepeatDelay--;
            }
            else
            {
                AutoRepeatDelay = _game.LevelManager.DAS; // Actually it doesn't matter what this value is as long as it's above zero
            }

            // Check appearance delay
            if (AppearanceDelay > 0)
            {
                AppearanceDelay--;
                // If we just now hit 0 frames then we increment the level and spawn a new piece
                if (AppearanceDelay == 0)
                {
                    if (_game.LevelManager.AutoIncrement) {
                        _game.LevelManager.Increase(1);
                    }
                    SpawnNewPiece();
                }
                return;
            }

            if (InputManager.WasKeyJustDown(Keys.Left))
            {
                if (TryMove(-1, 0)) {
                    GroundedFrames = 0;
                }
            }

            if (InputManager.WasKeyJustDown(Keys.Right))
            {
                if (TryMove(1, 0)) {
                    GroundedFrames = 0;
                }
            }

            if (AutoRepeatDelay <= 0)
            {
                if (_game.LevelManager.ARR == 0)
                {
                    // In the specific case that ARR is 0, just move the piece to the side until it hits something
                    while (TryMove(AutoRepeatDirection, 0)) { }
                }
                else
                {
                    TryMove(AutoRepeatDirection, 0);
                    AutoRepeatDelay = _game.LevelManager.ARR;
                }
            }

            if (InputManager.WasKeyJustDown(Keys.Z)) // CCW rotation
                TryRotate(-1);
            if (InputManager.WasKeyJustDown(Keys.X)) // CW rotation
                TryRotate(1);

            ApplyGravity();
            // Check if we are touching the ground
            if (!CanFit(Position.X, Position.Y + 1, Orientation) && AutoPieceLock) {
                GroundedFrames++;
                if (GroundedFrames >= _game.LevelManager.Lock) {
                    Assets.PieceLock.Play();
                    LockPiece();
                    return;
                }
            }

            if (InputManager.Keyboard.IsKeyDown(Keys.Down)) // Soft drop
                TryMove(0, 1);
            if (InputManager.WasKeyJustDown(Keys.Up))// Hard drop
            {
                while (TryMove(0, 1)) { }
                Assets.PieceLock.Play();
                LockPiece();
            }
            if (InputManager.WasKeyJustDown(Keys.Space))
            { // Hold
                _game.Hold.SwapHold();
            }
        }
        public void Draw(SpriteBatch spriteBatch, bool ghost = true)
        {
            if (AppearanceDelay > 0) return;
            // Draw ghost
            if (ghost)
            {
                int gy = 0;
                while (CanFit(Position.X, Position.Y + gy + 1, Orientation))
                {
                    gy++;
                }
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (PieceData.Tiles[Id, Orientation][x + y * 4] == '0')
                            continue;
                        spriteBatch.Draw(Assets.BlockW[Id], Utils.RectangleF(_game.PlayField.Offset + new Vector2(x + Position.X, y + gy + Position.Y) * TetrisGame.TILE_SIZE, new Vector2(TetrisGame.TILE_SIZE)), Color.White * 0.3f);

                    }
                }
            }
            // Draw actual piece
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (PieceData.Tiles[Id, Orientation][x + y * 4] == '0')
                        continue;
                    spriteBatch.Draw(Assets.BlockW[Id], Utils.RectangleF(_game.PlayField.Offset + new Vector2(x + Position.X, y + Position.Y) * TetrisGame.TILE_SIZE, new Vector2(TetrisGame.TILE_SIZE)), Color.White);
                }
            }
        }
    }
}
