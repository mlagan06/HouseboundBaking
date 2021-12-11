using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Models
{
    public class QuantityModel
    {
        public List<QuantityOLD> AllJobs { get; set; }
        public QuantityOLD SelectedJob { get; set; }
    }
}
