using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caros.Context
{
    public class Theme
    {
        public enum Style { Light, Dark }

        internal Theme()
        {
            Switch(Style.Light);
        }

        private const string LightStyleResource = "Caros;component/Styles/CarosLight.xaml";
        private const string DarkStyleResource = "Caros;component/Styles/CarosDark.xaml";

        internal void Switch(Style style)
        {
            RemovePreviousStyle(style);

            // apply new style
            var res = new ResourceDictionary();
            res.Source = GetResource(style);
            Application.Current.Resources.MergedDictionaries.Add(res);
        }

        private void RemovePreviousStyle(Style style)
        {
            var previousResUri = GetResource(GetOtherStyle(style));

            var previousRes = Application.Current.Resources.MergedDictionaries
                .SingleOrDefault(x => x.Source == previousResUri);

            if (previousRes != null)
                Application.Current.Resources.MergedDictionaries.Remove(previousRes);
        }

        private Uri GetResource(Style style)
        {
            string path = "";

            if (style == Style.Light)
                path = LightStyleResource;
            else
                path = DarkStyleResource;

            return new Uri(path, UriKind.Relative);
        }

        private Style GetOtherStyle(Style style)
        {
            if (style == Style.Light)
                return Style.Dark;
            else if (style == Style.Dark)
                return Style.Light;
            else
                throw new InvalidOperationException(String.Format("Unexpected style called on Theme.GetOtherStyle '0'", style.ToString()));
        }
    }
}
