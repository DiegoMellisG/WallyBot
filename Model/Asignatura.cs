using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Model
{
    [Serializable]
    public class Asignatura
    {
        public Asignatura(string name, string code) { this.Name = name; this.Code = code; }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}