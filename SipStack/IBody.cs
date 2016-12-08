namespace SipStack
{
    public interface IBody
    {
        #region properties

        int ContentLength { get; }

        #endregion

        #region functions

        void AddTo(MessageBuilder messageBuilder);

        #endregion
    }
}
