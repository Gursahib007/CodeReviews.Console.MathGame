using System;
using System.Data;
using System.Threading;


public class Transaction
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public char Operation { get; set; }
    public int Result { get; set; }
    public int Answer { get; set; }
    public int Points { get; set; }

    public Transaction(int number1, int number2, char operation, int result, int points, int inputNum1)
    {
        Number1 = number1;
        Number2 = number2;
        Operation = operation;
        Result = result;
        Points = points;
        Answer = inputNum1;
    }
    public override string ToString()
    {
        return @$"
>{Number1} {Operation} {Number2}:
        >Your solution : {Answer}
        >Actual solution : {Result}
        
        >Score : {Points}";
    }
}
public static class GameMenu
{
    public static void WelcomeMenu()
    {
        string text = "=-=-=-WELCOME TO MATH GAME 101-=-=-=";
        CenterText(text);

        text = "The calculator can perform Additions, Subtractions, Divisions, and Multiplications. CAN YOU?";
        CenterText(text);

        text = "For every correct answer, you get 1 point. For a wrong one, you lose 1 point.";
        CenterText(text);

        Console.WriteLine("\n");

        text = "Press Enter key to continue";
        CenterText(text);
        Console.ReadLine();
     

        Console.Clear();

        
        text = "Loading";
        CenterText(text);
        LoadingAnimation();
    }

    public static void EndMenu()
    {
        string text = "=-=-=-THANKS FOR PLAYING-=-=-=";
        CenterText(text);

        int finalScore = GamePlayer.FinalScore();

        text = $"FINAL SCORE: {finalScore}";
        CenterText(text);

        text = "Press Enter key to continue";
        CenterText(text);
        Console.ReadLine();
    }

    private static void CenterText(string text)
    {
        if (text.Length <= Console.WindowWidth)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
        }
        Console.WriteLine(text);
    }

    private static void LoadingAnimation()
    {
        for (int i = 0; i < 6; i++)
        {
            Console.Write("=-=-= *** =-=-= *** ");
            Thread.Sleep(750);
        }
        Console.WriteLine("\n\n");
    }
}

public static class GamePlayer
{
    static List<Transaction> transactionHistory = new List<Transaction>();
    private static int points;
    
    public static int Choose()
    {
        Console.Clear();
        char choice = '0';
        Console.WriteLine(@"         Choose an Operation:
     ===========================
     |       1. Addition       |
     |       2. Subtraction    |
     |       3. Multiplication |
     |       4. Division       |
     |       5. View History   |  
     |       0. Exit           |
     ===========================");
        Console.Write("\n\nEnter your choice in the form on equivalent character [+], [-], [*], [/], [5 for history], [0 for exit] : ");


        string? input = Console.ReadLine();
        choice = !string.IsNullOrEmpty(input) ? input[0] : '\0';

        switch (choice)
        {
            case '+':
                GameStage('+');
                break;

            case '-':
                GameStage('-');
                break;

            case '*':
                GameStage('*');
                break;

            case '/':
                GameStage('/');
                break;

            case '0':
                return 1;


            case '5':
                Console.Clear();
                Console.WriteLine("Transaction History:");
                foreach (var transaction in transactionHistory)
                {
                    Console.WriteLine(transaction);
                }
                Console.WriteLine("\n\nPress Enter key to continue");
                Console.ReadLine();
                Console.Clear();
                break;

            default:
                Console.WriteLine("Input Character is Invalid, Try again");
                Console.ReadLine();
                return 0;
            }
        return 0;
    }

    private static void GameStage(char choice)
    {
        Console.Clear();

        Random random = new Random();

        int num1, num2, inputNum1, result;
        inputNum1 = result = 0;

        do
        {
            num1 = random.Next(1, 99);
            num2 = random.Next(1, 99);
        } while (num1 % num2 != 0);

        Console.WriteLine($"> Answer the following expression: {num1} {choice} {num2}");

        Console.Write("\n\n> Input solution  ");
        Console.Write("Please enter a number:");
        string? userInput = Console.ReadLine();

        if (int.TryParse(userInput, out inputNum1))
        {
            Console.WriteLine($"> You entered: {inputNum1}");
        }
        else
        {
            Console.WriteLine("Invalid number. Please try again.");
            Console.ReadLine();
            return;
        }

        Thread.Sleep(175);

        switch (choice)
        {
            case '+':
                result = num1 + num2;
                break;

            case '-':
                result = num1 - num2;
                break;

            case '*':
                result = num1 * num2;
                break;

            case '/':
                result = num1 / num2;
                break;
        }
        if (inputNum1 == result)
        {
            Console.WriteLine("\n\n> Correct!, You have gained +1 point");
            points++;
            Transaction newTransaction = new Transaction(num1, num2, choice, result, points, inputNum1);
            transactionHistory.Add(newTransaction);

            Console.WriteLine("\n\n> Press Enter to Continue.");
            Console.ReadLine();
            Choose();
            
        }
    
        else
        {
            Console.WriteLine("\n\n> BZZZ Wrong Answer, You have lost 1 point");
            Console.WriteLine("\n\n> Correct answer was: " + result);
            points--;
            Transaction newTransaction = new Transaction(num1, num2, choice, result, points, inputNum1);
            transactionHistory.Add(newTransaction);

            Console.WriteLine("\n\n> Press Enter to Continue.");
            Console.ReadLine();
            Choose();
        }
    }
    public static int FinalScore()
    {
        return points;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int flag = 0;


        GameMenu.WelcomeMenu();
        Console.Clear();
        do
        {
            flag = GamePlayer.Choose();
        } while (flag != 1);

        Console.Clear();
        GameMenu.EndMenu();
    }
}
