namespace MyvarVM.Core
{
    public class CPUState
    {
        public Mode Mode { get; set; } = Mode.RealMode;
        public bool Running { get; set; }
    }

    public enum Mode
    {
        RealMode,
        ProtectedMode
    }
}