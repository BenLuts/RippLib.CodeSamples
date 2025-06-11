namespace SmartEnums
{
    public class MyEnum : Enumeration
    {
        #region Public Enums Values
        public static readonly MyEnum ValueOne = new ValueOneType();
        public static readonly MyEnum ValueTwo = new ValueTwoType();
        #endregion

        #region Constructors
        private MyEnum(int value, string name) : base(value, name)
        {
        }
        #endregion

        #region Methods
        #endregion

        private sealed class ValueOneType : MyEnum
        {
            public ValueOneType() : base(0, "Value One")
            {
            }
        }

        private sealed class ValueTwoType : MyEnum
        {
            public ValueTwoType() : base(1, "Value Two")
            {
            }
        }
    }
}
