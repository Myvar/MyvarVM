namespace MyvarVM.Core
{
    public class CPUState
    {
        public Mode Mode { get; set; } = Mode.RealMode;
    }

    public enum Mode
    {
        RealMode,
        ProtectedMode
    }
}