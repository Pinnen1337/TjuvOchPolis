﻿using System;

namespace Tjuv_Polis;

public class Prison
{
    public int HorisontalWallLength { get; set; }
    public int VerticalWallLength { get; set; }
    public List<Person> PersonsInPrison { get; set; }
    public int StartDrawPrisonAt { get; set; }

    public City CityNextToPrison { get; set; }

    public Prison(int horisontalSize, int verticalSize, List<Person> personsInPrison, City city)


    {
        HorisontalWallLength = horisontalSize;
        VerticalWallLength = verticalSize;
        PersonsInPrison = personsInPrison;
        StartDrawPrisonAt = city.HorisontalWallLength;
    

    }
    // TODO we need a method that adds a person to the PersonsInPrison list and also sets the correct positions and most importantly the correct Space properties.
    public void Move()
    {
        foreach (Person person in PersonsInPrison)
        {
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

            int newXPosition = person.XPosition + person.MovementX;
            int newYPosition = person.YPosition + person.MovementY;

            if (newXPosition < StartDrawPrisonAt + 2)
            {
                newXPosition = StartDrawPrisonAt + person.HorizontalSpace - 2;
            }
            if (newYPosition < 2)
            {
                newYPosition = person.VerticalSpace + 1;
            }
            if (newYPosition >= person.VerticalSpace + 2)
            {
                newYPosition = 2;
            }
            if (newXPosition >= StartDrawPrisonAt + person.HorizontalSpace - 1)
            {
                newXPosition = StartDrawPrisonAt + 2;
            }

            person.XPosition = newXPosition;
            person.YPosition = newYPosition;

            // Rita personen på nya positionen
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.ForegroundColor = person.Color;
            Console.Write(person.Symbol);
            Console.ResetColor();

        }
    }

    public void DrawPrison()
    {
        DrawTopWall();
        DrawLowerWall();
        DrawWalls();
    }

    private void DrawTopWall()
    {
        Console.SetCursorPosition(StartDrawPrisonAt + 1, 0);
        Console.WriteLine("Prison");
        Console.SetCursorPosition(StartDrawPrisonAt + 1, 1);
        for (int i = 0; i < HorisontalWallLength; i++)
        {
            Console.Write("=");
        }
    }
    private void DrawLowerWall()
    {
        Console.SetCursorPosition(StartDrawPrisonAt + 1, VerticalWallLength + 2);
        for (int i = 0; i < HorisontalWallLength; i++)
        {
            Console.Write("="); //Golv
        }
    }

    private void DrawWalls()
    {
        int cursorPositionLeft = StartDrawPrisonAt + 1;
        int cursorPositionTop = 2;
        for (int wall = 0; wall < VerticalWallLength; wall++)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
            Console.Write("░");  //Vänster vägg
            Console.SetCursorPosition(cursorPositionLeft + HorisontalWallLength - 1, cursorPositionTop);
            Console.WriteLine("░"); //Höger vägg
            cursorPositionTop++;
        }
    }
    public void ReleasePrisoners()
    {
        List<Person> prisonersToRelease = new List<Person>();

        foreach (Person prisoner in PersonsInPrison)
        {
            if (prisoner is Thief thief && thief.DoneTheTime())
            {
                Console.SetCursorPosition(thief.XPosition, thief.YPosition);
                Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

                thief.XPosition = Random.Shared.Next(2, CityNextToPrison.HorisontalWallLength - 1);
                thief.YPosition = Random.Shared.Next(2, CityNextToPrison.VerticalWallLength); // Sätt en startposition inom city

                thief.HorizontalSpace = CityNextToPrison.HorisontalWallLength;
                thief.VerticalSpace = CityNextToPrison.VerticalWallLength;

                thief.IsArrested = false;

                prisonersToRelease.Add(prisoner);
            }
        }
        CityNextToPrison.PersonsInCity.AddRange(prisonersToRelease);
        foreach (Thief thief in prisonersToRelease)
        {
            PersonsInPrison.Remove(thief);
        }
    }
}
