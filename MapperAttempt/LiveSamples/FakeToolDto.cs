namespace TinyMapper.Tests.LiveSamples
{
    public class FakeToolDto
    {
        public string Id { get; set; }
        public string EncodedId { get; set; }
        public string ArticleNumber { get; set; }
        public string Name { get; set; }

        public double? GaugeLength { get; set; }
        public double? BodyDiameter { get; set; }
        public double? ShankDiameter { get; set; }
        public double? ShankLength { get; set; }
        public double? CuttingEdgeLength { get; set; }
        public double? CuttingEdgeDiameter { get; set; }
        public double? TotalLength { get; set; }

        public double? UseableLength { get; set; }
        public bool? HasCooling { get; set; }
        public double? HelixAngle { get; set; }
        public double? PointDiameter { get; set; }
        public double? PointLength { get; set; }
        public double? PointAngle { get; set; }
        public double? ChamferAngle { get; set; }
        public bool? IsTurningDirectionLeft { get; set; }
        public double? CornerRadius { get; set; }
        public double? EdgeNumber { get; set; } 

        public double? CollarLength { get; set; }
        public double? CollarDiameter { get; set; }
        public double? ClampingDiameter { get; set; }
        public double? ExternalDiameter { get; set; }
        public double? InternalDiameter { get; set; }
        public double? LongStep { get; set; }
    }
}