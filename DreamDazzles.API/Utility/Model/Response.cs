namespace LoanCentral.API.Utility.Model;


public class BaseResponse
{
    public BaseResponse()
    {
        this.Errors = new List<(string Error, bool FriendlyError)>();
        this.ErrorsWithCodes = new List<(string Error, string ErrorCode, bool FriendlyError)>();
        this.CustomProperties = new Dictionary<string, string>();
    }


    /// <summary>
    /// Gets a value indicating whether request has been completed successfully
    /// </summary>
    public bool Success
    {
        get
        {
            return !this.Errors.Any() && !this.ErrorsWithCodes.Any() && !this.CustomProperties.Any();
        }
    }

    /// <summary>
    /// Add error
    /// </summary>
    /// <param name="error">Error</param>
    public void AddError(string error, bool friendly = true)
    {
        this.Errors.Add((error, friendly));
    }

    public void AddErrorWithCode(string code, string error, bool friendly = true)
    {
        this.ErrorsWithCodes.Add((error, code, friendly));
        this.Errors.Add((error, friendly));
    }

    /// <summary>
    /// Errors
    /// </summary>
    public IList<(string Error, bool FriendlyError)> Errors { get; }

    /// <summary>
    /// Errors
    /// </summary>
    public IList<(string Error, string ErrorCode, bool FriendlyError)> ErrorsWithCodes { get; }

    public Dictionary<string, string> CustomProperties { get; set; }

    public enum ResponseKeyHelpers
    {
        SERVICE_UNAVAILABLE = -1
    }
}
