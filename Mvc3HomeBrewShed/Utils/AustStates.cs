using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3HomeBrewShed.Utils
{
    class Aust_State
    {

        public string Name { get; set; }
        public string Abbreviations { get; set; }

        public Aust_State()
        {
            Name = null;
            Abbreviations = null;
        }

        public Aust_State(string ab, string name)
        {
            Name = name;
            Abbreviations = ab;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Abbreviations, Name);
        }

    }

    static class AustStates
    {
        static List<Aust_State> states;

        static AustStates()
        {
            states = new List<Aust_State>(8);
            states.Add(new Aust_State("ACT", "Australian Capital Territory"));            
            states.Add(new Aust_State("NSW", "New South Wales"));
            states.Add(new Aust_State("NT", "Northern Territory"));
            states.Add(new Aust_State("QLD", "Queensland"));
            states.Add(new Aust_State("SA", "South Australia"));    
            states.Add(new Aust_State("TAS", "Tasmaia"));            
            states.Add(new Aust_State("VIC", "Victoria"));
            states.Add(new Aust_State("WA", "Western Australia"));
        }

        public static string[] Abbreviations()
        {
            List<string> abbrevList = new List<string>(states.Count);
            foreach (var state in states)
            {
                abbrevList.Add(state.Abbreviations);
            }
            return abbrevList.ToArray();
        }

        public static string[] Names()
        {
            List<string> nameList = new List<string>(states.Count);
            foreach (var state in states)
            {
                nameList.Add(state.Name);
            }
            return nameList.ToArray();
        }
        
        /// <summary>
        /// States
        /// </summary>
        /// <returns>List of Aust States and Codes</returns>
        public static List<Aust_State> States()
        {
            return states;
        }

    }

    
}