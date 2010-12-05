using Microsoft.VisualStudio.Text;

namespace Raconteur
{
    public static class SyntaxExtensions
    {
        public static bool IsTableRow(this ITextSnapshotLine line)
        {
            return line.GetText().Trim().StartsWith("|") && line.GetText().Trim().EndsWith("|");
        }

        public static bool IsTableHeader(this ITextSnapshotLine line)
        {
            return line.PreviousLine().GetText().Trim().Equals(
                Settings.Language.Examples + ":");
        }

        public static ITextSnapshotLine PreviousLine(this ITextSnapshotLine line)
        {
            return line.LineNumber > 1 ? line.Snapshot.GetLineFromLineNumber(line.LineNumber - 1) : null;
        }
    }
}