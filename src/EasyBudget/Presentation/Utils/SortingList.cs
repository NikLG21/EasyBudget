using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Presentation.Enums;

namespace EasyBudget.Presentation.Utils
{
    public class SortingList
    {
        //false - спадання, true - зростання
        public bool Direction { get; set; }
        public SortingEntity Entity { get; set; }

        public SortingList()
        {
            Entity = SortingEntity.DateRequested;
            Direction = false;
        }
    }
}
