using System.Reflection;
using MigraDoc.DocumentObjectModel; 
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses;

public class ExpensesReportFontResolver : IFontResolver
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelper.DEFAULT_FONT);

        var length = (int)stream!.Length;

        var data = new byte[length];
        stream.Read(buffer: data, offset: 0, count: length);
        return data;
    }

    private static Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Expenses.Report.Pdf.Fonts.{faceName}.ttf");
    }
}