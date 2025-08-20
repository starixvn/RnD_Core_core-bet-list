using Fanex.BetList.Core.Entities;
using System;
using System.Text.Json;

namespace Fanex.BetList.Core.Utils
{
    /// <summary>
    /// Helper class for working with ChoiceInput JSON data
    /// </summary>
    public static class ChoiceInputHelper
    {
        /// <summary>
        /// Deserializes JSON string to ChoiceInput object
        /// </summary>
        /// <param name="jsonString">JSON string to deserialize</param>
        /// <returns>Deserialized ChoiceInput object</returns>
        public static ChoiceInput DeserializeFromJson(string jsonString)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                return JsonSerializer.Deserialize<ChoiceInput>(jsonString, options);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize JSON: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Serializes ChoiceInput object to JSON string
        /// </summary>
        /// <param name="choiceInput">ChoiceInput object to serialize</param>
        /// <returns>JSON string representation</returns>
        public static string SerializeToJson(ChoiceInput choiceInput)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                return JsonSerializer.Serialize(choiceInput, options);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to serialize to JSON: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Example usage method showing how to work with the JSON data
        /// </summary>
        /// <param name="jsonExample">Example JSON string</param>
        public static void ProcessChoiceInputExample(string jsonExample)
        {
            try
            {
                var choiceInput = DeserializeFromJson(jsonExample);
                
                Console.WriteLine($"Player: {choiceInput.PlayerName} (ID: {choiceInput.PlayerId})");
                Console.WriteLine($"Team ID: {choiceInput.TeamId}");
                Console.WriteLine($"Is Handicap: {choiceInput.IsHandicap}");
                
                Console.WriteLine("Selections:");
                foreach (var selection in choiceInput.Selections)
                {
                    Console.WriteLine($"  - {selection.Type}: {selection.Value}");
                }
                
                Console.WriteLine("Results:");
                foreach (var result in choiceInput.Results)
                {
                    Console.WriteLine($"  - {result.Type}: {result.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing choice input: {ex.Message}");
            }
        }
    }
}
