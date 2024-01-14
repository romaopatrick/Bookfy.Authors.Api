using System.Globalization;

namespace Bookfy.Authors.Api.Adapters;

public static class StringsExtensions
{
    public static string TitleCase(this string? value) =>
        value is null ? default! :
            CultureInfo
                .CurrentCulture
                .TextInfo
                .ToTitleCase(value);
}