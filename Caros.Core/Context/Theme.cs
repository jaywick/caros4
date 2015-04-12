using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caros.Core.Context
{
    public enum ThemeStyle { Light, Dark }

    public interface ITheme
    {
        ThemeStyle Current { get; set; }
        void Set(ThemeStyle style);
    }

    public class Theme : ContextComponent, ITheme
    {
        private const string LightStyleResource = "Caros.Core;component/Styles/CarosLight.xaml";
        private const string DarkStyleResource = "Caros.Core;component/Styles/CarosDark.xaml";

        public ThemeStyle Current { get; set; }

        public Theme(IContext context)
            : base(context)
        {
            if (Context.Environment.IsNight)
                Set(ThemeStyle.Dark);
            else
                Set(ThemeStyle.Light);
        }

        public virtual void Set(ThemeStyle style)
        {
            RemovePreviousStyle(style);

            // apply new style
            var res = LoadResource(style);
            Application.Current.Resources.MergedDictionaries.Add(res);

            Current = style;
        }

        private ResourceDictionary LoadResource(ThemeStyle style)
        {
            var res = new ResourceDictionary();
            res.Source = GetResourceUri(style);
            return res;
        }

        private void RemovePreviousStyle(ThemeStyle style)
        {
            var previousResUri = GetResourceUri(GetOtherStyle(style));

            var previousRes = Application.Current.Resources.MergedDictionaries
                .SingleOrDefault(x => x.Source == previousResUri);

            if (previousRes != null)
                Application.Current.Resources.MergedDictionaries.Remove(previousRes);
        }

        private Uri GetResourceUri(ThemeStyle style)
        {
            string path = "";

            if (style == ThemeStyle.Light)
                path = LightStyleResource;
            else
                path = DarkStyleResource;

            return new Uri(path, UriKind.Relative);
        }

        private ThemeStyle GetOtherStyle(ThemeStyle style)
        {
            if (style == ThemeStyle.Light)
                return ThemeStyle.Dark;
            else if (style == ThemeStyle.Dark)
                return ThemeStyle.Light;
            else
                throw new InvalidOperationException(String.Format("Unexpected style called on Theme.GetOtherStyle '0'", style.ToString()));
        }
    }
}
