using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048Solver
{
    public struct Coord2D
    {
        public int x;
        public int y;
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public interface IGameBoard : IEquatable<GameBoard>
    {
        bool GameOver { get; }
        int MaxValue { get; }
        bool Move(Direction direction);
        int Moves { get; }
        int Score { get; }
        string ToString();
        int GetBoardLength();
        IEnumerable<IGameBoard> GetAllPossibleResults(Direction direction);
    }

    public class GameBoard : IGameBoard
    {
        private const int BOARD_LENGTH = 4;
        private const int SPAWN_NO = 2;

        private Random rand;
        private int[,] board;

        public int MaxValue { get; private set; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public int Moves { get; private set; }

        public GameBoard()
            : this(new Random())
        {
        }

        public GameBoard(Random rand)
        {
            this.rand = rand;
            GameOver = false;
            MaxValue = SPAWN_NO;
            Score = 0;
            Moves = 0;

            board = new int[BOARD_LENGTH, BOARD_LENGTH];

            AddNewSquare();
            AddNewSquare();
        }

        public GameBoard(GameBoard gb)
            : this(gb, new Random())
        {
        }

        public GameBoard(GameBoard gb, Random rand)
        {
            this.rand = rand;

            GameOver = gb.GameOver;
            MaxValue = gb.MaxValue;
            Score = gb.Score;
            Moves = gb.Moves;

            var length = gb.GetBoardLength();
            board = new int[length, length];
            for (int x = 0; x < length; x++)
                for (int y = 0; y < length; y++)
                {
                    int value = gb.board[x, y];
                    board[x, y] = value;
                }
        }

        private void AddNewSquare()
        {
            SetSquare(GetRandomEmptySquare(), SPAWN_NO);
        }

        private Coord2D GetRandomEmptySquare()
        {
            List<Coord2D> emptySpaces = new List<Coord2D>();

            for (int x = 0; x < BOARD_LENGTH; x++)
                for (int y = 0; y < BOARD_LENGTH; y++)
                    if (board[x, y] == 0)
                        emptySpaces.Add(new Coord2D() { x = x, y = y });

            if (emptySpaces.Count == 0)
                throw new InvalidOperationException("Cannot get random empty square when board is full");

            return emptySpaces[rand.Next(emptySpaces.Count)];
        }

        private void SetSquare(Coord2D coord, int value)
        {
            SetSquare(coord.x, coord.y, value);
        }

        private void SetSquare(int x, int y, int value)
        {
            board[x, y] = value;
        }

        private int GetSquare(Coord2D coord)
        {
            return GetSquare(coord.x, coord.y);
        }

        private int GetSquare(int x, int y)
        {
            return board[x, y];
        }

        public bool Move(Direction direction)
        {
            var stateChange = ApplyMove(direction);

            if (stateChange)
            {
                AddNewSquare();
                UpdateMaxScore();
                CheckForGameOver();
                Moves++;
            }

            return stateChange;
        }

        private bool ApplyMove(Direction direction)
        {
            bool stateChange = false;

            switch (direction)
            {
                case Direction.Down:
                    stateChange = Down();
                    break;

                case Direction.Up:
                    stateChange = Up();
                    break;

                case Direction.Left:
                    stateChange = Left();
                    break;

                case Direction.Right:
                    stateChange = Right();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("TILT! How did you get here?");
            }

            return stateChange;
        }

        private bool Left()
        {
            bool modification = false;

            for (int y = 1; y < BOARD_LENGTH; y++)
            {
                for (int x = 0; x < BOARD_LENGTH; x++)
                {
                    int valLeftOf = GetSquare(x, y - 1);

                    if (valLeftOf == 0)
                    {
                        SetSquare(x, y - 1, GetSquare(x, y));
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                    else if (valLeftOf == GetSquare(x, y))
                    {
                        int value = GetSquare(x, y) * 2;
                        Score += value;
                        SetSquare(x, y - 1, value);
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                }
            }

            return modification;
        }

        private bool Right()
        {
            bool modification = false;

            for (int y = BOARD_LENGTH - 2; y > 0; y--)
            {
                for (int x = BOARD_LENGTH - 1; x > 0; x--)
                {
                    int valRightOf = GetSquare(x, y + 1);

                    if (valRightOf == 0)
                    {
                        SetSquare(x, y + 1, GetSquare(x, y));
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                    else if (valRightOf == GetSquare(x, y))
                    {
                        int value = GetSquare(x, y) * 2;
                        Score += value;
                        SetSquare(x, y + 1, value);
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                }
            }

            return modification;
        }

        private bool Up()
        {
            bool modification = false;

            for (int y = 0; y < BOARD_LENGTH; y++)
            {
                for (int x = 1; x < BOARD_LENGTH; x++)
                {
                    int valAboveOf = GetSquare(x - 1, y);

                    if (valAboveOf == 0)
                    {
                        SetSquare(x - 1, y, GetSquare(x, y));
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                    else if (valAboveOf == GetSquare(x, y))
                    {
                        int value = GetSquare(x, y) * 2;
                        Score += value;
                        SetSquare(x - 1, y, value);
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                }
            }

            return modification;
        }

        private bool Down()
        {
            bool modification = false;

            for (int y = BOARD_LENGTH - 1; y > 0; y--)
            {
                for (int x = BOARD_LENGTH - 2; x > 0; x--)
                {
                    int valDownOf = GetSquare(x + 1, y);

                    if (valDownOf == 0)
                    {
                        SetSquare(x + 1, y, GetSquare(x, y));
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                    else if (valDownOf == GetSquare(x, y))
                    {
                        int value = GetSquare(x, y) * 2;
                        Score += value;
                        SetSquare(x + 1, y, value);
                        SetSquare(x, y, 0);
                        modification = true;
                    }
                }
            }

            return modification;
        }

        private void UpdateMaxScore()
        {
            for (int x = 0; x < BOARD_LENGTH; x++)
                for (int y = 0; y < BOARD_LENGTH; y++)
                    if (GetSquare(x, y) > MaxValue)
                        MaxValue = GetSquare(x, y);
        }

        private void CheckForGameOver()
        {
            for (int x = 0; x < BOARD_LENGTH; x++)
                for (int y = 0; y < BOARD_LENGTH; y++)
                {
                    var currentSquareValue = GetSquare(x, y);
                    if (currentSquareValue == 0)
                        return;

                    if (x > 0 && currentSquareValue == GetSquare(x - 1, y))
                        return;

                    if (y > 0 && currentSquareValue == GetSquare(x, y - 1))
                        return;
                }

            GameOver = true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < BOARD_LENGTH; i++)
                sb.Append(" -------");

            sb.Append(" \n\n");

            for (int x = 0; x < BOARD_LENGTH; x++)
            {
                for (int y = 0; y < BOARD_LENGTH; y++)
                {
                    string val = GetSquare(x, y).ToString();
                    sb.Append("| " + val);
                    sb.Append("      ".Substring(val.Length));
                }                

                sb.Append("|\n\n");

                for (int y = 0; y < BOARD_LENGTH; y++)
                    sb.Append(" -------");
                sb.Append(" \n\n");
            }

            return sb.ToString();

        }

        public int GetBoardLength()
        {
            return BOARD_LENGTH;
        }

        public IEnumerable<IGameBoard> GetAllPossibleResults(Direction direction)
        {
            GameBoard testBoard = new GameBoard(this);

            if (!testBoard.ApplyMove(direction))
                yield break;

            foreach (var emptySquare in testBoard.GetAllEmptySquares())
            {
                testBoard.SetSquare(emptySquare, SPAWN_NO);
                yield return testBoard;
            }
        }

        private IEnumerable<Coord2D> GetAllEmptySquares()
        {
            for (int x = 0; x < BOARD_LENGTH; x++)
                for (int y = 0; y < BOARD_LENGTH; y++)
                    if (GetSquare(x, y) == 0)
                        yield return new Coord2D() { x = x, y = y };
        }

        public bool Equals(GameBoard other)
        {
            if (other == null)
            {
                return false;
            }

            if (GetBoardLength() != other.GetBoardLength())
            {
                return false;
            }

            for (int x = 0; x < BOARD_LENGTH; x++)
                for (int y = 0; y < BOARD_LENGTH; y++)
                    if (this.GetSquare(x, y) != other.GetSquare(x, y))
                        return false;

            return true;
        }
    }
}
