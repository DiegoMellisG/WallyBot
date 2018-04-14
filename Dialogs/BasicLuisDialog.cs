using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }
        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("Start")]
        public async Task StartIntent(IDialogContext context, LuisResult result)
        {
            
        }
        [LuisIntent("Bienvenida")]
        public async Task BienvenidaIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hola, c�mo est�s Javier?");
        }
        [LuisIntent("Estado")]
        public async Task EstadoIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Super!. �En qu� te puedo ayudar?");
        }
        [LuisIntent("Conocer")]
        public async Task NombreIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Mi nombre es WallyBotsito y estoy para servirte en algunas cosas por mientras voy aprendiendo.");
        }
        
        [LuisIntent("Horario")]
        public async Task HorarioIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Tu horarios es:\nL1 - Sistope\nL2 - PLF\nM1 - DBD");
        }

        [LuisIntent("HorarioDia")]
        public async Task HorarioDiaIntent(IDialogContext context, LuisResult result)
        {
            
            EntityRecommendation day;
            if (result.TryFindEntity("datetimeV2", out day))
            {
                await context.PostAsync($"Hoy d�a...");
                if (day.Equals("lunes"))
                    await context.PostAsync($"Tienes PLF");
                else
                    await context.PostAsync($"No tienes clases");
            }
            await context.PostAsync($"{day.ToString()}");

            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Pruebas")]
        public async Task PruebasIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }
        [LuisIntent("Despedida")]
        public async Task DespedidaIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Estar� atento a tus proximas preguntas, siempre recuerda que estar� a tus servicios.");
        }
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            //await this.ShowLuisResult(context, result);
            await context.PostAsync($"A�n no he aprendido a responder esa pregunta, en el futuro podr� satisfacer tus necesidades.");
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}.");
            context.Wait(MessageReceived);
        }
    }
}