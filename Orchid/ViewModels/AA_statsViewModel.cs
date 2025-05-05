using Orchid.Models;
using Orchid.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Diagnostics.Contracts;

namespace Orchid.ViewModels
{
    public class AA_statsViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;
        private bool inconstractor = false;



        private SortedList<int, int> scores;
        public SortedList<int, int> Scores
        {
            get { return scores; }

            set
            {
                scores = value;
                OnPropertyChanged("Scores");
            }
        }

        private double[] racialBoostsScores;
        public double[] RacialBoostsScores
        {
            get { return racialBoostsScores; }

            set
            {
                racialBoostsScores = value;
                OnPropertyChanged("RacialBoostsScores");
            }
        }

        private SortedList<int, int> scoreTotal;
        public SortedList<int, int> ScoreTotal
        {
            get { return scoreTotal; }

            set
            {
                scoreTotal = value;
                if (!inconstractor)
                    Sum();
                OnPropertyChanged("ScoreTotal");
            }
        }

        private SortedList<int, int> abilityModifier;
        public SortedList<int, int> AbilityModifier
        {
            get { return abilityModifier; }

            set
            {
                abilityModifier = value;
                if (!inconstractor)
                    SumAndTruncade();
                OnPropertyChanged("AbilityModifier");
            }
        }

        private bool pointBuy;
        public bool PointBuy
        {
            get { return pointBuy; }

            set
            {
                pointBuy = value;
                OnPropertyChanged("PointBuy");
            }
        }

        private SortedList<int, int> pointCost;
        public SortedList<int, int> PointCost
        {
            get { return pointCost; }

            set
            {
                pointCost = value;
                OnPropertyChanged("PointCost");
            }
        }

        private double pointTotal;
        public double PointTotal
        {
            get { return pointTotal; }

            set
            {
                pointTotal = value;
                OnPropertyChanged("PointTotal");
            }
        }

        private string alert;
        public string Alert
        {
            get { return alert; }

            set
            {
                alert = value;
                OnPropertyChanged("Alert");
            }
        }


        #endregion

        #region constructor
        private IServiceProvider serviceProvider;
        public AA_statsViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            inconstractor = true;
            this.serviceProvider = serviceProvider;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
            RacialBoostsScores = [0, 0, 0, 0, 0, 0];
            if (Scores == null) 
            {
                Scores = new SortedList<int, int>();
                Scores.Add(0, 8);
                Scores.Add(1, 8);
                Scores.Add(2, 8);
                Scores.Add(3, 8);
                Scores.Add(4, 8);
                Scores.Add(5, 8);
            }
            

            ScoreTotal = new SortedList<int, int>();
            ScoreTotal.Add(0, 8);
            ScoreTotal.Add(1, 8);
            ScoreTotal.Add(2, 8);
            ScoreTotal.Add(3, 8);
            ScoreTotal.Add(4, 8);
            ScoreTotal.Add(5, 8);

            AbilityModifier = new SortedList<int, int>();
            AbilityModifier.Add(0, -1);
            AbilityModifier.Add(1, -1);
            AbilityModifier.Add(2, -1);
            AbilityModifier.Add(3, -1);
            AbilityModifier.Add(4, -1);
            AbilityModifier.Add(5, -1);

            pointCost = new SortedList<int, int>();
            pointCost.Add(0, 0);
            pointCost.Add(1, 0);
            pointCost.Add(2, 0);
            pointCost.Add(3, 0);
            pointCost.Add(4, 0);
            pointCost.Add(5, 0);

            pointTotal = 0;
            PointBuy = false;
            Alert = "";
            inconstractor = false;
            Sum();
            SumAndTruncade();
            PointCF();
            PointTF();
            OnPropertyChanged("Scores");
        }
        #endregion
        public async Task InitilizeAsync()
        {
            //InServerCall = true;
            ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
            if (dynamicCh != (null))
            {
                try
                {
                    dynamic temp = dynamicCh;
                    var temp2 = temp.character;
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ExpandoObject temp3 = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                    dynamic temp4 = temp3;
                    var clas = temp4.Scores;
                    List<KeyValuePair<int,int>> templist = JsonSerializer.Deserialize<List<KeyValuePair<int, int>>>(clas);
                    foreach (KeyValuePair<int, int> item in templist)
                    {
                        Scores[item.Key] = item.Value;
                    }
                    try
                    {
                        Sum();
                        SumAndTruncade();
                        PointCF();
                        PointTF();
                        OnPropertyChanged("Scores");
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception)
                {
                }
            }
            //InServerCall = false;

        }
        //public ICommand Confirm => new Command(OnConfirm);

        public ICommand OnUpDownCommand => new Command(OnUpDown);
        public ICommand ResetCommand => new Command(OnReset);
        public ICommand ConfirmCommand => new Command(OnConfirm);
        public ICommand PointBuyCommand => new Command(OnPointBuy);


        public void OnPointBuy(object obj)
        {
            PointBuy = !PointBuy;
        }
        public void OnReset()
        {
            Scores[0] = 8;
            Scores[1] = 8;
            Scores[2] = 8;
            Scores[3] = 8;
            Scores[4] = 8;
            Scores[5] = 8;
            Sum();
            SumAndTruncade();
            PointCF();
            PointTF();
            OnPropertyChanged("Scores");
        }

        public void OnUpDown(object obj)
        {
            string parameters = obj.ToString();
            int location = parameters[0] - 48;
            int updown = parameters[1] - 48;
            if (updown == 0)
            {
                Scores[location] = Scores[location] + 1;
            }
            else
            {
                Scores[location] = Scores[location] - 1;
            }
            Sum();
            SumAndTruncade();
            PointCF();
            PointTF();
            OnPropertyChanged("Scores");

        }
        public void Sum()
        {
            foreach (var item in Scores)
            {
                ScoreTotal[item.Key] = (int)(item.Value + RacialBoostsScores[item.Key]);
            }
            OnPropertyChanged("ScoreTotal");

        }
        public void SumAndTruncade()
        {
            foreach (var item in Scores)
            {
                AbilityModifier[item.Key] = (int)Math.Floor((((item.Value + RacialBoostsScores[item.Key])-10)/2));
            }
            OnPropertyChanged("AbilityModifier");

        }

        //function
        //get the point cost of every attribute
        public void PointCF()
        {
            bool needAlert = false;
            for (int i = 0; i < 6; i++)
            {

                PointCost[i] = Scores[i] - 8 + Math.Max(Scores[i] - 13, 0) + Math.Max(Scores[i] - 15, 0);
                if (Scores[i] > 15 && PointBuy)
                {
                    Alert = "Score can be at most 15 in point buy";
                    needAlert = true;
                }
                else if (Scores[i] < 8 && PointBuy)
                {
                    Alert = "Score needs to be at least 8 in point buy";
                    needAlert = true;
                }
                else
                {
                    if ((Alert == "" || Alert == "Score needs to be at least 8 in point buy" || Alert == "Score can be at most 15 in point buy") && !needAlert)
                    {
                        Alert = "";
                    }
                }
            }
            needAlert = false;

            OnPropertyChanged("Alert");
            OnPropertyChanged("PointCost");
        }
        public void PointTF()
        {
            bool needAlert = false;
            PointTotal = 0;
            for (int i = 0; i < 6; i++)
            {
                PointTotal += PointCost[i];
            }
            if (PointTotal > 27 && PointBuy)
            {
                if (Alert != "")
                {
                    Alert += "\n ";
                }
                Alert = "you can use at most 27 points in point buy";
                needAlert = true;
            }
            else
            {
                if ((Alert == "" || Alert == "you can use at most 27 points in point buy") && !needAlert)
                {
                    Alert = "";
                }
            }
            OnPropertyChanged("Alert");
            OnPropertyChanged("PointTotal");
        }

        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            List<KeyValuePair<int,int>> ScoresList = Scores.Select(s => (KeyValuePair<int, int>)s).ToList();
            if (temp.ContainsKey("Scores"))
            {
                temp.Remove("Scores");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("Scores", ScoresList.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("Scores", ScoresList.ToList());

            }
            //Selected_Color = Colors.LightGreen;
            await OrchidService.StoreCharacter(((App)Application.Current).CurrentCharacterProperties, ((App)Application.Current).CurrentCharacter.Id, ((App)Application.Current).LoggedInUser.Id);
            //from json
            ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
            dynamic temp1 = dynamicCh;
            var temp2 = temp1.character;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ((App)Application.Current).CurrentCharacterProperties = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
            await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your Stats!", "ok");

        }
    }
}
