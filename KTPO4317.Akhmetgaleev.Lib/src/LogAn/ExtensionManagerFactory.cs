namespace KTPO4317.Akhmetgaleev.Lib.LogAn;


public static class ExtensionManagerFactory
{
        private static IExtensionManager? _customManager;

        public static IExtensionManager Create()
        {
            if (_customManager != null)
            {
                return _customManager;
            }
            
            return new FileExtensionManager();
        }

        public static void SetManager(IExtensionManager? mgr)
        {
            _customManager = mgr;
        }

}