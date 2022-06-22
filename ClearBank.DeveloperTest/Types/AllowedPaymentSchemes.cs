using System;

namespace ClearBank.DeveloperTest.Types
{
    [Flags] // Attribute for correct ToString() behaviour
    public enum AllowedPaymentSchemes
    {
        FasterPayments = 1 << 0,
        Bacs = 1 << 1,
        Chaps = 1 << 2
    }
}
