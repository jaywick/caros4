using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caros.Core.Context
{
    public interface ITheme
    {
        void Set(Theme.Style style);
    }

    public class Theme : ContextComponent, ITheme
    {
        public enum Style { Light, Dark }

        private const string LightStyleResource = "Caros.Core;component/Styles/CarosLight.xaml";
        private const string DarkStyleResource = "Caros.Core;component/Styles/CarosDark.xaml";

        public Theme(IContext context)
            : base(context)
        {
            if (Context.Environment.IsNight)
                Set(Style.Dark);
            else
                Set(Style.Light);
        }

        public void Set(Style style)
        {
            RemovePreviousStyle(style);

            // apply new style
            var res = LoadResource(style);
            Application.Current.Resources.MergedDictionaries.Add(res);
        }

        private ResourceDictionary LoadResource(Style style)
        {
            var res = new ResourceDictionary();
            res.Source = GetResourceUri(style);
            return res;
        }

        private void RemovePreviousStyle(Style style)
        {
            var previousResUri = GetResourceUri(GetOtherStyle(style));

            var previousRes = Application.Current.Resources.MergedDictionaries
                .SingleOrDefault(x => x.Source == previousResUri);

            if (previousRes != null)
                Application.Current.Resources.MergedDictionaries.Remove(previousRes);
        }

        private Uri GetResourceUri(Style style)
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
