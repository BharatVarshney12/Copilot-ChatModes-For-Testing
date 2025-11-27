namespace SeleniumTests.Driver
{
    /// <summary>
    /// Configuration options for the WebDriver
    /// </summary>
    public class DriverOptions
    {
        /// <summary>
        /// The browser type to use for tests
        /// </summary>
        public BrowserType BrowserType { get; set; } = BrowserType.Chrome;

        /// <summary>
        /// Whether to run the browser in headless mode
        /// </summary>
        public bool Headless { get; set; } = false;

        /// <summary>
        /// Implicit wait timeout in seconds
        /// </summary>
        public int ImplicitWaitSeconds { get; set; } = 10;

        /// <summary>
        /// Page load timeout in seconds
        /// </summary>
        public int PageLoadTimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Whether to maximize the browser window on start
        /// </summary>
        public bool MaximizeWindow { get; set; } = true;
    }
}
