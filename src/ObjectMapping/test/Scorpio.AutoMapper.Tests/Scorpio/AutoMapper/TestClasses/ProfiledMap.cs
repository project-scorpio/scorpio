using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AutoMapper.TestClasses
{
    public class ProfiledMapSource
    {
        public string Value { get; set; }

        public string IgnoreValue { get; set; }
    }

    public class ProfiledMapDestination
    {
        public string Value { get; set; }
        public string IgnoreValue { get; set; }
    }
}
