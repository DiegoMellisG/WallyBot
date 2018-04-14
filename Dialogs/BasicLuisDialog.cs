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
        [LuisIntent("Horario")]
        public async Task HorarioIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("HorarioDia")]
        public async Task HorarioDiaIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hoy día...");
            EntityRecommendation day;
            if (result.TryFindEntity("datetimeV2", out day))
                if(day.Equals("lunes"))
                    await context.PostAsync($"Tienes PLF");
                else
                    await context.PostAsync($"No tienes clases");
            await context.PostAsync($"{day.ToString()}");

            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Pruebas")]
        public async Task PruebasIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}.");
            context.Wait(MessageReceived);
        }
    }
}