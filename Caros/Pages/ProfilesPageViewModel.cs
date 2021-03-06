﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.UI;
using Caros.Music;
using Caros.Core;

namespace Caros.Pages
{
    class ProfilesPageViewModel : PageViewModel
    {
        public ProfilesPageViewModel(IContext context)
            : base(context)
        {
            Users = new BindableCollection<User>(context.Profiles.Users);
            NotifyOfPropertyChange(() => Users);
        }

        public BindableCollection<User> Users { get; set; }

        public void SwitchProfile(User user)
        {
            Context.Profiles.Switch(user);
        }
    }
}
