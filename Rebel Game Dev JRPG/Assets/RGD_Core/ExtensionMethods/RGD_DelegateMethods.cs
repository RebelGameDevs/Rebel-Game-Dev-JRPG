namespace RebelGameDevs.Utils.Containers
{
    /*
    ============================================================
    RGD_DelegateMethods:
        - This static class defines a good variety of delegates
         (both single and multi delegates).
    ============================================================
    */
    public static class RGD_DelegateMethods
    {
        //void rgdDelegate --{useful to have void methods added}--
        public delegate void RGDDelegate();

        //void rgdDelegate --{useful to have dynamic method params added}--
        public delegate void RGDDelegate<RGDType>(RGDType data);

        //void RGDDelegateDynamic --{useful to have dynamic return types for void methods}--
        public delegate RGDType RGDDelegateDynamic<RGDType>();

        //void RGDDelegateDynamicParam --{useful to have dynamic method return types and dynamic methods params added}--
        public delegate RGDType RGDDelegateDynamicParam<RGDType>(RGDType data);
    }
}
