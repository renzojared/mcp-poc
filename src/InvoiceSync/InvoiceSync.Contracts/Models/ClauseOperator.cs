namespace InvoiceSync.Contracts.Models;

public enum ClauseOperator
{
    Equals = 1,
    NotEquals = 2,
    Contains = 3,
    NotContains = 4,
    StartsWith = 5,
    EndsWith = 6,
    IsEmpty = 7,
    IsNotEmpty = 8,

    GreaterThan = 9,
    GreaterThanOrEqual = 10,
    LessThan = 11,
    LessThanOrEqual = 12,
    Between = 13,

    IsTrue = 14,
    IsFalse = 15,

    IsNull = 16,
    IsNotNull = 17,

    ContainsAny = 18,
    AnyEquals = 19,
    AnyGreaterThan = 20,
    AllEquals = 21,
    AllGreaterThan = 22,
    ArrayLengthEquals = 23,
    ArrayLengthGreaterThan = 24,
    ArrayNotEmpty = 25,
    ArrayIsEmpty = 26,

    AnyGreaterThanOrEqual = 27,
    AnyLessThan = 28,
    AnyLessThanOrEqual = 29,
    AllGreaterThanOrEqual = 30,
    AllLessThan = 31,
    AllLessThanOrEqual = 32
}