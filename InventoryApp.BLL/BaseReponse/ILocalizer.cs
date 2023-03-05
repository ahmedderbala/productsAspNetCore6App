﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.BLL.BaseReponse
{
    public interface ILocalizer
    {
       
        public string GetLocalizedString(string name, params object[] arguments);
    }
}
