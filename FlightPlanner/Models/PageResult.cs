﻿namespace FlightPlanner.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public FlightRequest[] Items { get; set; }
    }
}