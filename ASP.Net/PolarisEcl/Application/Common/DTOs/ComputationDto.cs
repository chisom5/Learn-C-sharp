using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Application.Common.Dtos;

public class AddNewComputationRequestDto
{
    public string Month { get; set; } = string.Empty;
    public int Year { get; set; }
}

public class ModelInfoRequestDto
{
    public Guid ComputationId { get; set; }
    public int Pd_Baseline { get; set; }
    public int Pd_Bestcase { get; set; }
    public int Pd_Worstcase { get; set; }
    public HistoricalMarginType HistoricalMargin { get; set; }
    public MacroeconomicAdjustmentFactorType AdjustmentFactor { get; set; }
}

// update
public class UpdateModelInfoRequestDto
{
    public Guid ComputationId { get; set; }
    public int? Pd_Baseline { get; set; }
    public int? Pd_Bestcase { get; set; }
    public int? Pd_Worstcase { get; set; }
    public HistoricalMarginType? HistoricalMargin { get; set; }
    public MacroeconomicAdjustmentFactorType? AdjustmentFactor { get; set; }
}

// Responses

public class NewComputationResponseDto
{
    public Guid Id { get; set; }
    public string ComputationName { get; set; } = string.Empty;
}