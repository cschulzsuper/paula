using Super.Paula.Application.Localization;
using System.Globalization;

namespace Super.Paula.Client.Localization
{
    public class TranslationHandlerFactory
    {

        public static ITranslationHandler Create()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            return currentCulture switch
            {
                "de" or "de-AT" or "de-CH" or "de-DE" or "de-LI" or "de-LU"
                    => new LocalGermanTranslationHandler(),

                _ => new LocalFallbackTranslationHandler(),
            };
        }
    }
}
