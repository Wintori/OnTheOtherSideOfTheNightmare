using System.Collections.Generic;
using System.Linq;

namespace QFSW.QC.Suggestors
{
    public class CommandSuggestor : BasicCachedQcSuggestor<CommandData>
    {
        protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
        {
            return context.Depth == 0;
        }

        protected override IQcSuggestion ItemToSuggestion(CommandData command)
        {
            return new CommandSuggestion(command);
        }

        protected override IEnumerable<CommandData> GetItems(SuggestionContext context, SuggestorOptions options)
        {
            string incompleteCommandName =
                context.Prompt
                    .SplitScopedFirst(' ')
                    .SplitFirst('<');

            return GetCommands(incompleteCommandName, options);
        }

        public IEnumerable<CommandData> GetCommands(string incompleteCommandName, SuggestorOptions options)
        {
            if (string.IsNullOrWhiteSpace(incompleteCommandName))
            {
                return Enumerable.Empty<CommandData>();
            }

            return QuantumConsoleProcessor.GetAllCommands()
                .Where(command => SuggestorUtilities.IsCompatible(incompleteCommandName, command.CommandName, options));
        }

        protected override bool IsMatch(SuggestionContext context, IQcSuggestion suggestion, SuggestorOptions options)
        {
            // Perform filtering in GetCommands
            return true;
        }
    }
}