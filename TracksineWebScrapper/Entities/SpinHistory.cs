﻿using Microsoft.EntityFrameworkCore;

namespace TracksineWebScrapper.Entities
{
    public class SpinHistory
    {
        //[Browsable(false)]        
        public int Id { get; set; }
        public string OccuredAt { get; set; }
        public string SlotResult { get; set; }
        public string SpinResult { get; set; }
        public string Multiplier { get; set; }
        [Precision(18, 0)]
        public decimal TotalWinners { get; set; }
        public string TotalPayout { get; set; }

    }
}
