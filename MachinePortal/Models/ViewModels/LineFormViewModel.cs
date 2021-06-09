﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class LineFormViewModel
    {
        [Display(Name = "Line")]
        public Line Line { get; set; }

        public LineFormViewModel()
        {
        }

        public LineFormViewModel(Line line)
        {
            Line = line;
        }
    }
}
