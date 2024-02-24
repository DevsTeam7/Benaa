﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class SchedualDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        [DataType(DataType.Time)]
        public TimeSpan TimeStart { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan TimeEnd { get; set; }
        public decimal? Price { get; set; } = 0;

        public string TeacherId { get; set; }
        public string? StudentId { get; set; }
    }
}