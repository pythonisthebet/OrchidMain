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

            // Extract the current word being typed
            string text = e.NewTextValue;
            int wordStart = text.LastIndexOf(' ', Math.Max(0, _cursorPosition - 1)) + 1;
            int wordEnd = text.IndexOf(' ', _cursorPosition);
            if (wordEnd == -1) wordEnd = text.Length;

            if (wordStart <= wordEnd && wordStart < text.Length)
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
        }

        private void OnSuggestionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                string selectedSuggestion = e.SelectedItem.ToString();
                string currentText = Text ?? string.Empty;

                // Find word boundaries
                int wordStart = currentText.LastIndexOf(' ', Math.Max(0, _cursorPosition - 1)) + 1;
                int wordEnd = currentText.IndexOf(' ', _cursorPosition);
                if (wordEnd == -1) wordEnd = currentText.Length;

                // Replace the current word with the selected suggestion
                string newText = currentText.Substring(0, wordStart) + selectedSuggestion;
                if (wordEnd < currentText.Length)
                {
                    newText += currentText.Substring(wordEnd);
                }

                Text = newText;
                CursorPosition = wordStart + selectedSuggestion.Length;

                HideSuggestions();
                _suggestionsListView.SelectedItem = null;
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
