using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore.Text;

namespace Sitecore.Support
{
    public class LookupNameLookupValue: Sitecore.XA.Foundation.SitecoreExtensions.CustomFields.FieldTypes.LookupNameLookupValue
    {
        protected override void LoadValue()
        {
            if (!this.ReadOnly && !this.Disabled)
            {
                System.Web.UI.Page handler = HttpContext.Current.Handler as System.Web.UI.Page;
                NameValueCollection values = (handler != null) ? handler.Request.Form : new NameValueCollection();
                UrlString str = new UrlString();
                
                foreach (string str2 in values.Keys)
                {
                    if ((!string.IsNullOrEmpty(str2) && str2.StartsWith(this.ID + "_Param")) && !str2.EndsWith("_value"))
                    {
                        string str3 = values[str2];
                        string str4 = values[str2 + "_value"];
                        if (!string.IsNullOrEmpty(str3))
                        {
                            str[str3] = str4 ?? string.Empty;
                        }
                    }
                }
                //fix for the Bug 7094
                string decodedValue = HttpUtility.UrlDecode(str.ToString());
                    if (this.Value != decodedValue)
                    {
                        this.Value = decodedValue;
                        this.SetModified();
                    }
            }
        }
    }
}