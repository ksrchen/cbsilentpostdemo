//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pts.models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProviderSetting
    {
        public int ProviderSettingID { get; set; }
        public int ProviderID { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    
        public virtual Provider Provider { get; set; }
    }
}
