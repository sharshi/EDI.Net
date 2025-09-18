using System;
using System.Collections.Generic;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

/// <summary>
/// Represents the Health Care Claim Payment/Advice EDI transaction (835) based on ASC X12N TR3, Version 005010X221A1.
/// </summary>
public class HealthCareClaimPaymentAdvice_835
{
    #region ISA and IEA
    [EdiValue("X(2)", Path = "ISA/0", Description = "ISA01 - Authorization Information Qualifier")]
    public string AuthorizationInformationQualifier { get; set; }

    [EdiValue("X(10)", Path = "ISA/1", Description = "ISA02 - Authorization Information")]
    public string AuthorizationInformation { get; set; }

    [EdiValue("X(2)", Path = "ISA/2", Description = "ISA03 - Security Information Qualifier")]
    public string SecurityInformationQualifier { get; set; }

    [EdiValue("X(10)", Path = "ISA/3", Description = "ISA04 - Security Information")]
    public string SecurityInformation { get; set; }

    [EdiValue("X(2)", Path = "ISA/4", Description = "ISA05 - Interchange ID Qualifier")]
    public string InterchangeIdQualifier { get; set; }

    [EdiValue("X(15)", Path = "ISA/5", Description = "ISA06 - Interchange Sender ID")]
    public string InterchangeSenderId { get; set; }

    [EdiValue("X(2)", Path = "ISA/6", Description = "ISA07 - Interchange ID Qualifier")]
    public string InterchangeIdQualifier2 { get; set; }

    [EdiValue("X(15)", Path = "ISA/7", Description = "ISA08 - Interchange Receiver ID")]
    public string InterchangeReceiverId { get; set; }

    [EdiValue("X(6)", Path = "ISA/8", Format = "yyMMdd", Description = "ISA09 - Interchange Date")]
    public string InterchangeDate { get; set; }

    [EdiValue("X(4)", Path = "ISA/9", Format = "HHmm", Description = "ISA10 - Interchange Time")]
    public string InterchangeTime { get; set; }

    [EdiValue("X(1)", Path = "ISA/10", Description = "ISA11 - Repetition Separator")]
    public string RepetitionSeparator { get; set; }

    [EdiValue("X(5)", Path = "ISA/11", Description = "ISA12 - Interchange Control Version Number")]
    public string InterchangeControlVersion { get; set; }

    [EdiValue("X(9)", Path = "ISA/12", Description = "ISA13 - Interchange Control Number")]
    public string InterchangeControlNumber { get; set; }

    [EdiValue("X(1)", Path = "ISA/13", Description = "ISA14 - Acknowledgement Requested")]
    public string AcknowledgementRequested { get; set; }

    [EdiValue("X(1)", Path = "ISA/14", Description = "ISA15 - Usage Indicator")]
    public string UsageIndicator { get; set; }

    [EdiValue("X(1)", Path = "ISA/15", Description = "ISA16 - Component Element Separator")]
    public char? ComponentElementSeparator { get; set; }

    [EdiValue("X(1)", Path = "IEA/0", Description = "IEA01 - Number of Included Functional Groups")]
    public int GroupsCount { get; set; }

    [EdiValue("X(9)", Path = "IEA/1", Description = "IEA02 - Interchange Control Number")]
    public string TrailerControlNumber { get; set; }
    #endregion

    public List<FunctionalGroup> Groups { get; set; }

    [EdiGroup]
    public class FunctionalGroup
    {
        [EdiValue("X(2)", Path = "GS/0", Description = "GS01 - Functional Identifier Code")]
        public string FunctionalIdentifierCode { get; set; }

        [EdiValue("X(15)", Path = "GS/1", Description = "GS02 - Application Sender's Code")]
        public string ApplicationSenderCode { get; set; }

        [EdiValue("X(15)", Path = "GS/2", Description = "GS03 - Application Receiver's Code")]
        public string ApplicationReceiverCode { get; set; }

        [EdiValue("X(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "GS04 - Date")]
        public string Date { get; set; }

        [EdiValue("X(8)", Path = "GS/4", Format = "HHmm", Description = "GS05 - Time")]
        public string Time { get; set; }

        [EdiValue("X(9)", Path = "GS/5", Description = "GS06 - Group Control Number")]
        public string GroupControlNumber { get; set; }

        [EdiValue("X(2)", Path = "GS/6", Description = "GS07 - Responsible Agency Code")]
        public string AgencyCode { get; set; }

        [EdiValue("X(12)", Path = "GS/7", Description = "GS08 - Version / Release / Industry Identifier Code")]
        public string Version { get; set; }

        public List<Transaction> Transactions { get; set; }

        [EdiValue("X(1)", Path = "GE/0", Description = "GE01 - Number of Transaction Sets Included")]
        public int TransactionsCount { get; set; }

        [EdiValue("X(9)", Path = "GE/1", Description = "GE02 - Group Control Number")]
        public string GroupTrailerControlNumber { get; set; }
    }

    [EdiMessage]
    public class Transaction
    {
        #region Header & Trailer
        [EdiValue("X(3)", Path = "ST/0", Description = "ST01 - Transaction Set Identifier Code")]
        public string TransactionSetCode { get; set; }

        [EdiValue("X(9)", Path = "ST/1", Description = "ST02 - Transaction Set Control Number")]
        public string TransactionSetControlNumber { get; set; }
        #endregion

        /// <summary>
        /// BPR - Financial Information
        /// </summary>
        public BPR_FinancialInformation FinancialInformation { get; set; }
        
        /// <summary>
        /// Loop 1000A - Payer Identification
        /// </summary>
        public Loop1000A_PayerIdentification Payer { get; set; }

        /// <summary>
        /// Loop 2000 / 2100 - A list of claim payment details.
        /// </summary>
        [EdiLoop("LX")]
        public List<Loop2100_ClaimPaymentInformation> Claims { get; set; }
        
        /// <summary>
        /// PLB - Provider Level Adjustment
        /// </summary>
        public List<PLB_ProviderAdjustment> ProviderAdjustments { get; set; }

        #region Trailer
        [EdiValue(Path = "SE/0", Description = "SE01 - Number of Included Segments")]
        public int SegmentCounts { get; set; }

        [EdiValue("X(9)", Path = "SE/1", Description = "SE02 - Transaction Set Control Number")]
        public string TrailerTransactionSetControlNumber { get; set; }
        #endregion
    }

    #region Segments and Loops
    
    [EdiSegment, EdiPath("BPR")]
    public class BPR_FinancialInformation
    {
        [EdiValue("X(2)", Path = "BPR/0", Description = "BPR01 - Transaction Handling Code")]
        public string TransactionHandlingCode { get; set; }

        [EdiValue("X(18)", Path = "BPR/1", Description = "BPR02 - Total Actual Provider Payment Amount")]
        public string MonetaryAmount { get; set; }

        [EdiValue("X(1)", Path = "BPR/2", Description = "BPR03 - Credit or Debit Flag Code")]
        public string CreditDebitFlag { get; set; }

        [EdiValue("X(3)", Path = "BPR/3", Description = "BPR04 - Payment Method Code")]
        public string PaymentMethodCode { get; set; }
    }

    [EdiLoop("N1")]
    public class Loop1000A_PayerIdentification
    {
        [EdiCondition("PR", Path = "N1/0")]
        public N1_PartyIdentification PayerName { get; set; }

        [EdiCondition("2U", Path = "REF/0")]
        public REF_Identification AdditionalPayerId { get; set; }
    }

    [EdiSegment, EdiPath("N1")]
    public class N1_PartyIdentification
    {
        [EdiValue("X(3)", Path = "N1/0", Description = "N101 - Entity Identifier Code")]
        public string EntityIdentifierCode { get; set; }

        [EdiValue("X(60)", Path = "N1/1", Description = "N102 - Name")]
        public string Name { get; set; }

        [EdiValue("X(2)", Path = "N1/2", Description = "N103 - Identification Code Qualifier")]
        public string IdentificationCodeQualifier { get; set; }

        [EdiValue("X(80)", Path = "N1/3", Description = "N104 - Identification Code")]
        public string IdentificationCode { get; set; }
    }

    [EdiSegment, EdiPath("LX")]
    public class LX_HeaderNumber
    {
        [EdiValue("X(6)", Path = "LX/0", Description = "LX01 - Assigned Number")]
        public string AssignedNumber { get; set; }
    }

    [EdiSegment, EdiPath("CLP")]
    public class CLP_ClaimPayment
    {
        [EdiValue("X(38)", Path = "CLP/0", Description = "CLP01 - Patient Control Number")]
        public string PatientControlNumber { get; set; }

        [EdiValue("X(2)", Path = "CLP/1", Description = "CLP02 - Claim Status Code")]
        public string ClaimStatusCode { get; set; }
        
        [EdiValue("X(18)", Path = "CLP/2", Description = "CLP03 - Total Claim Charge Amount")]
        public string TotalClaimChargeAmount { get; set; }

        [EdiValue("X(18)", Path = "CLP/3", Description = "CLP04 - Claim Payment Amount")]
        public string ClaimPaymentAmount { get; set; }
        
        [EdiValue("X(18)", Path = "CLP/4", Description = "CLP05 - Patient Responsibility Amount")]
        public string PatientResponsibilityAmount { get; set; }

        [EdiValue("X(2)", Path = "CLP/5", Description = "CLP06 - Claim Filing Indicator Code")]
        public string ClaimFilingIndicatorCode { get; set; }
    }
    
    [EdiSegment, EdiPath("CAS")]
    public class CAS_ClaimAdjustment
    {
        [EdiValue("X(2)", Path = "CAS/0", Description = "CAS01 - Claim Adjustment Group Code")]
        public string ClaimAdjustmentGroupCode { get; set; }

        [EdiValue("X(5)", Path = "CAS/1", Description = "CAS02 - Claim Adjustment Reason Code")]
        public string ClaimAdjustmentReasonCode { get; set; }

        [EdiValue("X(18)", Path = "CAS/2", Description = "CAS03 - Monetary Amount")]
        public string MonetaryAmount { get; set; }
    }
    
    [EdiSegment, EdiPath("NM1")]
    public class NM1_PartyName
    {
        [EdiValue("X(3)", Path = "NM1/0", Description = "NM101 - Entity Identifier Code")]
        public string EntityIdentifierCode { get; set; }

        [EdiValue("X(1)", Path = "NM1/1", Description = "NM102 - Entity Type Qualifier")]
        public string EntityTypeQualifier { get; set; }
        
        // ... Other NM1 elements
        
        [EdiValue("X(2)", Path = "NM1/7", Description = "NM108 - Identification Code Qualifier")]
        public string IdentificationCodeQualifier { get; set; }

        [EdiValue("X(80)", Path = "NM1/8", Description = "NM109 - Identification Code")]
        public string IdentificationCode { get; set; }
    }

    [EdiSegment, EdiPath("REF")]
    public class REF_Identification
    {
        [EdiValue("X(3)", Path = "REF/0", Description = "REF01 - Reference Identification Qualifier")]
        public string ReferenceIdentificationQualifier { get; set; }

        [EdiValue("X(50)", Path = "REF/1", Description = "REF02 - Reference Identification")]
        public string ReferenceIdentification { get; set; }
    }

    [EdiSegment, EdiPath("AMT")]
    public class AMT_Amount
    {
        [EdiValue("X(3)", Path = "AMT/0", Description = "AMT01 - Amount Qualifier Code")]
        public string AmountQualifierCode { get; set; }

        [EdiValue("X(18)", Path = "AMT/1", Description = "AMT02 - Monetary Amount")]
        public string MonetaryAmount { get; set; }
    }
    
    [EdiSegment, EdiPath("QTY")]
    public class QTY_Quantity
    {
        [EdiValue("X(2)", Path = "QTY/0", Description = "QTY01 - Quantity Qualifier")]
        public string QuantityQualifier { get; set; }

        [EdiValue("X(15)", Path = "QTY/1", Description = "QTY02 - Quantity")]
        public string Quantity { get; set; }
    }

    [EdiSegment, EdiPath("SVC")]
    public class SVC_ServicePayment
    {
        [EdiValue("X(48)", Path = "SVC/0", Description = "SVC01 - Composite Medical Procedure Identifier")]
        public string CompositeMedicalProcedureId { get; set; }
        
        [EdiValue("X(18)", Path = "SVC/1", Description = "SVC02 - Line Item Charge Amount")]
        public string LineItemChargeAmount { get; set; }
        
        [EdiValue("X(18)", Path = "SVC/2", Description = "SVC03 - Line Item Provider Payment Amount")]
        public string LineItemProviderPaymentAmount { get; set; }
    }
    
    [EdiSegment, EdiPath("LQ")]
    public class LQ_RemarkCode
    {
        [EdiValue("X(3)", Path = "LQ/0", Description = "LQ01 - Code List Qualifier Code")]
        public string CodeListQualifierCode { get; set; }
        
        [EdiValue("X(30)", Path = "LQ/1", Description = "LQ02 - Industry Code")]
        public string IndustryCode { get; set; }
    }

    [EdiSegment, EdiPath("PLB")]
    public class PLB_ProviderAdjustment
    {
        [EdiValue("X(50)", Path = "PLB/0", Description = "PLB01 - Provider Identifier")]
        public string ProviderIdentifier { get; set; }
        
        [EdiValue("X(8)", Path = "PLB/1", Format="yyyyMMdd", Description = "PLB02 - Fiscal Period Date")]
        public string FiscalPeriodDate { get; set; }
        
        [EdiValue("X(5)", Path = "PLB/2", Description = "PLB03-1 - Adjustment Reason Code")]
        public string AdjustmentReasonCode { get; set; }
        
        [EdiValue("X(18)", Path = "PLB/3", Description = "PLB04 - Provider Adjustment Identifier")]
        public string ProviderAdjustmentIdentifier { get; set; }
    }


    [EdiLoop("SVC")]
    public class Loop2110_ServicePaymentInformation
    {
        public SVC_ServicePayment ServicePayment { get; set; }

        public List<CAS_ClaimAdjustment> ServiceAdjustments { get; set; }

        [EdiCondition("LU", "1S", "APC", "RB", Path = "REF/0")]
        public List<REF_Identification> ServiceIdentifications { get; set; }
        
        [EdiCondition("HPI", "SY", "TJ", "1C", Path = "REF/0")]
        public List<REF_Identification> RenderingProviderInfo { get; set; }

        [EdiCondition("0K", Path = "REF/0")]
        public REF_Identification HealthCarePolicyId { get; set; }

        public List<AMT_Amount> ServiceAmounts { get; set; }
        
        public List<LQ_RemarkCode> RemarkCodes { get; set; }
    }

    [EdiLoop("CLP", Path = "LX/1")]
    public class Loop2100_ClaimPaymentInformation
    {
        public LX_HeaderNumber HeaderNumber { get; set; }

        public CLP_ClaimPayment ClaimPayment { get; set; }
        
        public List<CAS_ClaimAdjustment> ClaimAdjustments { get; set; }

        [EdiCondition("QC", Path = "NM1/0")] // QC = Patient
        public NM1_PartyName PatientName { get; set; }
        
        [EdiCondition("IL", Path = "NM1/0")] // IL = Insured or Subscriber (Not used by Medicare per guide, but included for completeness)
        public NM1_PartyName InsuredName { get; set; }

        [EdiCondition("TT", Path = "NM1/0")] // TT = Transfer To (Crossover Carrier)
        public NM1_PartyName CrossoverCarrierName { get; set; }

        [EdiCondition("28", "6P", "EA", "F8", Path = "REF/0")]
        public List<REF_Identification> OtherClaimRelatedIds { get; set; }

        public List<AMT_Amount> ClaimAmounts { get; set; }
        
        public List<QTY_Quantity> ClaimQuantities { get; set; }

        public List<Loop2110_ServicePaymentInformation> ServiceLines { get; set; }
    }
    #endregion
}