using System;

namespace SampleApp
{
    public class MainViewModel
    {
        public string Hello { get { return "World"; } }

        public DateTime ADate { get { return DateTime.Now; } }
        public CustomerNumber Number { get { return new CustomerNumber() { Number = "ABC 123"}; } }
    }
}