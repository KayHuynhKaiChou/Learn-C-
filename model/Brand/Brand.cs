using ObjCommonN;

namespace BrandN {
    public class Brand<TkeyId> : ObjCommon<TkeyId> {
        public string Name {set; get;}

        public Brand(TkeyId id, string name) : base(id) {
            Name = name;
        }
    }
}