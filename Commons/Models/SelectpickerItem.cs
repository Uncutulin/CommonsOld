using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Models
{
    /// <summary>
    /// Model for the select and ajax-select taghelpers.
    /// </summary>
    public class SelectpickerItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string Subtext { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; } 
        public bool Disabled { get; set; } = false;
        public bool Divider { get; set; } = false;
        
    }
}
