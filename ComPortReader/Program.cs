namespace ComPortReader
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }

    public class IpqcStd
    {
        public string LowerRoll { get; set; }
        public string UpperRoll { get; set; }
        public string LowerCuff { get; set; }
        public string UpperCuff { get; set; }
        public string LowerPalm { get; set; }
        public string UpperPalm { get; set; }
        public string LowerFinger { get; set; }
        public string UpperFinger { get; set; }
        public string LowerFingerTip { get; set; }
        public string UpperFingerTip { get; set; }
    }
    public class ReturnMESApiResponse
    {
        public string RunCard { get; set; }
        public double Roll { get; set; }
        public double Cuff { get; set; }
        public double Palm { get; set; }
        public double Finger { get; set; }
        public double FingerTip { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class RunCardInfoApiResponse
    {
        public string Runcard { get; set; }
        public IpqcStd Ipqc_std { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}