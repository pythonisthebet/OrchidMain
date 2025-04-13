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
        private bool inconstractor;
        private double[] scores;
        public double[] Scores
        {
            get { return scores; }

            set
            {
                scores = value;
                if (RacialBoostsScores != null && Scores != null && !inconstractor)
                {
                    scoreTotal = [0, 0, 0, 0, 0, 0];
                    AbilityModifier = [0, 0, 0, 0, 0, 0];
                }
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
                if (RacialBoostsScores != null && Scores != null && !inconstractor)
                {
                    scoreTotal = [0, 0, 0, 0, 0, 0];
                    AbilityModifier = [0, 0, 0, 0, 0, 0];
                }
                OnPropertyChanged("RacialBoostsScores");
            }
        }

        private double[] scoreTotal;
        public double[] ScoreTotal
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

        private double[] abilityModifier;
        public double[] AbilityModifier
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

        private double[] pointCost;
        public double[] PointCost
        {
            get { return pointCost; }

            set
            {
                pointCost = value;
                if (!inconstractor)
                    PointCF();
                OnPropertyChanged("PointCost");
            }
        }

        private double pointTotal;
        public double PointTotal
        {
            get { return pointTotal; }

            set
            {
                if (!inconstractor)
                    PointTF();
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
            RacialBoostsScores = [0, 0, 0, 0, 0, 0];
            Scores = [8, 8, 8, 8, 8, 8];
            ScoreTotal = [8, 8, 8, 8, 8, 8];
            AbilityModifier = [-1, -1, -1, -1, -1, -1];
            pointCost = [0, 0, 0, 0, 0, 0];
            pointTotal = 0;
            PointBuy = false;
            Alert = "";
            inconstractor = false;

        }
        #endregion

        //public ICommand Confirm => new Command(OnConfirm);

        public ICommand OnUpDownCommand => new Command(OnUpDown);

        public void OnUpDown(object obj)
        {
            string parameters = obj.ToString();
            char location = parameters[0];
            char updown = parameters[1];
            if (updown == 0)
            {
                Scores[location] = Scores[location] + 1;
            }
            else
            {
                Scores[location] = Scores[location] - 1;
            }

        }
        public void Sum()
        {
            scoreTotal = Scores.Select((x, index) => x + RacialBoostsScores[index]).ToArray();
        }
        public void SumAndTruncade()
        {
            abilityModifier = Scores.Select((x, index) => Math.Truncate((x + RacialBoostsScores[index]) / 2 - 5)).ToArray();
        }
        public void PointCF()
        {
            for (int i = 0; i < scores.Length; i++)
            {

                pointCost[i] = Scores[i] - 8 + Math.Max(Scores[i] - 13, 0) + Math.Max(Scores[i] - 15, 0);
                if (Scores[i] > 15 && PointBuy)
                {
                    Alert = "Score can be at most 15";
                }
                else if (Scores[i] < 8 && PointBuy)
                {
                    Alert = "Score needs to be at least 8";
                }
                else
                {
                    Alert = "";
                    if (PointTotal > 27)
                    {
                        PointTotal = 0;
                    }
                }
            }
            pointCost = Scores.Select((x, index) => (x - 8)).ToArray();
        }
        public void PointTF()
        {
            for (int i = 0; i < PointCost.Length; i++)
            {
                PointTotal += PointCost[i];
            }
            if (PointTotal > 27)
            {
                if (Alert != "")
                {
                    Alert += "\n ";
                }
                Alert = "you can use at most 27 points";
            }
            else
            {
                Alert = "";
                PointCost = [0];
            }
        }
    }
}
