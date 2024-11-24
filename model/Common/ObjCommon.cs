namespace ObjCommonN {
    public class ObjCommon<TkeyId>(TkeyId _id)
    {
        public TkeyId Id { set; get; } = _id;
    }
}