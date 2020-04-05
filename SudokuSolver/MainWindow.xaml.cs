using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            if (CheckManualSolution())
            {
                Console.WriteLine("Solved!");
            }
            else
            {
                Console.WriteLine("Try Again...");
            }
        }

        private void ButtonSolve_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (index % 100000 == 0)
                {
					Console.WriteLine("Attempt: " + index + "   Time: " + stopwatch.Elapsed);
                }

                guess.Clear();
                AttemptSolution();
                index++;
            } while (!CheckAutoSolution());

            int i = 0;

            foreach (UIElement element in wrapPanel.Children)
            {
                if (element is TextBox textBox)
                {
                    textBox.Text = guess[i].ToString();
                    i++;
                }
            }

            stopwatch.Stop();
        }

        private bool CheckManualSolution()
        {
            int[] horizontalLine0 = { N(A0), N(B0), N(C0), N(D0), N(E0), N(F0), N(G0), N(H0), N(I0) };
            int[] horizontalLine1 = { N(A1), N(B1), N(C1), N(D1), N(E1), N(F1), N(G1), N(H1), N(I1) };
            int[] horizontalLine2 = { N(A2), N(B2), N(C2), N(D2), N(E2), N(F2), N(G2), N(H2), N(I2) };
            int[] horizontalLine3 = { N(A3), N(B3), N(C3), N(D3), N(E3), N(F3), N(G3), N(H3), N(I3) };
            int[] horizontalLine4 = { N(A4), N(B4), N(C4), N(D4), N(E4), N(F4), N(G4), N(H4), N(I4) };
            int[] horizontalLine5 = { N(A5), N(B5), N(C5), N(D5), N(E5), N(F5), N(G5), N(H5), N(I5) };
            int[] horizontalLine6 = { N(A6), N(B6), N(C6), N(D6), N(E6), N(F6), N(G6), N(H6), N(I6) };
            int[] horizontalLine7 = { N(A7), N(B7), N(C7), N(D7), N(E7), N(F7), N(G7), N(H7), N(I7) };
            int[] horizontalLine8 = { N(A8), N(B8), N(C8), N(D8), N(E8), N(F8), N(G8), N(H8), N(I8) };

            int[][] horizontalLines = { horizontalLine0, horizontalLine1, horizontalLine2 , horizontalLine3, horizontalLine4, horizontalLine5, horizontalLine6, horizontalLine7, horizontalLine8 };

            foreach (int [] line in horizontalLines)
            {
                if (line.Distinct().Count() != 9)
                {
                    return false;
                }
            }

            int[] verticalLine0 = { N(A0), N(A1), N(A2), N(A3), N(A4), N(A5), N(A6), N(A7), N(A8) };
            int[] verticalLine1 = { N(B0), N(B1), N(B2), N(B3), N(B4), N(B5), N(B6), N(B7), N(B8) };
            int[] verticalLine2 = { N(C0), N(C1), N(C2), N(C3), N(C4), N(C5), N(C6), N(C7), N(C8) };
            int[] verticalLine3 = { N(D0), N(D1), N(D2), N(D3), N(D4), N(D5), N(D6), N(D7), N(D8) };
            int[] verticalLine4 = { N(E0), N(E1), N(E2), N(E3), N(E4), N(E5), N(E6), N(E7), N(E8) };
            int[] verticalLine5 = { N(F0), N(F1), N(F2), N(F3), N(F4), N(F5), N(F6), N(F7), N(F8) };
            int[] verticalLine6 = { N(G0), N(G1), N(G2), N(G3), N(G4), N(G5), N(G6), N(G7), N(G8) };
            int[] verticalLine7 = { N(H0), N(H1), N(H2), N(H3), N(H4), N(H5), N(H6), N(H7), N(H8) };
            int[] verticalLine8 = { N(I0), N(I1), N(I2), N(I3), N(I4), N(I5), N(I6), N(I7), N(I8) };

            int[][] verticalLines = { verticalLine0, verticalLine1, verticalLine2, verticalLine3, verticalLine4, verticalLine5, verticalLine6, verticalLine7, verticalLine8 };

            foreach (int[] line in verticalLines)
            {
                if (line.Distinct().Count() != 9)
                {
                    return false;
                }
            }

            int[] square0 = { N(A0), N(B0), N(C0), N(A1), N(B1), N(C1), N(A2), N(B2), N(C2) };
            int[] square1 = { N(A3), N(B3), N(C3), N(A4), N(B4), N(C4), N(A5), N(B5), N(C5) };
            int[] square2 = { N(A6), N(B6), N(C6), N(A7), N(B7), N(C7), N(A8), N(B8), N(C8) };

            int[] square3 = { N(D0), N(E0), N(F0), N(D1), N(E1), N(F1), N(D2), N(E2), N(F2) };
            int[] square4 = { N(D3), N(E3), N(F3), N(D4), N(E4), N(F4), N(D5), N(E5), N(F5) };
            int[] square5 = { N(D6), N(E6), N(F6), N(D7), N(E7), N(F7), N(D8), N(E8), N(F8) };

            int[] square6 = { N(G0), N(H0), N(I0), N(G1), N(H1), N(I1), N(G2), N(H2), N(I2) };
            int[] square7 = { N(G3), N(H3), N(I3), N(G4), N(H4), N(I4), N(G5), N(H5), N(I5) };
            int[] square8 = { N(G6), N(H6), N(I6), N(G7), N(H7), N(I7), N(G8), N(H8), N(I8) };

            int[][] squares = { square0, square1, square2, square3, square4, square5, square6, square7, square8 };

            foreach (int[] square in squares)
            {
                if (square.Distinct().Count() != 9)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckAutoSolution()
        {
            for (int i = 0; i < guess.Count; i += 9)
            {
                List<int> temp = new List<int>();

                for (int j = i; j < i + 9; j++)
                {
                    temp.Add(guess[j]);
                }

                if (temp.Distinct().Count() != 9)
                {
                    return false;
                }
            }


            //int[] verticalLine0 = { N(A0), N(A1), N(A2), N(A3), N(A4), N(A5), N(A6), N(A7), N(A8) };
            //int[] verticalLine1 = { N(B0), N(B1), N(B2), N(B3), N(B4), N(B5), N(B6), N(B7), N(B8) };
            //int[] verticalLine2 = { N(C0), N(C1), N(C2), N(C3), N(C4), N(C5), N(C6), N(C7), N(C8) };
            //int[] verticalLine3 = { N(D0), N(D1), N(D2), N(D3), N(D4), N(D5), N(D6), N(D7), N(D8) };
            //int[] verticalLine4 = { N(E0), N(E1), N(E2), N(E3), N(E4), N(E5), N(E6), N(E7), N(E8) };
            //int[] verticalLine5 = { N(F0), N(F1), N(F2), N(F3), N(F4), N(F5), N(F6), N(F7), N(F8) };
            //int[] verticalLine6 = { N(G0), N(G1), N(G2), N(G3), N(G4), N(G5), N(G6), N(G7), N(G8) };
            //int[] verticalLine7 = { N(H0), N(H1), N(H2), N(H3), N(H4), N(H5), N(H6), N(H7), N(H8) };
            //int[] verticalLine8 = { N(I0), N(I1), N(I2), N(I3), N(I4), N(I5), N(I6), N(I7), N(I8) };

            //int[][] verticalLines = { verticalLine0, verticalLine1, verticalLine2, verticalLine3, verticalLine4, verticalLine5, verticalLine6, verticalLine7, verticalLine8 };

            //foreach (int[] line in verticalLines)
            //{
            //    if (line.Distinct().Count() != 9)
            //    {
            //        return false;
            //    }
            //}

            //int[] square0 = { N(A0), N(B0), N(C0), N(A1), N(B1), N(C1), N(A2), N(B2), N(C2) };
            //int[] square1 = { N(A3), N(B3), N(C3), N(A4), N(B4), N(C4), N(A5), N(B5), N(C5) };
            //int[] square2 = { N(A6), N(B6), N(C6), N(A7), N(B7), N(C7), N(A8), N(B8), N(C8) };

            //int[] square3 = { N(D0), N(E0), N(F0), N(D1), N(E1), N(F1), N(D2), N(E2), N(F2) };
            //int[] square4 = { N(D3), N(E3), N(F3), N(D4), N(E4), N(F4), N(D5), N(E5), N(F5) };
            //int[] square5 = { N(D6), N(E6), N(F6), N(D7), N(E7), N(F7), N(D8), N(E8), N(F8) };

            //int[] square6 = { N(G0), N(H0), N(I0), N(G1), N(H1), N(I1), N(G2), N(H2), N(I2) };
            //int[] square7 = { N(G3), N(H3), N(I3), N(G4), N(H4), N(I4), N(G5), N(H5), N(I5) };
            //int[] square8 = { N(G6), N(H6), N(I6), N(G7), N(H7), N(I7), N(G8), N(H8), N(I8) };

            //int[][] squares = { square0, square1, square2, square3, square4, square5, square6, square7, square8 };

            //foreach (int[] square in squares)
            //{
            //    if (square.Distinct().Count() != 9)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        private List<int> guess = new List<int>();

        private void AttemptSolution()
        {
            foreach (UIElement element in wrapPanel.Children)
            {
                if (element is TextBox textBox)
                {
                    guess.Add(R());
                }
            }
        }

        private int N(TextBox textBox)
        {
            int.TryParse(textBox.Text, out int result);
            return result;
        }

        private readonly Random random = new Random();

        private int R()
        {
            return random.Next(1, 9);
        }
    }
}
