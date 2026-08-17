using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Exceptions;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Services;


public class SettingServices : ISettingsService
{
    private readonly IAppDbContext _context;
    private readonly ILogger<SettingServices> _logger;

    public SettingServices(IAppDbContext context, ILogger<SettingServices> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<SettingsResponseDto> GetSettingsAsync()
    {
        var segments = await _context.Segments
            .Select(s => new SegmentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive,
                UpdatedAt = s.UpdatedAt,
                UpdatedById = s.UpdatedById,
                UpdatedByName = $"{s.UpdatedBy.FirstName} {s.UpdatedBy.LastName}"
            })
            .ToListAsync();

        var products = await _context.Products
        .Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            SegmentType = p.SegmentType,
            IsActive = p.IsActive,
            UpdatedAt = p.UpdatedAt,
            UpdatedById = p.UpdatedById,
            UpdatedByName = $"{p.UpdatedBy.FirstName} {p.UpdatedBy.LastName}"
        })
        .ToListAsync();

        var regressionParams = await _context.RegressionParameters
        .Select(r => new RegressionParamsResponseDto
        {
            Id = r.Id,
            RSquareLimit = r.RSquareLimit,
            PValueSelected = r.PValueSelected,
            IsActive = r.IsActive,
            UpdatedAt = r.UpdatedAt,
            UpdatedById = r.UpdatedById,
            UpdatedByName = $"{r.UpdatedBy.FirstName} {r.UpdatedBy.LastName}"
        })
        .ToListAsync();

        var correlations = await _context.ExpectedCorrelations
        .Select(c => new ExpectedCorrelationResponseDto
        {
            Id = c.Id,
            PrimeLendingRate = c.PrimeLendingRate,
            Inflation = c.Inflation,
            YieldOnTreasuryBills = c.YieldOnTreasuryBills,
            ExchangeRate = c.ExchangeRate,
            MonetaryPolicyRate = c.MonetaryPolicyRate,
            IsActive = c.IsActive,
            UpdatedAt = c.UpdatedAt,
            UpdatedById = c.UpdatedById,
            UpdatedByName = $"{c.UpdatedBy.FirstName} {c.UpdatedBy.LastName}"
        })
        .ToListAsync();

        var collateralTypes = await _context.CollateralTypes
        .Select(ct => new CollateralTypeResponseDto
        {
            Id = ct.Id,
            Type = ct.Type,
            HaircutPercent = ct.HaircutPercent,
            Perfected = ct.Perfected,
            DurationYears = ct.DurationYears,
            IsActive = ct.IsActive,
            UpdatedAt = ct.UpdatedAt,
            UpdatedById = ct.UpdatedById,
            UpdatedByName = $"{ct.UpdatedBy.FirstName} {ct.UpdatedBy.LastName}"
        })
        .ToListAsync();

        _logger.LogInformation("Settings retrieved successfully.");
        return new SettingsResponseDto
        {
            Segments = segments,
            Products = products,
            RegressionParams = regressionParams,
            ExpectedCorrelations = correlations,
            CollateralTypes = collateralTypes
        };
    }
    public async Task<SegmentResponseDto> AddSegmentAsync(SegmentRequestDto request, User user)
    {

        var segmentData = new Segment
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsActive = true,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = user

        };

        _context.Segments.Add(segmentData);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"new Segment added");
        return new SegmentResponseDto
        {
            Id = segmentData.Id,
            Name = segmentData.Name,
            IsActive = segmentData.IsActive,
            UpdatedByName = $"{segmentData.UpdatedBy.FirstName} {segmentData.UpdatedBy.LastName}"
        };

    }
    public async Task<ProductResponseDto> AddProductAsync(ProductRequestDto request, User user)
    {

        var productData = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            SegmentType = request.SegmentType,
            IsActive = true,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = user

        };

        _context.Products.Add(productData);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"new Product added");

        return new ProductResponseDto
        {
            Id = productData.Id,
            Name = productData.Name,
            SegmentType = productData.SegmentType,
            IsActive = productData.IsActive,
            UpdatedByName = $"{productData.UpdatedBy.FirstName} {productData.UpdatedBy.LastName}"
        };

    }
    public async Task<RegressionParamsResponseDto> AddRegressionParamsAsync(RegressionParamsRequestDto request, User user)
    {

        var regressionParamsData = new RegressionParameter
        {
            Id = Guid.NewGuid(),
            RSquareLimit = request.RSquareLimit,
            PValueSelected = request.PValueSelected,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = user

        };

        _context.RegressionParameters.Add(regressionParamsData);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"new Regression Parameter added");
        return new RegressionParamsResponseDto
        {
            Id = regressionParamsData.Id,
            RSquareLimit = regressionParamsData.RSquareLimit,
            PValueSelected = regressionParamsData.PValueSelected,
            IsActive = regressionParamsData.IsActive,
            UpdatedByName = $"{regressionParamsData.UpdatedBy.FirstName} {regressionParamsData.UpdatedBy.LastName}"
        };

    }
    public async Task<ExpectedCorrelationResponseDto> AddCorrelationAsync(ExpectedCorrelationRequestDto request, User user)
    {

        var correlationData = new ExpectedCorrelation
        {
            Id = Guid.NewGuid(),
            PrimeLendingRate = request.PrimeLendingRate,
            Inflation = request.Inflation,
            YieldOnTreasuryBills = request.YieldOnTreasuryBills,
            ExchangeRate = request.ExchangeRate,
            MonetaryPolicyRate = request.MonetaryPolicyRate,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = user

        };

        _context.ExpectedCorrelations.Add(correlationData);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"new correlation data added");
        return new ExpectedCorrelationResponseDto
        {
            Id = correlationData.Id,
            PrimeLendingRate = correlationData.PrimeLendingRate,
            Inflation = correlationData.Inflation,
            YieldOnTreasuryBills = correlationData.YieldOnTreasuryBills,
            ExchangeRate = correlationData.ExchangeRate,
            MonetaryPolicyRate = correlationData.MonetaryPolicyRate,
            IsActive = correlationData.IsActive,
            UpdatedByName = $"{correlationData.UpdatedBy.FirstName} {correlationData.UpdatedBy.LastName}"
        };

    }
    public async Task<CollateralTypeResponseDto> AddCollateralTypeAsync(CollateralTypeRequestDto request, User user)
    {

        var collateralTypeData = new CollateralType
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            HaircutPercent = request.HaircutPercent,
            Perfected = request.Perfected,
            IsActive = true,
            DurationYears = request.DurationYears,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = user

        };

        _context.CollateralTypes.Add(collateralTypeData);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"new collateral types added");
        return new CollateralTypeResponseDto
        {
            Id = collateralTypeData.Id,
            Type = collateralTypeData.Type,
            HaircutPercent = collateralTypeData.HaircutPercent,
            Perfected = collateralTypeData.Perfected,
            DurationYears = collateralTypeData.DurationYears,
            IsActive = collateralTypeData.IsActive,
            UpdatedByName = $"{collateralTypeData.UpdatedBy.FirstName} {collateralTypeData.UpdatedBy.LastName}"
        };

    }

    public async Task<SegmentResponseDto> UpdateSegmentAsync(UpdateSegmentRequestDto request, Guid segmentId)
    {
        var segment = await _context.Segments
        .Include(p => p.UpdatedBy)
        .FirstOrDefaultAsync(p => p.Id == segmentId);

        if (segment == null)
        {
            throw new NotFoundException($"Segment with ID {segmentId} not found.");
        }

        var finalName = !string.IsNullOrWhiteSpace(request.Name) ? request.Name : segment.Name;

        segment.Name = request.Name ?? segment.Name;
        segment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Segment with ID {segmentId} updated.");
        return new SegmentResponseDto
        {
            Id = segment.Id,
            IsActive = segment.IsActive,
            UpdatedAt = segment.UpdatedAt,
            UpdatedById = segment.UpdatedById,
            UpdatedByName = $"{segment.UpdatedBy.FirstName} {segment.UpdatedBy.LastName}"
        };
    }
    public async Task<ProductResponseDto> UpdateProductAsync(UpdateProductRequestDto request, Guid productId)
    {
        var product = await _context.Products
        .Include(p => p.UpdatedBy)
        .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {productId} not found.");
        }

        product.Name = request.Name ?? product.Name;
        product.SegmentType = !string.IsNullOrWhiteSpace(request.SegmentType) ? request.SegmentType : product.SegmentType;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Product with ID {productId} updated.");
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            SegmentType = product.SegmentType,
            IsActive = product.IsActive,
            UpdatedAt = product.UpdatedAt,
            UpdatedById = product.UpdatedById,
            UpdatedByName = $"{product.UpdatedBy.FirstName} {product.UpdatedBy.LastName}"
        };
    }
    public async Task<RegressionParamsResponseDto> UpdateRegressionParamsAsync(UpdateRegressionParamsRequestDto request, Guid regressionParamId)
    {
        var regressionParam = await _context.RegressionParameters
        .Include(p => p.UpdatedBy)
        .FirstOrDefaultAsync(p => p.Id == regressionParamId);

        if (regressionParam == null)
        {
            throw new NotFoundException($"Regression Parameter with ID {regressionParamId} not found.");
        }

        regressionParam.RSquareLimit = request.RSquareLimit ?? regressionParam.RSquareLimit;
        regressionParam.PValueSelected = request.PValueSelected ?? regressionParam.PValueSelected;
        regressionParam.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Regression Parameter with ID {regressionParamId} updated.");
        return new RegressionParamsResponseDto
        {
            Id = regressionParam.Id,
            RSquareLimit = regressionParam.RSquareLimit,
            PValueSelected = regressionParam.PValueSelected,
            IsActive = regressionParam.IsActive,
            UpdatedAt = regressionParam.UpdatedAt,
            UpdatedById = regressionParam.UpdatedById,
            UpdatedByName = $"{regressionParam.UpdatedBy.FirstName} {regressionParam.UpdatedBy.LastName}"
        };
    }
    public async Task<ExpectedCorrelationResponseDto> UpdateCorrelationAsync(UpdateExpectedCorrelationRequestDto request, Guid correlationId)
    {
        var correlation = await _context.ExpectedCorrelations
        .Include(p => p.UpdatedBy)
        .FirstOrDefaultAsync(p => p.Id == correlationId);

        if (correlation == null)
        {
            throw new NotFoundException($"Expected Correlation with ID {correlationId} not found.");
        }

        correlation.PrimeLendingRate = request.PrimeLendingRate ?? correlation.PrimeLendingRate;
        correlation.Inflation = request.Inflation ?? correlation.Inflation;
        correlation.YieldOnTreasuryBills = request.YieldOnTreasuryBills ?? correlation.YieldOnTreasuryBills;
        correlation.ExchangeRate = request.ExchangeRate ?? correlation.ExchangeRate;
        correlation.MonetaryPolicyRate = request.MonetaryPolicyRate ?? correlation.MonetaryPolicyRate;
        correlation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Expected Correlation with ID {correlationId} updated.");
        return new ExpectedCorrelationResponseDto
        {
            Id = correlation.Id,
            PrimeLendingRate = correlation.PrimeLendingRate,
            Inflation = correlation.Inflation,
            YieldOnTreasuryBills = correlation.YieldOnTreasuryBills,
            ExchangeRate = correlation.ExchangeRate,
            MonetaryPolicyRate = correlation.MonetaryPolicyRate,
            IsActive = correlation.IsActive,
            UpdatedAt = correlation.UpdatedAt,
            UpdatedById = correlation.UpdatedById,
            UpdatedByName = $"{correlation.UpdatedBy.FirstName} {correlation.UpdatedBy.LastName}"
        };
    }
    public async Task<CollateralTypeResponseDto> UpdateCollateralTypeAsync(UpdateCollateralTypeRequestDto request, Guid collateralTypeId)
    {
        var collateralType = await _context.CollateralTypes
        .Include(p => p.UpdatedBy)
        .FirstOrDefaultAsync(p => p.Id == collateralTypeId);

        if (collateralType == null)
        {
            throw new NotFoundException($"Collateral Type with ID {collateralTypeId} not found.");
        }

        collateralType.Type = request.Type ?? collateralType.Type;
        collateralType.HaircutPercent = request.HaircutPercent ?? collateralType.HaircutPercent;
        collateralType.Perfected = request.Perfected ?? collateralType.Perfected;
        collateralType.DurationYears = request.DurationYears ?? collateralType.DurationYears;
        collateralType.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Collateral Type with ID {collateralTypeId} updated.");
        return new CollateralTypeResponseDto
        {
            Id = collateralType.Id,
            Type = collateralType.Type,
            HaircutPercent = collateralType.HaircutPercent,
            Perfected = collateralType.Perfected,
            DurationYears = collateralType.DurationYears,
            IsActive = collateralType.IsActive,
            UpdatedAt = collateralType.UpdatedAt,
            UpdatedById = collateralType.UpdatedById,
            UpdatedByName = $"{collateralType.UpdatedBy.FirstName} {collateralType.UpdatedBy.LastName}"
        };
    }

    public async Task<string> DeleteSegmentAsync(Guid segmentId)
    {
        var segment = await _context.Segments.FindAsync(segmentId);
        if (segment == null)
        {
            throw new NotFoundException($"Segment with ID {segmentId} not found.");
        }
        segment.IsActive = false;
        segment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Segment with ID {segmentId} deleted.");
        return "Successful";
    }
    public async Task<string> DeleteProductAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new NotFoundException($"Product with ID {productId} not found.");
        }

        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Product with ID {productId} deleted.");
        return "Successful";
    }
    public async Task<string> DeleteRegressionParamsAsync(Guid regressionId)
    {
        var regressionParam = await _context.RegressionParameters.FindAsync(regressionId);
        if (regressionParam == null)
        {
            throw new NotFoundException($"Regression Parameter with ID {regressionId} not found.");
        }

        regressionParam.IsActive = false;
        regressionParam.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Regression Parameter with ID {regressionId} deleted.");
        return "Successful";
    }
    public async Task<string> DeleteCorrelationAsync(Guid correlationId)
    {
        var correlation = await _context.ExpectedCorrelations.FindAsync(correlationId);
        if (correlation == null)
        {
            throw new NotFoundException($"Expected Correlation with ID {correlationId} not found.");
        }

        correlation.IsActive = false;
        correlation.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Expected Correlation with ID {correlationId} deleted.");
        return "Successful";
    }
    public async Task<string> DeleteCollateralTypeAsync(Guid collateralTypeId)
    {
        var collateralType = await _context.CollateralTypes.FindAsync(collateralTypeId);
        if (collateralType == null)
        {
            throw new NotFoundException($"Collateral Type with ID {collateralTypeId} not found.");
        }

        collateralType.IsActive = false;
        collateralType.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Collateral Type with ID {collateralTypeId} deleted.");
        return "Successful";
    }
}