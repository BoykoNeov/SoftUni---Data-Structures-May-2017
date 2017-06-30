using System;
using System.Linq;

public class PitFortressPlayground
{
    public static void Main()
    {
        PitFortressCollection pitFort = new PitFortressCollection();
        pitFort.AddPlayer("pencho", 77);
        pitFort.AddPlayer("pencho2", 2);
        pitFort.AddPlayer("pencho3", 3);
        pitFort.AddPlayer("pencho4", 4);
        pitFort.playersByName["pencho"].Score = 5;
        pitFort.playersByName["pencho2"].Score = 6;
        pitFort.playersByName["pencho3"].Score = 7;

        pitFort.playersByName["pencho4"].Score = 8;

        var a = pitFort.Top3PlayersByScore();
        var b = pitFort.Min3PlayersByScore();

        pitFort.AddMinion(5);
        pitFort.AddMinion(6);
        pitFort.AddMinion(7);
        pitFort.AddMinion(8);
        pitFort.AddMinion(8);
        pitFort.AddMinion(4);

        var c = pitFort.ReportMinions();

        pitFort.SetMine("pencho", 1, 2, 100);
        pitFort.SetMine("pencho", 1, 1, 80);
        pitFort.SetMine("pencho", 1, 1, 100);

        pitFort.SetMine("pencho", 0, 4, 100);
        pitFort.SetMine("pencho", 0, 1, 100);

        var d = pitFort.GetMines().ToList();

        PitFortressCollection pit2 = new PitFortressCollection();
        pit2.AddPlayer("Poncho", 77);
        pit2.SetMine("Poncho", 77, 77, 88);
        pit2.SetMine("Poncho", 77, 77, 77);
        pit2.SetMine("Poncho", 77, 77, 77);

        var mines = pit2.GetMines().ToList();

        Console.ReadLine();


    }
}
