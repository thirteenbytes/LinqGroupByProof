using System.ComponentModel;

namespace DatabaseFix.Domain;

public class GovernmentIdPhoto : Photo
{
    public GovernmentIdPhotoType Type { get; set; }    
    
}

public enum GovernmentIdPhotoType
{
    [Description("Unknown")]
    Unknown = 0,

    [Description("Passport")]
    Passport = 1,

    [Description("Drivers License")]
    DriversLicense = 2,

    [Description("Government Employment Card")]
    GovernmentEmploymentCard = 3,

    [Description("Age of Majority Card")]
    AgeOfMajorityCard = 4,

    [Description("Canadian Citizenship Card")]
    CanadianCitizenshipCard = 5,

    [Description("International Student Card")]
    InternationalStudentCard = 6,

    [Description("Permanent Resident Card")]
    PermanentResidentCard = 7,

    [Description("Firearms Acquisition Certificate (FAC)")]
    FirearmsAcquisitionCertificate = 8,

    [Description("Canadian National Institute for the Blind Card (CNIB)")]
    CanadianNationalInstituteForTheBlindCard = 9,

    [Description("Indian Status Card")]
    IndianStatusCard = 10
}

