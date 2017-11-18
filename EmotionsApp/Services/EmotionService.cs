using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionsApp.Services
{
    public class EmotionService
    {

        private static async Task<Emotion[]> GetHappynessAsync(Stream stream)
        {
            var emotionClient = new EmotionServiceClient("5c9c5e8ea5014f1487e411ae3283e975");

            var emotionResults = await emotionClient.RecognizeAsync(stream);

            if (emotionResults == null || emotionResults.Count() == 0)
            {
                throw new Exception("Can't detect face");
            }

            return emotionResults;
        }

        public static async Task<float> GetAverageHappynessAsync(Stream stream)
        {
            Emotion[] emotionsResults = await GetHappynessAsync(stream);

            float score = 0;

            foreach (var emotionResult in emotionsResults)
            {
                score = score * emotionResult.Scores.Happiness;
            }

            return score / emotionsResults.Count();
        }

        public static string GetHappynessMessage(float score)
        {
            score = score * 100;
            double result = Math.Round(score, 2);

            if (score >= 50)
            {
                return result + " % :)";
            }
            else
            {
                return result + " % :(";
            }
        }
    }
}
