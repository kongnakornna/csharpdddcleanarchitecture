namespace ICMON.Domain.Enums;

public enum JobStatus
{
    Open,
    InProgress,
    QuotationPending,
    QuotationApproved,
    PartPicking,
    RepairInProgress,
    RepairDone,
    InvoicePending,
    InvoiceCreated,
    PaymentReceived,
    Closed
}
