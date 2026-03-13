using System;
using System.Collections.Generic;

namespace Sensorialab.Domain.Entities;

// 1. Users and Profiles
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // "QGrader", "CustomerAdmin", "CustomerUser"
    public string PreferredLanguage { get; set; } = "pt-BR"; // For multi-language support (pt-BR, en-US)
    
    // SSO Authentication
    public string AuthProvider { get; set; } = "Local"; // "Local", "Google", "Microsoft"
    public string ExternalAuthId { get; set; } = string.Empty; // The subject/ID from the SSO provider

    // If the user belongs to a Customer Company
    public Guid? CustomerCompanyId { get; set; }
    public CustomerCompany? CustomerCompany { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class QGraderProfile
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!; // Navigation property
    public string Bio { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    
    // Legacy CQI Certification (2004 Protocol)
    public string LegacyCqiCertificationNumber { get; set; } = string.Empty;
    public DateTime? LegacyCertificateExpiryDate { get; set; }
    public bool IsLegacyVerified { get; set; } // Verified against the CQI database
    
    // SCA Evolved Q Grader Certification (CVA Protocol)
    public string CvaCertificationNumber { get; set; } = string.Empty;
    public DateTime? CvaCertificateExpiryDate { get; set; }
    public bool IsCvaVerified { get; set; } // Verified against the SCA database

    public bool CertifiesArabica { get; set; }
    public bool CertifiesRobusta { get; set; }
    
    // Supported Protocols & Pricing
    public bool SupportsCva { get; set; } = true;
    public bool SupportsLegacy2004 { get; set; } = true;
    public decimal BasePricePerEvaluation { get; set; } 
}

public class CustomerCompany
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    
    public List<User> Users { get; set; } = new();
    public List<ApiKey> ApiKeys { get; set; } = new();
    
    // Default Preferences for Report Delivery
    public List<string> DefaultDeliveryEmails { get; set; } = new();
    public List<string> DefaultDeliveryWhatsAppNumbers { get; set; } = new();
}

public class ApiKey
{
    public Guid Id { get; set; }
    public Guid CustomerCompanyId { get; set; }
    public CustomerCompany CustomerCompany { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty; // e.g., "Production ERP", "Staging Server"
    public string KeyHash { get; set; } = string.Empty; // Stored as a hash. The raw key is never stored and only shown once to the user upon creation.
    
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; } // Customers can define expiration
    public bool IsRevoked { get; set; } // Customers can revoke keys
}

// 2. B2B Connections
public class QGraderConnection
{
    public Guid Id { get; set; }
    public Guid CustomerCompanyId { get; set; }
    public Guid QGraderId { get; set; }
    
    // Status can be: "Pending", "Active", "Rejected"
    public string Status { get; set; } = "Pending"; 
    public DateTime CreatedAt { get; set; }
}

// 3. The Coffee Sample
public class CoffeeSample
{
    public Guid Id { get; set; }
    public Guid? CustomerCompanyId { get; set; } // Null if a QGrader registers it manually for themselves
    public string CustomerReferenceCode { get; set; } = string.Empty; // For mapping to the customer's internal ERP
    
    // Physical Logistics
    public decimal SampleWeightGrams { get; set; } 
    
    // Traceability
    public string OriginCountry { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string FarmName { get; set; } = string.Empty;
    public string ProducerName { get; set; } = string.Empty;
    
    // Agriculture & Post-Harvest
    public string Varietal { get; set; } = string.Empty;
    public string Altitude { get; set; } = string.Empty;
    public string CropYear { get; set; } = string.Empty;
    public string ProcessingMethod { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
}

// 4. Evaluation Request (The Workflow & Logistics)
public class EvaluationRequest
{
    public Guid Id { get; set; }
    public Guid? CustomerCompanyId { get; set; } // Nullable if registered manually for an external/offline client
    public Guid QGraderId { get; set; }
    public Guid CoffeeSampleId { get; set; }
    
    // External Client Info (Used when CustomerCompanyId is null)
    public string ExternalClientName { get; set; } = string.Empty;
    
    // Delivery Destinations for the Report
    public List<string> DeliveryEmails { get; set; } = new();
    public List<string> DeliveryWhatsAppNumbers { get; set; } = new();

    public string RequestedProtocol { get; set; } = "CVA"; // "CVA" or "Legacy2004"
    public string CourierTrackingNumber { get; set; } = string.Empty; // Physical sample tracking
    
    // State Machine: Pending -> Accepted -> SampleReceived -> Evaluating -> Completed -> Rejected
    public string Status { get; set; } = "Pending"; 
    
    // If the customer used the API, they provide a URL to receive the JSON results when status == "Completed"
    public string WebhookCallbackUrl { get; set; } = string.Empty; 
    
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

// 5. The Roast Profile
public class RoastProfile
{
    public Guid Id { get; set; }
    public Guid QGraderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal AgtronScore { get; set; } // Standard measure of roast color
    public TimeSpan TotalRoastTime { get; set; }
    public decimal DevelopmentTimeRatio { get; set; } // e.g. 20.5%
}

// 6. The Core Evaluation (Abstracts CVA vs Legacy)
public class Evaluation
{
    public Guid Id { get; set; }
    public Guid QGraderId { get; set; }
    public Guid CoffeeSampleId { get; set; }
    public Guid? EvaluationRequestId { get; set; } // Links back to the customer's request, if applicable
    public Guid? RoastProfileId { get; set; } // The roast profile used for this specific cupping
    
    public string ProtocolUsed { get; set; } = "CVA"; // "CVA" or "Legacy2004"
    public bool IsConfidential { get; set; } // Controls public sharing visibility
    
    // CVA Specific Properties (Null if ProtocolUsed == "Legacy2004")
    public PhysicalAssessment? Physical { get; set; }
    public DescriptiveAssessment? Descriptive { get; set; }
    public AffectiveAssessment? Affective { get; set; }
    public ExtrinsicAssessment? Extrinsic { get; set; }

    // Legacy Specific Properties (Null if ProtocolUsed == "CVA")
    public LegacyAssessment? Legacy { get; set; }

    public DateTime EvaluatedAt { get; set; }
}

// 7. The 4 CVA Pillars
public class PhysicalAssessment
{
    public decimal MoistureContentPercentage { get; set; }
    public int Category1DefectsCount { get; set; }
    public int Category2DefectsCount { get; set; }
    public string Color { get; set; } = string.Empty;
}

public class DescriptiveAssessment
{
    // Objective Intensities (Typically 1-15 scale, completely neutral)
    public int FragranceAromaIntensity { get; set; }
    public int FlavorIntensity { get; set; }
    public int AcidityIntensity { get; set; }
    public int SweetnessIntensity { get; set; }
    public int BodyIntensity { get; set; }

    // Flavor Wheel CATA (Check-All-That-Apply) 
    // In PostgreSQL, this can be efficiently stored as a JSONB array of strings or IDs mapping to the Flavor Wheel
    public List<string> FlavorNotes { get; set; } = new();
}

public class AffectiveAssessment
{
    // Subjective Quality (Hedonic Scale, typically 1-9 or -4 to +4)
    public int OverallImpressionOfQuality { get; set; }
    public int FlavorQuality { get; set; }
    public int AcidityQuality { get; set; }
    public int BodyQuality { get; set; }
    public string PersonalNotes { get; set; } = string.Empty;
}

public class ExtrinsicAssessment
{
    // Value markers beyond the physical bean
    public List<string> Certifications { get; set; } = new(); // e.g. Organic, Fair Trade
    public string SustainabilityPractices { get; set; } = string.Empty;
    public string ProducerStory { get; set; } = string.Empty;
}

// 8. Legacy 2004 Protocol
public class LegacyAssessment
{
    public decimal FragranceAroma { get; set; }
    public decimal Flavor { get; set; }
    public decimal Aftertaste { get; set; }
    public decimal Acidity { get; set; }
    public decimal Body { get; set; }
    public decimal Uniformity { get; set; }
    public decimal Balance { get; set; }
    public decimal CleanCup { get; set; }
    public decimal Sweetness { get; set; }
    public decimal Overall { get; set; }
    public decimal DefectsDeduction { get; set; }
    
    public decimal FinalScore { get; set; } // Max 100
    public List<string> Notes { get; set; } = new();
}
