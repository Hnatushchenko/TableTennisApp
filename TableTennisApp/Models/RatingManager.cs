using ConsoleApp2;
using System;

public class Class1
{
	public Class1()
	{
		static void RatingCount(Player PlayerWhoWon,Player PlayerWhoLost)
		{
			PlayerWhoWon.Rating += 30;
			PlayerWhoLost.Rating -= 27;
			
		}

	}
}
