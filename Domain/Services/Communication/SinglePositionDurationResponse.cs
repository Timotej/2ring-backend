namespace project
{
    public class SinglePositionDurationResponse : BaseResponse
    {
        public SinglePositionDuration SinglePositionDuration { get; private set; }

        private SinglePositionDurationResponse(bool success, string message, SinglePositionDuration singlePositionDuration) : base(success, message)
        {
            SinglePositionDuration = singlePositionDuration;
        }
        public SinglePositionDurationResponse(SinglePositionDuration singlePositionDuration) : this(true, string.Empty, singlePositionDuration)
        { }

        public SinglePositionDurationResponse(string message) : this(false, message, null)
        { }
    }
}