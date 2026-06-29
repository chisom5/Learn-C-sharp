namespace PolarisEcl.Application.Common.Dtos;

public class DashboardRequestDto
{
    public int? Month { get; set; }
    public int Year { get; set; }
}

public class DashboardResponseDto
{
    public IEnumerable<SegmentDistributionDto> DistributionBySegment { get; set; } = []; //chart1
    public IEnumerable<StageDistributionDto> DistributionByStage { get; set; } = []; //chart1
    public IEnumerable<EadByStageDto> EadBySegmentAndStage { get; set; } = []; //chart2
    public IEnumerable<TrendDataDto> TrendChartByMonth { get; set; } = []; //chart3
    public IEnumerable<PDDistributionDto> PDDistributionBySegment { get; set; } = [];
}
public class SegmentDistributionDto
{
    public decimal ECL { get; set; }
    public decimal EAD { get; set; }
    public string Segment { get; set; } = string.Empty;
}

public class StageDistributionDto
{
    public decimal ECL { get; set; }
    public decimal EAD { get; set; }
    public string Stages { get; set; } = string.Empty;
}

public class EadByStageDto
{
    public decimal Stage1 { get; set; }
    public decimal Stage2 { get; set; }
    public decimal Stage3 { get; set; }

    public string Segment { get; set; } = string.Empty;
}

public class TrendDataDto
{
    public string Month { get; set; } = string.Empty;
    public decimal ECL { get; set; }
    public decimal EAD { get; set; }
}
public class PDDistributionDto
{
    public string Segment { get; set; } = string.Empty;
    public decimal RG1 { get; set; }
    public decimal RG2 { get; set; }

}