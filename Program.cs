using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Math_Game
{
    internal class Program
    {
        // Global Variables
        static int score;
        static bool gameStarted;
        static int totalQuestions;
        static int difficulty = 1;
        static bool hasDecimals = false;


        static void Main(string[] args)
        {
            StartGame();
            Menu();


        } // Main

        // Game States ( different options the variables can be in during the game )
        public static void StartGame()
        {
            gameStarted = true;
            score = 0;
            totalQuestions = 0;
            difficulty = 1;
        } // StartGame()

        public static void Menu()
        {

            while(gameStarted)
            {
                Console.Clear();
                Console.WriteLine("Math Game-------");
                UpdateInformation();
                Console.WriteLine("1 - Addition");
                Console.WriteLine("2 - Subtraction");
                Console.WriteLine("3 - Multiplcation");
                Console.WriteLine("4 - Division");
                Console.WriteLine($"5 - Change Difficulty: {difficulty}");
                Console.WriteLine($"6 - Has Decimals: {hasDecimals}");
                Console.WriteLine("7 - Exit: ");
                Console.Write("Enter your choice: ");
                int userChoice = (int)UserResponse();
                double[] values = Difficulty(difficulty);
                switch (userChoice)
                {
                    
                    case 1:
                        AdditionQuestion(values);
                        break;
                    case 2:
                        SubtractionQuestion(values);
                        break;
                    case 3:
                        MultiplicationQuestion(values);
                        break;
                    case 4:
                        DivisionQuestion(values);
                        break;
                    case 5:
                        ChangeDifficulty();
                        break;
                    case 6:
                        ChangeDecimals();
                        break;
                    case 7:
                        gameStarted = false;
                        break; 
                }

            }


            Console.ReadKey();
        } // GenerateQuestion()

        public static void ChangeDecimals()
        {
            hasDecimals = !hasDecimals;
        } 

        public static void ChangeDifficulty()
        {
            Console.WriteLine("Enter a new difficulty: ");
            Console.WriteLine("1 - Easy: 1 - 10");
            Console.WriteLine("2 - Medium: 1 - 100");
            Console.WriteLine("3 - Hard: 1 - 1000");

            int userInput = (int)UserResponse();

            if(userInput == 2)
            {
                difficulty = 2;
            }
            else if (userInput == 3)
            {
                difficulty = 3;
            }
            else
            {
                difficulty = 1;
            }
            
            Difficulty(difficulty);


        }

        public static double[] Difficulty(int difficulty)
        {
            switch(difficulty)
            {
                case 2:
                    return GenerateTwoNumbers(1, 101, hasDecimals);
                case 3:
                    return GenerateTwoNumbers(1, 1001, hasDecimals);
                default:
                    return GenerateTwoNumbers(1, 11, hasDecimals);
            }
        }

        public static string FormatQuestion(double[] values, string sign)
        {
            return $"{values[0]} {sign} {values[1]}";

        } // Format Question

        public static void AdditionQuestion(double[] values)
        {

            double value1 = values[0];
            double value2 = values[1];

            double answer = Add(value1, value2);

            string formattedQuestion = FormatQuestion(values, "+");

            Console.WriteLine(formattedQuestion);

            Console.WriteLine("Enter your answer: ");
            double usersAnswer = UserResponse();

            CompareAnswers(usersAnswer, answer);


            Console.WriteLine();
        } // AdditionQuestion()

        public static void SubtractionQuestion(double[] values)
        {

            double value1 = values[0];
            double value2 = values[1];

            double answer = Subtract(value1, value2);

            string formattedQuestion = FormatQuestion(values, "-");

            Console.WriteLine(formattedQuestion);

            Console.WriteLine("Enter your answer: ");
            double usersAnswer = UserResponse();

            CompareAnswers(usersAnswer, answer);


            Console.WriteLine();
        } // SubtractionQuestion()

        public static void MultiplicationQuestion(double[] values)
        {

            double value1 = values[0];
            double value2 = values[1];

            double answer = Multiplcation(value1, value2);

            string formattedQuestion = FormatQuestion(values, "*");

            Console.WriteLine(formattedQuestion);

            Console.WriteLine("Enter your answer: ");
            double usersAnswer = UserResponse();

            CompareAnswers(usersAnswer, answer);


            Console.WriteLine();
        } // MultiplicationQuestion()

        public static void DivisionQuestion(double[] values)
        {

            double value1 = values[0];
            double value2 = values[1];

            double answer = Divide(value1, value2);

            string formattedQuestion = FormatQuestion(values, "/");

            Console.WriteLine(formattedQuestion);

            Console.WriteLine("Enter your answer: ");
            double usersAnswer = UserResponse();

            CompareAnswers(usersAnswer, answer);


            Console.WriteLine();
        } // DivisionQuestion()

        public static double UserResponse()
        {
            string stringAnswer = Console.ReadLine();

            double userNumber;
            bool isNumber = double.TryParse(stringAnswer, out userNumber);

            while(!isNumber)
            {
                Console.WriteLine("Please enter a valid number.");
                isNumber = double.TryParse(Console.ReadLine(), out userNumber);
            }

            return userNumber;
        } // User Response

        public static void ConsoleColorChange(ConsoleColor text, ConsoleColor background)
        {
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;
        } // ConsoleColorChange

        public static void ConsoleColorDefault()
        {
            ConsoleColorChange(ConsoleColor.White, ConsoleColor.Black);
        } // ConsoleColorDefault

        public static void ConsoleColorCorrect()
        {
            ConsoleColorChange(ConsoleColor.Black, ConsoleColor.Green);
        } // ConsoleColorCorrect

        public static void ConsoleColorIncorrect()
        {
            ConsoleColorChange(ConsoleColor.Black, ConsoleColor.Red);
        } // ConsoleColorIncorrect

        public static void CompareAnswers(double userAnswer, double correctAnswer)
        {
            bool wereTheAnswersTheSame = userAnswer == correctAnswer;


            if(wereTheAnswersTheSame)
            {
                CorrectAnswer(correctAnswer);
            }
            else
            {
                IncorrectAnswer(correctAnswer);
            }

            ConsoleColorDefault();
            totalQuestions++;
            Console.ReadKey();
        } // CompareAnswers

        public static void CorrectAnswer(double userAnswer)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($" Congratulations! {userAnswer} was the correct answer.");
            score++;
        } // CorrectAnswer

        public static void IncorrectAnswer(double correctAnswer)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($" The correct answer is {correctAnswer} ");
        } // IncorrectAnswer

        public static double[] GenerateTwoNumbers(int low, int high, bool hasDecimals)
        {
            Random rand = new Random();
            double value1 = rand.Next(low, high);
            double value2 = rand.Next(low, high);



            if(hasDecimals)
            {
                value1 += Math.Round(rand.NextDouble(), 2);
                value2 += Math.Round(rand.NextDouble(), 2);
            }

            double[] numbers = { value1, value2 };

            return numbers;

        } // GenerateTwoNumbers

        public static void UpdateInformation()
        {
            Console.WriteLine($"Current Score: {score}");
            Console.WriteLine($"Number of Questions: {totalQuestions}");

            if (totalQuestions != 0 && score != 0)
            {
                double percentageCorrect = Math.Round(((double)score / totalQuestions) * 100, 2);
                Console.WriteLine($"Percentage Correct: { percentageCorrect }%");

            }
        } // UpdateInformation

        public static double Add(double num1, double num2)
        {
            return num1 + num2;
        } // Add

        public static double Subtract(double num1, double num2)
        {
            return num1 - num2;
        } // Subtract

        public static double Multiplcation(double num1, double num2)
        {
            return num1 * num2;
        } // Multiplication

        public static double Divide(double num1, double num2)
        {
            return num1 / num2;
        } // Divide

    } // class

} // namespace
