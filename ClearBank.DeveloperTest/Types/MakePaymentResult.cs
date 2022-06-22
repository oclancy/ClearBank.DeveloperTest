namespace ClearBank.DeveloperTest.Types
{
    public class MakePaymentResult
    {
        MakePaymentResult(bool success)
        {
            _success = success;
        }
        private readonly bool _success;

        public bool Success { get { return _success;} }
        

        public static MakePaymentResult Failed { get; } = new MakePaymentResult(false);
        public static MakePaymentResult Succeeded { get; } = new MakePaymentResult(true);
    }
}
