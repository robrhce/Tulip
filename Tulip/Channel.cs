//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tulip
{
    using System;
    using System.Collections.Generic;
    
    public partial class Channel
    {
        public Channel()
        {
            this.OutstationChannelMappings = new HashSet<OutstationChannelMapping>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public int TTL { get; set; }
        public int Multidrop { get; set; }
    
        public virtual ICollection<OutstationChannelMapping> OutstationChannelMappings { get; set; }
    }
}
