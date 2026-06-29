namespace PolarisEcl.Domain.Enums;

public enum ComputationStatus
{
    NotDone,
    Pending,
    Approved,
    Returned
}

public enum HistoricalMarginType
{
    Beta,
    ExpertJudgement
}

public enum MacroeconomicAdjustmentFactorType
{
    Value,
    Count
}

public enum FileType
{
    PdInput = 1,
    PdPoolInput = 2
}