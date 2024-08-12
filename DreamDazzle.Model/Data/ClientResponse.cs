using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DreamDazzle.Model.Data
{
    /// <summary>
    /// Encapsulates the client response to API calls such as
    /// </summary>
    public class ClientResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        /// <summary>
        /// The <see cref="HttpStatusCode"/> from the underlying API call.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Exception is case of failed request.
        /// </summary>
        //public Exception Exception { get; set; }

        /// <summary>
        /// String representation of the HttpWebRequest similar to Fiddler's.
        /// </summary>
        public object? HttpRequest { get; set; }

        /// <summary>
        /// String representation of the HttpWebResponse similar to Fiddler's.
        /// </summary>
        public object? HttpResponse { get; set; }
        public SeverityType Severity { get; set; }
        public CodeMinorValueType MinorCode { get; set; }
    }
    /// <summary>
    /// Represents a <see cref="ClientResponse"/> that also contains a Response of type T.
    /// </summary>
    /// <typeparam name="T">The type of the Response object that is in the client response.</typeparam>
    public class ClientResponse<T> : ClientResponse where T : class
    {
        /// <summary>
        /// Get or Set the Response.
        /// </summary>
        public T Response { get; set; }
    }

    
    public enum SeverityType
    {

        /// <remarks/>
        status,

        /// <remarks/>
        warning,

        /// <remarks/>
        error,
    }
        
    public enum CodeMinorValueType
    {

        /// <remarks/>
        fullsuccess,

        /// <remarks/>
        createsuccess,

        /// <remarks/>
        nosourcedids,

        /// <remarks/>
        idallocfail,

        /// <remarks/>
        overflowfail,

        /// <remarks/>
        idallocinusefail,

        /// <remarks/>
        invaliddata,

        /// <remarks/>
        incompletedata,

        /// <remarks/>
        partialdatastorage,

        /// <remarks/>
        unknownobject,

        /// <remarks/>
        unknownquery,

        /// <remarks/>
        deletefailure,

        /// <remarks/>
        targetreadfailure,

        /// <remarks/>
        savepointerror,

        /// <remarks/>
        savepointsyncerror,

        /// <remarks/>
        toomuchdata,

        /// <remarks/>
        unsupportedlineitemtype,

        /// <remarks/>
        unknowncontext,

        /// <remarks/>
        unauthorizedresultreplace,

        /// <remarks/>
        unknownperson,

        /// <remarks/>
        gradingnotpermitted,

        /// <remarks/>
        invalidresult,

        /// <remarks/>
        resultalreadyposted,

        /// <remarks/>
        unknownextension,

        /// <remarks/>
        unknownvocabulary,

        /// <remarks/>
        unknownmdvocabulary,

        /// <remarks/>
        targetisbusy,

        /// <remarks/>
        linkfailure,

        /// <remarks/>
        unauthorizedrequest,

        /// <remarks/>
        unsupportedLIS,

        /// <remarks/>
        unsupportedLISoperation,
    }

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
}
