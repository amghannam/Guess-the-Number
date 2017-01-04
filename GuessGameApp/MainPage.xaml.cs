using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections; 
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GuessGameApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int answer = GetRandomNumber(); // initial random value 

        public MainPage()
        {
            this.InitializeComponent();
            this.DisableGuessingComponents();
        }

        private static int GetRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 21); 
        }

        private void EnableGuessingComponents()
        {
            tbxGuess.IsEnabled = true;
            btnGuess.IsEnabled = true;
        }

        private void DisableGuessingComponents()
        {
            tbxGuess.IsEnabled = false;
            btnGuess.IsEnabled = false;
        }

        private void ReadyButtonActionPerformed(object sender, RoutedEventArgs e)
        {
            // As soon as the button is hit, display a message acknowledging the action. 
            txtGo.Text = $"Hey there, {tbxName.Text}! You can now make a guess!";
            txtPlayerName.Text = tbxName.Text; 
            // Enable the text box and button so as to allow the user to proceed. 
            EnableGuessingComponents();
        }

        private void GuessButtonActionPerformed(object sender, RoutedEventArgs e)
        {
            // First, declare and initialize the variables 
            int attemptsRemaining = Int32.Parse(txtRemaining.Text);
            int guess;
            // Now check the guesses and proceed accordingly.
            bool parseSuccessful = Int32.TryParse(tbxGuess.Text, out guess);
            while (attemptsRemaining > 0)
            {
                // Check the input type for the guess
                if (!parseSuccessful)
                {
                    MessageDialog msg = new MessageDialog("Only numerical input is accepted!");
                    msg.ShowAsync();
                    tbxGuess.Text = ""; 
                }
                else
                {
                    // Compare the guess against the answer and respond accordingly 
                    if (guess == answer)
                    {
                        txtResult.Text = $"Well done, {tbxName.Text}! You answered correctly!";
                        btnGuess.IsEnabled = false;
                        break;
                    }
                    else if (guess > answer)
                    {
                        txtResult.Text = "Too large!";
                        txtRemaining.Text = "";
                        attemptsRemaining--;
                        txtRemaining.Text = attemptsRemaining.ToString();
                    }
                    else if (guess < answer)
                    {
                        txtResult.Text = "Too small!";
                        txtRemaining.Text = "";
                        attemptsRemaining--;
                        txtRemaining.Text = attemptsRemaining.ToString();
                    }
                }
                break; // just skip this 
            }
            if (attemptsRemaining == 0)
            {
                txtResult.Text = $"You have run out of attempts! The correct answer is {answer}.";
                btnGuess.IsEnabled = false;
            }

        }

        private void RestartButtonActionPerformed(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "";
            txtRemaining.Text = "5";
            txtGo.Text = "";
            txtResult.Text = "";
            tbxGuess.Text = "";
            tbxName.Text = "";
            txtPlayerName.Text = ""; 
            DisableGuessingComponents();
            this.answer = GetRandomNumber(); // overwrrite with a new random value 
        }

        private void ExitButtonActionPerformed(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit(); 
        }
    }
}
