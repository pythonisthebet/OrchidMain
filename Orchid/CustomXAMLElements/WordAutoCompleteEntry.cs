using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Orchid.CustomXAMLElements
{
    public class WordAutoCompleteEntry : Entry
    {
        public static readonly BindableProperty SuggestionsProperty =
            BindableProperty.Create(nameof(Suggestions), typeof(List<string>), typeof(WordAutoCompleteEntry), new List<string>());

        public List<string> Suggestions
        {
            get => (List<string>)GetValue(SuggestionsProperty);
            set => SetValue(SuggestionsProperty, value);
        }

        private ListView _suggestionsListView;
        private Grid _mainGrid;
        private string _currentWord = string.Empty;
        private int _cursorPosition;

        public WordAutoCompleteEntry()
        {
            TextChanged += OnTextChanged;
            Focused += OnEntryFocused;
            Unfocused += OnEntryUnfocused;

            // Create suggestions list view
            _suggestionsListView = new ListView
            {
                IsVisible = false,
                HeightRequest = 150,
                BackgroundColor = Colors.White,
                SeparatorColor = Colors.LightGray,
                VerticalOptions = LayoutOptions.Start
            };

            _suggestionsListView.ItemSelected += OnSuggestionSelected;

            // Set up container grid
            _mainGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            _mainGrid.Add(this, 0, 0);
            _mainGrid.Add(_suggestionsListView, 0, 1);
        }

        public Grid GetContainerGrid()
        {
            return _mainGrid;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                HideSuggestions();
                return;
            }

            _cursorPosition = CursorPosition;

            try
            {
                // Extract the current word being typed
                string text = e.NewTextValue;

                // Ensure cursor position is valid
                _cursorPosition = Math.Min(_cursorPosition, text.Length);

                // Find the start of the current word
                int wordStart = -1;
                if (_cursorPosition > 0)
                {
                    wordStart = text.LastIndexOf(' ', Math.Max(0, _cursorPosition - 1));
                }
                wordStart += 1; // Move past the space or start at 0

                // Find the end of the current word
                int wordEnd = text.IndexOf(' ', _cursorPosition);
                if (wordEnd == -1) wordEnd = text.Length;

                // Check bounds to avoid exceptions
                if (wordStart >= 0 && wordStart < text.Length && wordStart <= wordEnd)
                {
                    _currentWord = text.Substring(wordStart, wordEnd - wordStart);

                    // Filter suggestions based on current word
                    if (!string.IsNullOrEmpty(_currentWord))
                    {
                        var matchingSuggestions = Suggestions
                            .Where(s => s.StartsWith(_currentWord, StringComparison.OrdinalIgnoreCase))
                            .Take(5)
                            .ToList();

                        if (matchingSuggestions.Any())
                        {
                            _suggestionsListView.ItemsSource = matchingSuggestions;
                            ShowSuggestions();
                        }
                        else
                        {
                            HideSuggestions();
                        }
                    }
                    else
                    {
                        HideSuggestions();
                    }
                }
                else
                {
                    HideSuggestions();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnTextChanged: {ex.Message}");
                HideSuggestions();
            }
        }

        private void OnSuggestionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                try
                {
                    string selectedSuggestion = e.SelectedItem.ToString();
                    string currentText = Text ?? string.Empty;

                    // Ensure cursor position is valid
                    _cursorPosition = Math.Min(_cursorPosition, currentText.Length);

                    // Find word boundaries
                    int wordStart = -1;
                    if (_cursorPosition > 0)
                    {
                        wordStart = currentText.LastIndexOf(' ', Math.Max(0, _cursorPosition - 1));
                    }
                    wordStart += 1; // Move past the space or start at 0

                    int wordEnd = currentText.IndexOf(' ', _cursorPosition);
                    if (wordEnd == -1) wordEnd = currentText.Length;

                    // Replace the current word with the selected suggestion
                    if (wordStart >= 0 && wordStart <= currentText.Length)
                    {
                        string newText = currentText.Substring(0, wordStart) + selectedSuggestion;
                        if (wordEnd < currentText.Length)
                        {
                            newText += currentText.Substring(wordEnd);
                        }

                        Text = newText;
                        CursorPosition = wordStart + selectedSuggestion.Length;
                    }

                    HideSuggestions();
                    _suggestionsListView.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in OnSuggestionSelected: {ex.Message}");
                }
            }
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            // Logic for when entry is focused
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            // Delay hiding suggestions to allow selection
            Microsoft.Maui.Controls.Application.Current.Dispatcher.Dispatch(() =>
            {
                HideSuggestions();
            });
        }

        private void ShowSuggestions()
        {
            _suggestionsListView.IsVisible = true;
        }

        private void HideSuggestions()
        {
            _suggestionsListView.IsVisible = false;
        }
    }
}