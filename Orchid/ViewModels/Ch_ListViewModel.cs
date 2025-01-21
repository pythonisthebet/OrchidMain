using Orchid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.ViewModels
{
    public class Ch_ListViewModel : ViewModelBase
    {
        private List<Character> chList;

        public List<Character> ChList 
        {
            get { return chList; }

            set
            {
                chList = value;
                OnPropertyChanged("ChList");
            }
        }
    }
}
