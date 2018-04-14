using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Model
{
    public class ClientAsignaturas
    {
        private Dictionary<string, Asignatura> asignaturas = new Dictionary<string, Asignatura>()
        {
            {"Procesamiento de Lenguajes Formales", new Asignatura("Procesamiento de Lenguajes Formales", "A-1")},
            { "Sistemas Operativos", new Asignatura("Sistemas Operativos", "B-2")}
        };

        public Asignatura GetAsignatura(string name)
            => asignaturas.ContainsKey(name.ToLower()) ? characters[name.ToLower()] : null;
    }
}