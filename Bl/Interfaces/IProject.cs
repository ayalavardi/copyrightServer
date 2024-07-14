﻿using Bl.Interfaces;
using Dto.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Interfaces
{
    public interface IProject :IBlcrud<Projects>
    {
       
        Task<bool> IsOnTheDB(int? id);
        bool IsCorrectDates(DateTime? start, DateTime? end);
        bool IsOnlyLetters(string input);
    }
}
