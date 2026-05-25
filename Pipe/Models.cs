#nullable disable

using System;

namespace Pipe
{
    public class Pipeline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double StartKm { get; set; }
        public double EndKm { get; set; }
        public double DiameterMm { get; set; }
        public double WallThicknessMm { get; set; }
        public string SteelGrade { get; set; }
        public double YieldStrengthMpa { get; set; }
        public double DesignPressureMpa { get; set; }
        public double OperatingPressureMpa { get; set; }
        public string Region { get; set; }
        public DateTime CommissioningDate { get; set; }
    }

    public class Inspection
    {
        public int Id { get; set; }
        public int PipelineId { get; set; }
        public DateTime Date { get; set; }
        public string ToolType { get; set; }
        public double? SpeedMps { get; set; }
        public int CoveragePercent { get; set; }
        public string Status { get; set; }
    }

    public class Defect
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public double DistanceM { get; set; }
        public int AngleDeg { get; set; }
        public string DefectType { get; set; }
        public double DepthPercent { get; set; }
        public double DepthMm { get; set; }
        public double LengthMm { get; set; }
        public double WidthMm { get; set; }
        public double? AllowablePressureMpa { get; set; }
        public double? Erf { get; set; }
        public string Severity { get; set; }
    }
}