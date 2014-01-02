//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tulip
{
    using System;
    using System.Collections.Generic;
    
    public partial class Point
    {
        public int Id { get; set; }
        public int OutstationID { get; set; }
        public POINT_TYPE Type { get; set; }
        public Nullable<int> ValueDigital { get; set; }
        public Nullable<int> ValueInteger { get; set; }
        public Nullable<float> ValueFloat { get; set; }
        public Nullable<int> ControlDigital { get; set; }
        public Nullable<int> ControlInteger { get; set; }
        public Nullable<float> ControlFloat { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<System.DateTime> LastControl { get; set; }
        public POINT_STATUS Status { get; set; }
        public int PointIndex { get; set; }
        public Nullable<System.DateTime> LastMeasurement { get; set; }
        public Nullable<int> Quality { get; set; }
    
        public virtual Outstation Outstation { get; set; }
    }
}