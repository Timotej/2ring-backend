using System;

namespace project
{
    public class PositionResponse : BaseResponse
    {
        public Position Position { get; private set; }

        private PositionResponse(bool success, string message, Position position) : base(success, message)
        {
            Position = position;
        }
        public PositionResponse(Position position) : this(true, string.Empty, position)
        { }

        public PositionResponse(string message) : this(false, message, null)
        { }
    }
}