using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Application.Common.Dtos;

public class SegmentRequestDto
{
    public string Name { get; set; } = string.Empty;
}

public class ProductRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string SegmentType { get; set; } = string.Empty;
}

public class RegressionParamsRequestDto
{
    public decimal RSquareLimit { get; set; }
    public decimal PValueSelected { get; set; }
}

public class ExpectedCorrelationRequestDto
{
    public CorrelationDirection PrimeLendingRate { get; set; }
    public CorrelationDirection Inflation { get; set; }
    public CorrelationDirection YieldOnTreasuryBills { get; set; }
    public CorrelationDirection ExchangeRate { get; set; }
    public CorrelationDirection MonetaryPolicyRate { get; set; }
}

public class CollateralTypeRequestDto
{
    public string Type { get; set; } = string.Empty;
    public decimal HaircutPercent { get; set; }
    public bool Perfected { get; set; }
    public int DurationYears { get; set; }
}

// update request Dto
public class UpdateSegmentRequestDto
{
    public string? Name { get; set; }
}

public class UpdateProductRequestDto
{
    public string? Name { get; set; }
    public string? SegmentType { get; set; }
}

public class UpdateRegressionParamsRequestDto
{
    public decimal? RSquareLimit { get; set; }
    public decimal? PValueSelected { get; set; }
}

public class UpdateExpectedCorrelationRequestDto
{
    public CorrelationDirection? PrimeLendingRate { get; set; }
    public CorrelationDirection? Inflation { get; set; }
    public CorrelationDirection? YieldOnTreasuryBills { get; set; }
    public CorrelationDirection? ExchangeRate { get; set; }
    public CorrelationDirection? MonetaryPolicyRate { get; set; }
}

public class UpdateCollateralTypeRequestDto
{
    public string? Type { get; set; }
    public decimal? HaircutPercent { get; set; }
    public bool? Perfected { get; set; }
    public int? DurationYears { get; set; }
}


// Response DTOs
public class SettingsResponseDto
{
    public IEnumerable<SegmentResponseDto> Segments { get; set; } = [];
    public IEnumerable<ProductResponseDto> Products { get; set; } = [];
    public IEnumerable<RegressionParamsResponseDto> RegressionParams { get; set; } = [];
    public IEnumerable<ExpectedCorrelationResponseDto> ExpectedCorrelations { get; set; } = [];
    public IEnumerable<CollateralTypeResponseDto> CollateralTypes { get; set; } = [];
}
public class SegmentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public string UpdatedByName { get; set; } = string.Empty;
}
public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SegmentType {get; set;} = string.Empty;
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public string UpdatedByName { get; set; } = string.Empty;
}

public class RegressionParamsResponseDto
{
    public Guid Id { get; set; }
    public decimal RSquareLimit { get; set; }
    public decimal PValueSelected { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public string UpdatedByName { get; set; } = string.Empty;
}

public class ExpectedCorrelationResponseDto
{
    public Guid Id { get; set; }
    public CorrelationDirection PrimeLendingRate { get; set; }
    public CorrelationDirection Inflation { get; set; }
    public CorrelationDirection YieldOnTreasuryBills { get; set; }
    public CorrelationDirection ExchangeRate { get; set; }
    public CorrelationDirection MonetaryPolicyRate { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public string UpdatedByName { get; set; } = string.Empty;
}

public class CollateralTypeResponseDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal HaircutPercent { get; set; }
    public bool Perfected { get; set; }
    public bool IsActive { get; set; }
    public int DurationYears { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public string UpdatedByName { get; set; } = string.Empty;
}