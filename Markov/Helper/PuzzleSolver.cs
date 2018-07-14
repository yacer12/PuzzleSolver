using Markov.Models.Puzzle;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class handles all the logic related to the convenient way of solving a word search puzzle.
/// </summary>
public static class PuzzleSolver
{
    public static PuzzleResponse SolvePuzzle(char[,] matrix, string word)
    {
        string wordResponse = string.Empty;
        PuzzleResponse puzzleResponse = new PuzzleResponse();
        bool isSolved = false;
        PuzzleResponse pr = new PuzzleResponse();
        if (!isSolved)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {      
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    char c = word.ToCharArray()[0];
                    if (c == matrix[i, j])
                    {
                        //Search on Left to Right on Horizontal
                        if ((pr = GetHorizontalRight(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                           // }
                                break;
                            }
                        }
                        //Search on Left to Right on Horizontal
                        if ((pr = GetHorizontalLeft(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }
                        //Search on the Vertical way, from top to down
                        if ((pr = GetVerticalDown(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }
                        //Search on the Vertical way, from top to down
                        if ((pr = GetVerticalUp(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }
                        //Search on the left diagonal on down direction
                        if ((pr = GetDiagonalDownLeft(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;

                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }
                        //Search on the left diagonal on down direction
                        if ((pr = GetDiagonalUpLeft(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }

                        //Search on the right diagonal on down direction
                        if ((pr = GetDiagonalUpRight(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }
                        //Search on the right diagonal on down direction
                        if ((pr = GetDiagonalDownRight(word, i, j, matrix)) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(pr.Word))
                            {
                                isSolved = true;
                                puzzleResponse = pr;
                                //Console.WriteLine(pr.Word);
                                //foreach (var item in pr.Breakdown)
                                //{
                                //    Console.WriteLine(item.Row + "," + item.Column);
                                //}
                                break;
                            }
                        }
                    }
                }
                if (isSolved)
                    break;
            }
        }
        return puzzleResponse;
    }
    #region Private Methods
    private static PuzzleResponse GetHorizontalRight(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int height = array.GetLength(1);
                int width = array.GetLength(0);
                char cTemp = word.ToCharArray()[i];
                if (y > 0
                    && y < height)

                {
                    if (array[x, y] == cTemp)
                    {
                        coords.Add(new Breakdown()
                        {
                            Character = array[x, y],
                            Row = x,
                            Column = y
                        });

                        sBuilder.Append(array[x, y]);
                        y++;
                    }
                    else
                        return null;
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    private static PuzzleResponse GetHorizontalLeft(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int height = array.GetLength(1);
                int width = array.GetLength(0);
                char cTemp = word.ToCharArray()[i];
                if (y > 0 && y < height)
                {
                    if (array[x, y] == cTemp)
                    {
                        coords.Add(new Breakdown()
                        {
                            Character = array[x, y],
                            Row = x,
                            Column = y
                        });

                        sBuilder.Append(array[x, y]);
                        y--;
                    }
                    else
                        return null;
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        //  Console.WriteLine(sBuilder.ToString());
        return response;
    }
    private static PuzzleResponse GetVerticalDown(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int height = array.GetLength(1);
                int width = array.GetLength(0);
                char cTemp = word.ToCharArray()[i];
                if (y > 0
                    && y < height
                    && (x + i) < width)
                {
                    if (array[i + 1, y] == cTemp)
                    {
                        coords.Add(new Breakdown()
                        {
                            Character = array[i + 1, y],
                            Row = i + 1,
                            Column = y
                        });
                        sBuilder.Append(array[i, y]);
                    }
                    else
                        return null;
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    private static PuzzleResponse GetVerticalUp(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int height = array.GetLength(1);
                int width = array.GetLength(0);
                char cTemp = word.ToCharArray()[i];

                if (y > 0
                    && x - 1 > 0
                    && y < height
                    && (x - i) < width)
                {
                    if (array[x - i, y] == cTemp)
                    {
                        coords.Add(new Breakdown()
                        {
                            Character = array[x - i, y],
                            Row = x - i,
                            Column = y
                        });
                        sBuilder.Append(array[i, y]);
                    }
                    else
                        return null;
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    private static PuzzleResponse GetDiagonalDownRight(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int yy = y + i;
                if (yy > 0
                    && yy < array.GetLength(1)
                    && (x + i) < array.GetLength(0))
                {
                    coords.Add(new Breakdown()
                    {
                        Character = array[x + i, y + i],
                        Row = x + i,
                        Column = y + i
                    });
                    sBuilder.Append(array[x + i, y + i]);
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    private static PuzzleResponse GetDiagonalUpRight(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int yy = y + 1;
                if (yy > 0
                    && x > 0
                    && y < array.GetLength(1)
                    && x < array.GetLength(0))
                {
                    coords.Add(new Breakdown()
                    {
                        Character = array[x, y],
                        Row = x,
                        Column = y
                    });
                    sBuilder.Append(array[x, y]);
                    x--;
                    y++;
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    private static PuzzleResponse GetDiagonalDownLeft(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int yy = y - i;
                if (yy > 0
                    && yy < array.GetLength(1)
                    && (x + i) < array.GetLength(0)
                    && (x + i) < array.GetLength(0))
                {
                    coords.Add(new Breakdown()
                    {
                        Character = array[x + i, y - i],
                        Row = x + i,
                        Column = y - i
                    });
                    sBuilder.Append(array[x + i, y - i]);
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    private static PuzzleResponse GetDiagonalUpLeft(string word, int x, int y, char[,] array)
    {
        List<Breakdown> coords = new List<Breakdown>();
        int length = word.Length;
        bool isFound = false;
        PuzzleResponse response = new PuzzleResponse();
        StringBuilder sBuilder = new StringBuilder();
        if (!isFound)
        {
            for (int i = 0; i < length; i++)
            {
                int yy = y - i;
                if (yy > 0
                    && x > 0
                    && yy < array.GetLength(1)
                    && (x + i) < array.GetLength(0)
                    && (x + i) < array.GetLength(0))
                {
                    coords.Add(new Breakdown()
                    {
                        Character = array[x, y],
                        Row = x,
                        Column = y
                    });
                    sBuilder.Append(array[x, y]);
                    x--;
                    y--;
                }
            }
            if (sBuilder.ToString().Equals(word))
            {
                isFound = true;
                response.Word = sBuilder.ToString();
                response.Breakdown = coords;
            }
        }
        return response;
    }
    #endregion
}